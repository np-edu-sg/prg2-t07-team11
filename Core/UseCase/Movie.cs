using System.Collections.Generic;
using Core.Repository;

namespace Core.UseCase
{
    public class Movie
    {
        private readonly IMovie _movieRepository;

        public Movie(IMovie movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public List<Models.Movie> Find()
        {
            return _movieRepository.Find();
        }
    }
}