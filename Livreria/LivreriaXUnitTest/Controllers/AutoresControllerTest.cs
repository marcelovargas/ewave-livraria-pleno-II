 namespace LivreriaXUnitTest.Controllers
{
    using ApplicationApp.Interfaces;
    using Entities;
    using LivreriaWeb.Controllers.API;
    using LivreriaXUnitTest.Services;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xunit;

    public class AutoresControllerTest
    {
        AutoresController _controller;
        IAutorApp _service;
        public AutoresControllerTest()
        {
            _service = new AutorServiceFake();
            _controller = new AutoresController(_service);

        }

        [Fact]
        public void Get_When_Called_ReturnsOk()
        {
            // Act  
            var okResult = _controller.GetAutores().Result;
            //Assert
            Assert.IsType<OkObjectResult>(okResult);

        }

   
    }
}
