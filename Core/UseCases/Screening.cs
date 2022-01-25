using System;
using System.Collections.Generic;
using Core.Repository;

namespace Core.UseCases
{
    public class Screening
    {
        private readonly IScreening _screeningRepository;
        private readonly ICinema _cinemaRepository;
        private readonly IMovie _movieRepository;

        private readonly TimeSpan _cleaningTime = new(0, 0, 30, 0);
        private readonly int _startingIdx = 1001;

        public Screening(IScreening screeningRepository, ICinema cinemaRepository, IMovie movieRepository)
        {
            _screeningRepository = screeningRepository;
            _cinemaRepository = cinemaRepository;
            _movieRepository = movieRepository;
        }

        public void LoadData() => _screeningRepository.Init();

        public List<Models.Screening> Find()
        {
            return _screeningRepository.FindAll();
        }

        public List<Models.Screening> FindAllByMovieTitle(string title) =>
            _screeningRepository.FindAllByMovieTitle(title);

        public void Add(DateTime dateTime, string screeningType, string cinemaName, int cinemaHallNo, string movieTitle)
        {
            if (screeningType is not "2D" or "3D") throw new Exception("ScreeningType must be 2D or 3D");

            var cinema = _cinemaRepository.FindOneByNameAndHallNo(cinemaName, cinemaHallNo);
            if (cinema is null) throw new Exception("Invalid cinema");

            var movie = _movieRepository.FindOneByTitle(movieTitle);
            if (dateTime < movie.OpeningDate) throw new Exception("Screening DateTime is before Movie OpeningDate");

            var screening = _screeningRepository.FindAllByCinema(cinema);

            var movieTimeSpan = TimeSpan.FromMinutes(movie.Duration);
            var proposedEndDateTime = dateTime + movieTimeSpan + _cleaningTime;

            foreach (var s in screening)
            {
                if (s.ScreeningDateTime.Date != dateTime.Date) continue;

                var existingEndDateTime = s.ScreeningDateTime + movieTimeSpan + _cleaningTime;

                if (existingEndDateTime > dateTime && s.ScreeningDateTime < proposedEndDateTime)
                    throw new Exception("Cinema hall is unavailable");
            }

            _screeningRepository.Add(new Models.Screening(_startingIdx + _screeningRepository.FindAll().Count, dateTime,
                screeningType, cinema, movie));
        }

        public List<Models.Screening> FindAllWithoutTickets()
        {
            return _screeningRepository.FindAllWithoutTickets();
        }

        public void Remove(Models.Screening screening)
        {
            _screeningRepository.Remove(screening);
        }
    }
}