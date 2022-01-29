using System.Collections.Generic;
using Core.Repository;

namespace Core.UseCases
{
    public class Movie
    {
        private readonly IMovie _movieRepository;

        public Movie(IMovie movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void LoadData() => _movieRepository.Init();

        public List<Models.Movie> FindAll()
        {
            return _movieRepository.FindAll();
        }
    }
}