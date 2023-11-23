//using Microsoft.AspNetCore.Mvc.Infrastructure;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.AspNetCore.Mvc;
//using System.Diagnostics;
//using ClimateMonitor.Validations;

//namespace ClimateMonitor.CustomProblemDetails
//{
//    public class CustomProblemDetailsFactory : ProblemDetailsFactory
//    {
//        public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string title = null,
//            string type = null, string detail = null, string instance = null)
//        {
//            var problemDetails = new ProblemDetails
//            {
//                Status = statusCode,
//                Title = title,
//                Type = type,
//                Detail = detail,
//                Instance = instance,
//            };

//            return problemDetails;
//        }

    
//    }
//}
