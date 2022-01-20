﻿using System.Collections.Generic;
using Core.Repository;

namespace Core.UseCase
{
    public class Cinema
    {
        private readonly ICinema _cinemaRepository;

        public Cinema(ICinema cinemaRepository)
        {
            _cinemaRepository = cinemaRepository;
        }

        public void LoadData() => _cinemaRepository.Init();

        public List<Models.Cinema> Find()
        {
            return _cinemaRepository.Find();
        }
    }
}