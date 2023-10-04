namespace Platformio.DI
{
    // TODO A subject to be replaced when proper DI is here
    internal interface IProvider<out T>
    {
        T GetCurrentValue();
    }
}