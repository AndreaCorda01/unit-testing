using DemoUnitTesting.Core.Entities;
using DemoUnitTesting.Core.Enums;
using DemoUnitTesting.Core.Interfaces;
using DemoUnitTesting.Core.Models;
using DemoUnitTesting.Core.Movies;
using DemoUnitTesting.DataAccess;
using DemoUnitTesting.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUnitTesting.Tests
{
    [TestFixture]
    public class MoviesServiceTests
    {
        private Mock<IFilesService> _filesServiceMock;
        private Mock<IMoviesRepository> _moviesRepositoryMock;
        private MoviesService _service;

        [SetUp]
        public void SetUp()
        {
            _filesServiceMock = new Mock<IFilesService>();
            _moviesRepositoryMock = new Mock<IMoviesRepository>();
            _service = new MoviesService(
                _filesServiceMock.Object,
                _moviesRepositoryMock.Object
            );
        }

        [Test]
        public async Task GetByGenreAsyncTest()
        {
            _moviesRepositoryMock
                .Setup(x => x.GetByGenreAsync(It.IsAny<MovieGenre>()))
                .ReturnsAsync((MovieGenre x) => new List<Movie>().AsEnumerable())
                .Verifiable();

            var result = await _service.GetByGenreAsync(MovieGenre.Action);

            _moviesRepositoryMock.VerifyAll();
        }

        [Test]
        public void CreateAsyncEmptyNameTest()
        {
            var movieStub = new Movie()
            {
                Name = ""
            };

            var ex = Assert.ThrowsAsync<Exception>(async () =>
            {
                await _service.CreateAsync(movieStub, null);
            });

            Assert.AreEqual("EMPTY_NAME", ex.Message);
        }

        [Test]
        public async Task CreateAsyncTest()
        {
            var movieStub = new Movie()
            {
                Name = "TEST_MOVIE",
                Genre = MovieGenre.Action
            };

            var posterStub = new AppFile()
            {
                FileName = "TEST_FILE",
                Content = null
            };

            _filesServiceMock
                .Setup(x => x.Save(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Verifiable();

            _moviesRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Movie>()))
                .ReturnsAsync(new Movie())
                .Verifiable();

            var result = await _service.CreateAsync(movieStub, posterStub);

            _filesServiceMock.VerifyAll();
            _moviesRepositoryMock.VerifyAll();
        }
    }
}
