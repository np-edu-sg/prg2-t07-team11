using System;

namespace Core
{
    /// <summary>
    /// IDateTimeProvider is used so that DateTime.Now can be mocked for testing
    /// </summary>
    public interface IDateTimeProvider
    {
        public DateTime Now();
    }
}