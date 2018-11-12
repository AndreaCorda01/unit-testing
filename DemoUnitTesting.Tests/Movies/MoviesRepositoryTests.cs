using DemoUnitTesting.Core.Entities;
using DemoUnitTesting.Core.Enums;
using DemoUnitTesting.DataAccess;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUnitTesting.Tests
{
    [TestFixture]
    public class MoviesRepositoryTests
    {
        private MoviesRepository _repository;
        private ApplicationDbContext _db;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;
            _db = new ApplicationDbContext(options);
            _repository = new MoviesRepository(_db);
        }


        [TearDown]
        public void TearDown()
        {
            _db.Dispose();
        }

        [Test]
        public async Task GetByGenreTest()
        {
            _db.Movies.AddRange(new List<Movie>() {
                new Movie()
                {
                    Name = "TEST_MOVIE_1",
                    Genre = MovieGenre.Action
                },
                new Movie()
                {
                    Name = "TEST_MOVIE_2",
                    Genre = MovieGenre.Animation
                },
                new Movie()
                {
                    Name = "TEST_MOVIE_3",
                    Genre = MovieGenre.Drama
                }
            });
            _db.SaveChanges();

            var result = await _repository.GetByGenreAsync(MovieGenre.Action);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("TEST_MOVIE_1", result.First().Name);
        }
    }
}
