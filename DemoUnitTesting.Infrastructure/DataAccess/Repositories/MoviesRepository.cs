using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoUnitTesting.Core.Entities;
using DemoUnitTesting.Core.Enums;
using DemoUnitTesting.Core.Interfaces;
using DemoUnitTesting.Infrastructure.DataAccess.Repositories;

namespace DemoUnitTesting.DataAccess
{
    public class MoviesRepository : Repository<Movie>, IMoviesRepository
    {
        public MoviesRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Movie>> GetByGenreAsync(MovieGenre genre)
        {
            return await FindAsync(x => x.Genre == genre);
        }
    }
}
