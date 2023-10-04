namespace Platformio.DI
{
    /// <summary>
    /// Generic interface to provide some value where Extenject doesn't fit.
    /// </summary>
    /// <typeparam name="T">The type of an object to provide</typeparam>
    internal interface IProvider<out T>
    {
        T GetCurrentValue();
    }
}