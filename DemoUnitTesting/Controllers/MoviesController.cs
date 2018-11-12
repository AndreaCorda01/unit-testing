using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DemoUnitTesting.Core.Entities;
using DemoUnitTesting.Core.Enums;
using DemoUnitTesting.Core.Interfaces;
using DemoUnitTesting.Core.Models;
using DemoUnitTesting.Dto;
using DemoUnitTesting.Extensions;
using DemoUnitTesting.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoUnitTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _service;
        private readonly IMapper _mapper;

        public MoviesController(
            IMoviesService moviesService,
            IMapper mapper)
        {
            _service = moviesService;
            _mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> Get()
        {
            return new OkObjectResult(
                _mapper.Map<IEnumerable<Movie>, List<MovieDto>>(await _service.GetAllAsync())
            );
        }

        [HttpGet]
        [Route("{genre}")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> Get(MovieGenre genre)
        {
            return new OkObjectResult(
                _mapper.Map<IEnumerable<Movie>, List<MovieDto>>(await _service.GetByGenreAsync(genre))
            );
        }

        // GET api/values/5
        [HttpPost]
        public async Task<ActionResult<MovieDto>> Post()
        {
            var poster =
                Request.Form.Files.Count() > 0 ?
                    new AppFile()
                    {
                        FileName = Request.Form.Files[0].FileName,
                        Content = await Request.Form.Files[0].ToByteArrayAsync()
                    } :
                    null;

            Enum.TryParse(Request.Form["Genre"].ToString(), out MovieGenre genre);
            var movie = new Movie()
            {
                Name = Request.Form["Name"].ToString(),
                Genre = genre
            };

            return new OkObjectResult(
                _mapper.Map<Movie, MovieDto>(await _service.CreateAsync(movie, poster))
            );
        }
    }
}
