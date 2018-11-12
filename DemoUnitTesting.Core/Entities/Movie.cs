using DemoUnitTesting.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUnitTesting.Core.Entities
{
    public class Movie : BaseEntity
    {
        public string Name { get; set; }
        public MovieGenre Genre { get; set; }
        public string Poster { get; set; }
    }
}
