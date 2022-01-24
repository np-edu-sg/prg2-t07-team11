using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Cli.Display
{
    public class ResizeEventArgs
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public ResizeEventArgs(int width, int height) => (Width, Height) = (width, height);
    }

    public class Window : BackgroundService
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public event ResizeEventHandler ResizeEvent;

        public delegate void ResizeEventHandler(object sender, ResizeEventArgs args);

        public Window() => ResizeEvent += Handler;

        private void Handler(object sender, ResizeEventArgs args)
        {
            Width = args.Width;
            Height = args.Height;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(100), stoppingToken);

                if (Width == Console.WindowWidth && Height == Console.WindowHeight) continue;
                ResizeEvent?.Invoke(this, new ResizeEventArgs(Console.WindowWidth, Console.WindowHeight));
            }
        }
    }
}