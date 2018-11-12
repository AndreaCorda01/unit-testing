using DemoUnitTesting.Core.Entities;
using DemoUnitTesting.Core.Enums;
using DemoUnitTesting.Core.Interfaces;
using DemoUnitTesting.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUnitTesting.Core.Movies
{
    public class MoviesService : IMoviesService
    {
        private readonly IFilesService _filesService;
        private readonly IMoviesRepository _moviesRepository;

        public MoviesService(
            IFilesService filesService,
            IMoviesRepository moviesRepository)
        {
            _filesService = filesService;
            _moviesRepository = moviesRepository;
        }

        public async Task<Movie> CreateAsync(Movie movie, AppFile poster)
        {
            // Movie validations
            if (string.IsNullOrEmpty(movie.Name))
                throw new Exception("EMPTY_NAME");

            // Save movie's poster in disk
            if (poster != null)
            {
                _filesService.Save(poster.FileName, poster.Content);
                movie.Poster = poster.FileName;
            }

            // Save movie in DB
            return await _moviesRepository.AddAsync(movie);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _moviesRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Movie>> GetByGenreAsync(MovieGenre genre)
        {
            return await _moviesRepository.GetByGenreAsync(genre);
        }
    }
}
