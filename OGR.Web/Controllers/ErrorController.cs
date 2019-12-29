using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace OnlineGuitarRentals.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch(statusCode)
            {
                case 400:
                    ViewData["Title"] = "Bad Request";
                    ViewData["ErrorCode"] = $"{statusCode} - Bad Request";
                    ViewData["ErrorMsg"] = "Sorry, there was a bad request. Please try reloading the page.";
                    return View("BadRequest");
                case 401:
                    ViewData["Title"] = "Unauthorized";
                    ViewData["ErrorCode"] = $"{statusCode} - Unauthorized";
                    ViewData["ErrorMsg"] = "Sorry, your login credentials don't permit you authorization to view this page. If you're not logged in, please log in and try again.";
                    return View("Unauthorized");
                case 403:
                    ViewData["Title"] = "Forbidden";
                    ViewData["ErrorCode"] = $"{statusCode} - Forbidden";
                    ViewData["ErrorMsg"] = "Sorry, access to this page is forbidden.";
                    return View("Forbidden");
                case 404:
                    ViewData["Title"] = "Page Not Found";
                    ViewData["ErrorCode"] = $"{statusCode} - Page Not Found";
                    ViewData["ErrorMsg"] = "Sorry, the resource you requested could not be found.";
                    return View("NotFound");
                case 500:
                    ViewData["Title"] = "Internal Server Error";
                    ViewData["ErrorCode"] = $"{statusCode} - Internal Server Error";
                    ViewData["ErrorMsg"] = "Sorry, an internal server error has occured. The IT team has been notified. We apologise for the inconvinience.";
                    return View("InternalServer");
                default:
                    ViewData["Title"] = "Unknown Error";
                    ViewData["ErrorCode"] = $"Status Code - {statusCode}";
                    ViewData["ErrorMsg"] = "An unexpected error has occured and the IT team has been notified. We apologise for the inconvinience.";
                    return View("Error");
            }            
        }
    }
}