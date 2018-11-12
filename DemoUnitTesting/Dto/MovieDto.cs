using DemoUnitTesting.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUnitTesting.Dto
{
    public class MovieDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public MovieGenre Genre { get; set; }
        public string Poster { get; set; }
    }
}
