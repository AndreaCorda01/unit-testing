using DemoUnitTesting.Core.Entities;
using DemoUnitTesting.Core.Enums;
using DemoUnitTesting.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUnitTesting.Core.Interfaces
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> CreateAsync(Movie movie, AppFile poster);
        Task<IEnumerable<Movie>> GetByGenreAsync(MovieGenre genre);
    }
}
