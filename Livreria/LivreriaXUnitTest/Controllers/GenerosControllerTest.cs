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

    public class GenerosControllerTest
    {
        GenerosController _controller;
        IGeneroApp _service;
        public GenerosControllerTest()
        {
            _service = new GeneroServiceFake();
            _controller = new GenerosController(_service);

        }

        [Fact]
        public void Get_When_Called_ReturnsOk()
        {
            // Act  
            var okResult = _controller.GetGeneros().Result;
            //Assert
            Assert.IsType<OkObjectResult>(okResult);

        }


    }
}
