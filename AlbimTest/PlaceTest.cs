using albim.Controllers.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Place;
using Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using albim.Result;
using Test1;
using Xunit;

namespace AlbimTest
{
    public class PlaceTest
    {
        PlaceController _controller;
        IPlaceService _service;
        IConfiguration _configuration;

        public PlaceTest()
        {
            _service = new PlaceServiceFake();
            _controller = new PlaceController(_configuration, _service);
        }
        [Fact]
        public async void Create()
        {
            PlaceViewModel model = new PlaceViewModel()
            {
                Description = "Description test",
                Name = "Name test",
                Id=1
            };
            CancellationToken cancellationToken = new CancellationToken();
            // Act
            var okResult = await _controller.Create(model, cancellationToken);
            // Assert
            Assert.IsType<PlaceViewModel>(okResult.Data);
        }
       // [Fact]
        public void Get()
        {
            int? page = 1;
            int? pageSize = 10;
            CancellationToken cancellationToken = CancellationToken.None;
            // Act
            var okResult = _controller.GetAll(page, pageSize, cancellationToken);
            // Assert
            Assert.IsType<PlaceViewModel>(okResult.Result.Data);
        }
        [Fact]
        public void Delete()
        {
            // Arrange
            var id = 1;
            CancellationToken cancellationToken = new CancellationToken();

            // Act
            var badResponse = _controller.Delete(id, cancellationToken);

            // Assert
            Assert.IsType<string>(badResponse.Result.Data);
        }

    }
}
