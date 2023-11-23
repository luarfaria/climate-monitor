using Microsoft.AspNetCore.Mvc;
using ClimateMonitor.Services;
using ClimateMonitor.Services.Models;
using ClimateMonitor.Validations;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClimateMonitor.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReadingsController : ControllerBase
{
    private readonly DeviceSecretValidatorService _secretValidator;
    private readonly DeviceFirmwareValidatorService _firmwareValidator;
    private readonly AlertService _alertService;

    public ReadingsController(
        DeviceSecretValidatorService secretValidator, DeviceFirmwareValidatorService firmwareValidator,
        AlertService alertService)
    {
        _secretValidator = secretValidator;
        _firmwareValidator = firmwareValidator;
        _alertService = alertService;
    }

    /// <summary>
    /// Evaluate a sensor readings from a device and return possible alerts.
    /// </summary>
    /// <remarks>
    /// The endpoint receives sensor readings (temperature, humidity) values
    /// as well as some extra metadata (firmwareVersion), evaluates the values
    /// and generate the possible alerts the values can raise.
    /// 
    /// There are old device out there, and if they get a firmwareVersion 
    /// format error they will request a firmware update to another service.
    /// </remarks>
    /// <param name="deviceSecret">A unique identifier on the device included in the header(x-device-shared-secret).</param>
    /// <param name="deviceReadingRequest">Sensor information and extra metadata from device.</param>
    [HttpPost("evaluate")]
    public ActionResult<IEnumerable<Alert>> EvaluateReading([FromBody] DeviceReadingRequest deviceReadingRequest)
    {
        var deviceSecret = Request.Headers["x-device-shared-secret"];
        if (!_secretValidator.ValidateDeviceSecret(deviceSecret))
        {
            return Problem(
                detail: "Device secret is not within the valid range.",
                statusCode: StatusCodes.Status401Unauthorized);
        }

        if (!_firmwareValidator.ValidateDeviceFirmware(deviceReadingRequest.FirmwareVersion))
        {
            //var problem = ValidationProblem(
            //    detail: "The firmware value does not match semantic versioning format.",
            //    statusCode: StatusCodes.Status400BadRequest);
            var problemDetails = new CustomValidationProblemDetails(new List<ValidationError>
            {
                new ValidationError { Key = "FirmwareVersion", Value = "The firmware value does not match semantic versioning format."  }
            });

            return new ObjectResult(problemDetails)
            {
                StatusCode = 400
            };
        }

        return Ok(_alertService.GetAlerts(deviceReadingRequest));
    }
}
