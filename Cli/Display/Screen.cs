namespace Cli.Display
{
    public abstract class Screen
    {
        public readonly IDisplay Display;
        public Screen(IDisplay display) => Display = display;
        public abstract void Mount();
        public abstract void Unmount();
    }
}