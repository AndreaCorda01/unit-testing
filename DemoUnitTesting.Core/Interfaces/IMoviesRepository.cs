using DemoUnitTesting.Core.Entities;
using DemoUnitTesting.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DemoUnitTesting.Core.Interfaces
{
    public interface IMoviesRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetByGenreAsync(MovieGenre genre);
    }
}
