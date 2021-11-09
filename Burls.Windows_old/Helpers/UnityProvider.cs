using System;
using Unity;

namespace Burls.Windows.Helpers
{
    class UnityProvider : IServiceProvider
    {
        private IUnityContainer _container { get; }

        public UnityProvider(IUnityContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }
    }
}
