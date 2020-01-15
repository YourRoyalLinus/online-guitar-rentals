using FluentAssertions;
using OnlineGuitarRentals.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class HomeControllerShould
    {
        [Test]
        public void Return_Home_Page()
        {
            var controller = new HomeController();
            var result = controller.Index();
            result.Should().BeOfType<ViewResult>();
        }
    }
}

