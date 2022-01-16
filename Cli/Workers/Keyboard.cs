using System;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace Cli.Workers
{
    public class Keyboard
    {
        private readonly IHost _host;
        private Stream _stdInStream;

        public Keyboard(IHost host)
        {
            _host = host;

            Console.OpenStandardInput();
        }
    }
}