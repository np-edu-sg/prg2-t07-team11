using System.Collections.Generic;
using Core.Repository;

namespace Core.UseCases
{
    public class Cinema
    {
        private readonly ICinema _cinemaRepository;

        public Cinema(ICinema cinemaRepository)
        {
            _cinemaRepository = cinemaRepository;
        }

        public void LoadData() => _cinemaRepository.Init();

        public List<Models.Cinema> FindAll()
        {
            return _cinemaRepository.FindAll();
        }
    }
}