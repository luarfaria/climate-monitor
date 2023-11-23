using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace ClimateMonitor.Validations
{
    public class CustomValidationProblemDetails : ValidationProblemDetails
    {
        public CustomValidationProblemDetails(IEnumerable<ValidationError> errors)
        {
            Errors = errors;
        }
        
        public new IEnumerable<ValidationError> Errors { get; } = new List<ValidationError>();
    }
}
