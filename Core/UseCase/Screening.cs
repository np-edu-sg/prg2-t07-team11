using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Core.Repository;

namespace Core.UseCase
{
    public class Screening
    {
        private readonly IScreening _screeningRepository;
        private readonly ICinema _cinemaRepository;
        private readonly IMovie _movieRepository;

        public Screening(IScreening screeningRepository, ICinema cinemaRepository, IMovie movieRepository)
        {
            _screeningRepository = screeningRepository;
            _cinemaRepository = cinemaRepository;
            _movieRepository = movieRepository;
        }

        public void LoadData() => _screeningRepository.Init();

        public List<Models.Screening> Find()
        {
            return _screeningRepository.Find();
        }

        public void Add(DateTime dateTime, string screeningType, string cinemaName, int cinemaHallNo, string movieTitle)
        {
            var cinema = _cinemaRepository.FindOneByNameAndHallNo(cinemaName, cinemaHallNo);
            var movie = _movieRepository.FindOneByTitle(movieTitle);
            var movieDuration = TimeSpan.FromMinutes(movie.Duration);
            var screening = _screeningRepository.FindByCinema(cinema);
            var cleaningTime = new TimeSpan(0, 0, 30, 0);
            var endDateTime = dateTime + movieDuration + cleaningTime;
            if (cinema is null) throw new Exception("Invalid cinema");

            if (dateTime < movie.OpeningDate) throw new Exception("Screening DateTime is before Movie OpeningDate");

            if (screeningType is not "2D" or "3D") throw new Exception("ScreeningType must be 2D or 3D");
            
            foreach (var s in screening)
            {
                if (s.ScreeningDateTime.Date == dateTime.Date)
                {
                    Console.WriteLine(endDateTime);
                    if (endDateTime > s.ScreeningDateTime || (dateTime > s.ScreeningDateTime && dateTime <= screeningEndDateTime))
                    {
                        throw new Exception("Cinema Hall Is Not Available");
                    }
                }
            }
            _screeningRepository.Add(new Models.Screening(_screeningRepository.Find().Count + 1, dateTime,
                screeningType, cinema, movie));
        }
    }
}