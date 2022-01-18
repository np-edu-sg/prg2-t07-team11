namespace Cli.Display
{
    public abstract class Screen
    {
        private readonly IDisplay _display;

        public Screen(IDisplay display) => _display = display;
        // display provids render helper functions

        public virtual void Render()
        {
            // _display.newscreen or something
        }
    }
}