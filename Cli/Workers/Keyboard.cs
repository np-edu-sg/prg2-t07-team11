using System;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace Cli.Workers
{
    public class Keyboard
    {
        private Stream _stdInStream;
        private readonly IHost _host;

        public Keyboard(IHost host)
        {
            _host = host;

            Console.OpenStandardInput();
        }
    }
}