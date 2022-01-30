//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System;

namespace Core
{
    /// <summary>
    ///     IDateTimeProvider is used so that DateTime.Now can be mocked for testing
    /// </summary>
    public interface IDateTimeProvider
    {
        public DateTime Now();
    }
}