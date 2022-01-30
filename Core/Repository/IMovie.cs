//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System.Collections.Generic;
using Core.Models;

namespace Core.Repository
{
    public interface IMovieReader
    {
        public void Init();
        public List<Movie> FindAll();
        public Movie FindOneByTitle(string movieTitle);
    }

    public interface IMovieWriter
    {
    }

    public interface IMovie : IMovieReader, IMovieWriter
    {
    }
}