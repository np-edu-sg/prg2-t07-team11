//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


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

        public void LoadData()
        {
            _cinemaRepository.Init();
        }

        public List<Models.Cinema> FindAll()
        {
            return _cinemaRepository.FindAll();
        }
    }
}