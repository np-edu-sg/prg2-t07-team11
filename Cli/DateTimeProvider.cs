using System;
using Core;

namespace Cli
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now() => DateTime.Now;
    }
}