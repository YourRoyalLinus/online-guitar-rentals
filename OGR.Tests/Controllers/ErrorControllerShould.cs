using FluentAssertions;
using NUnit.Framework;
using OnlineGuitarRentals.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace UnitTests.Controllers
{
    [TestFixture]
    class ErrorControllerShould
    {
        [Test]
        public void Return_BadRequest_View()
        {
            var statusCode = 400;
            var controller = new ErrorController();

            var result = controller.HttpStatusCodeHandler(statusCode);

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData["Title"].Should().Be("Bad Request");
        }

        [Test]
        public void Return_Unathorized_View()
        {
            var statusCode = 401;
            var controller = new ErrorController();

            var result = controller.HttpStatusCodeHandler(statusCode);

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData["Title"].Should().Be("Unauthorized");
        }

        [Test]
        public void Return_Forbidden_View()
        {
            var statusCode = 403;
            var controller = new ErrorController();

            var result = controller.HttpStatusCodeHandler(statusCode);

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData["Title"].Should().Be("Forbidden");
        }

        [Test]
        public void Return_PageNotFound_View()
        {
            var statusCode = 404;
            var controller = new ErrorController();

            var result = controller.HttpStatusCodeHandler(statusCode);

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData["Title"].Should().Be("Page Not Found");
        }

        [Test]
        public void Return_ISE_View()
        {
            var statusCode = 500;
            var controller = new ErrorController();

            var result = controller.HttpStatusCodeHandler(statusCode);

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData["Title"].Should().Be("Internal Server Error");
        }

        [Test]
        public void Return_UnknownErrors_View()
        {
            Random rand = new Random();
            var statusCode = rand.Next(501, 999);
            {
                var controller = new ErrorController();

                var result = controller.HttpStatusCodeHandler(statusCode);

                var viewResult = result.Should().BeOfType<ViewResult>();
                var viewModel = viewResult.Subject.ViewData["Title"].Should().Be("Unknown Error");
            }
        }
    }
}
