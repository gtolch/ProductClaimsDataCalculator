using System;

namespace ClaimsReserveCalculator.FrameworksAndDriversInterfaces
{
    /// <summary>
    /// This is the interface to the class that encapsulates the
    /// dependency injector container.
    /// </summary>
    public interface IDependencyInjector
    {
        /// <summary>
        /// Registers interfaces with the container.
        /// </summary>
        void RegisterInterfaces();

        /// <summary>
        /// Resolves a type using the underlying container.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <returns>Returns the resolved object if a mapping is found.</returns>
        T Resolve<T>();

        /// <summary>
        /// Resolves a type using the underlying container.
        /// </summary>
        /// <param name="t">Type of the object.</param>
        /// <returns>Returns the resolved object if a mapping is found.</returns>
        object Resolve(Type t);
    }
}
