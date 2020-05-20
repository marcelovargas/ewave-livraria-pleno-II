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

    public class LivrosControllerTest
    {
        LivrosController _controller;
        ILivroApp _service;
        public LivrosControllerTest()
        {
            _service = new LivroServiceFake();
            _controller = new LivrosController(_service);

        }

        [Fact]
        public void Get_When_Called_ReturnsOk()
        {
            // Act  
            var okResult = _controller.GetLivros().Result;
            //Assert
            Assert.IsType<OkObjectResult>(okResult);

        }


    }
}
