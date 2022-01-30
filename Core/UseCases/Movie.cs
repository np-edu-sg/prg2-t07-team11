using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Core.Repository;

namespace Core.UseCases
{
    public class Movie
    {
        private readonly IMovie _movieRepository;
        private readonly IOrder _orderRepository;

        public Movie(IMovie movieRepository, IOrder orderRepository)
        {
            _movieRepository = movieRepository;
            _orderRepository = orderRepository;
        }

        public void LoadData() => _movieRepository.Init();

        public List<Models.Movie> FindAll()
        {
            return _movieRepository.FindAll();
        }

        public Dictionary<string, int> Top3Movies()
        {
            var movieTitleList = new List<string>();
            Dictionary<string, int> movieDictionary = new Dictionary<string, int>();
            foreach (var movie in _movieRepository.FindAll())
            {
                movieTitleList.Add(movie.Title);
            }

            foreach (var order in _orderRepository.FindAll())
            {
                foreach (var ticket in order.TicketList)
                {
                    if (!movieDictionary.ContainsKey(ticket.Screening.Movie.Title))
                    {
                        movieDictionary.Add(ticket.Screening.Movie.Title, 1);
                    }
                    else
                    {
                        int idx = 0;
                        movieDictionary.TryGetValue(ticket.Screening.Movie.Title, out idx);
                        movieDictionary.Remove(ticket.Screening.Movie.Title);
                        movieDictionary.Add(ticket.Screening.Movie.Title, idx + 1);
                    }
                }
            }

            List<KeyValuePair<string, int>> lst = new List<KeyValuePair<string, int>>(movieDictionary);
            lst.Sort((kvp1, kvp2) => kvp2.Value.CompareTo(kvp1.Value));
            movieDictionary.Clear();
            if (lst.Count < 3)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    movieDictionary.Add(lst[i].Key, lst[i].Value);
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    movieDictionary.Add(lst[i].Key, lst[i].Value);
                }
            }
            return movieDictionary;
        }
    }
}