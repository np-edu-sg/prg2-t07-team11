﻿using System.Collections.Generic;
using Core.Repository;

namespace Core.UseCase
{
    public class Screening
    {
        private readonly IScreening _screeningRepository;

        public Screening(IScreening screeningRepository)
        {
            _screeningRepository = screeningRepository;
        }

        public void LoadData() => _screeningRepository.Init();

        public List<Models.Screening> Find()
        {
            return _screeningRepository.Find();
        }
    }
}