using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using ClaimsReserveCalculator.ClaimsDataInputAndOutputInterfaces;
using ClaimsReserveCalculator.ClaimsDataIO;
using ClaimsReserveCalculator.ClaimsDataProcessing;
using ClaimsReserveCalculator.FrameworksAndDriversInterfaces;
using System;
using Unity;
using Unity.Lifetime;
using Unity.RegistrationByConvention;

namespace ClaimsReserveCalculator.FrameworksAndDrivers
{
    /// <summary>
    /// This is a class that encapsulates dependency injection services and
    /// abstracts some details of the specific DI framework/container used.
    /// </summary>
    public class DependencyInjector : IDependencyInjector
    {
        private static IDependencyInjector _instance;
        internal IUnityContainer _container;

        /// <summary>
        /// The single instance of the class. Follows a singleton pattern.
        /// </summary>
        public static IDependencyInjector Instance
        {
            get
            {
                _instance = _instance ?? new DependencyInjector();
                return _instance;
            }
        }

        /// <summary>
        /// Private constructor that initialises the underlying DI container.
        /// The Instance property should be used by the application.
        /// </summary>
        private DependencyInjector()
        {
            _container = new UnityContainer();
            RegisterInterfaces();
        }

        /// <summary>
        /// Registers interfaces with the container.
        /// </summary>
        public void RegisterInterfaces()
        {
            // Register those types that can't be found/matched by their naming convention.
            _container.RegisterType<IProductsClaimsDataReader, ProductsClaimsDataFileReader>();
            _container.RegisterType<IProductsClaimsDataWriter, ProductsClaimsDataFileWriter>();

            // Register those types for which single instances should be used.
            _container.RegisterType<IClaimsDataManager, ClaimsDataManager>(new ContainerControlledLifetimeManager());

            // Find and register all other types through (naming) convention.
            _container.RegisterTypes(AllClasses.FromLoadedAssemblies(), 
                WithMappings.FromMatchingInterface, WithName.Default, WithLifetime.ContainerControlled, overwriteExistingMappings: true);
        }

        /// <summary>
        /// Resolves a type using the underlying container.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <returns>Returns the resolved object if a mapping is found.</returns>
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// Resolves a type using the underlying container.
        /// </summary>
        /// <typeparam name="t">Type of the object.</typeparam>
        /// <returns>Returns the resolved object if a mapping is found.</returns>
        public object Resolve(Type t)
        {
            return _container.Resolve(t);
        }
    }
}
