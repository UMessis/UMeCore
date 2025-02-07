namespace UMeGames.Core.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UMeGames.Core.Logger;

    public class ServiceInitializer
    {
        private readonly List<ServiceInstance> serviceInstances = new();

        public IEnumerator InitializeServices()
        {
            GetAllServices();
            SetAllServicesInServiceHub();

            foreach (ServiceInstance service in serviceInstances)
            {
                yield return InitializeService(service);
            }
        }

        private IEnumerator InitializeService(ServiceInstance serviceInstance)
        {
            if (serviceInstance.Initialized)
            {
                yield break;
            }

            if (serviceInstance.Service.Dependencies != null)
            {
                foreach (Type dependency in serviceInstance.Service.Dependencies)
                {
                    yield return InitializeService(serviceInstances.Find(x => x.Service.GetType() == dependency));
                }
            }

            List<IService> dependencies = new();
            if (serviceInstance.Service.Dependencies != null)
            {
                foreach (Type dependency in serviceInstance.Service.Dependencies)
                {
                    dependencies.Add(serviceInstances.Find(x => x.Service.GetType() == dependency).Service);
                }
            }
            
            yield return serviceInstance.Service.Initialize(dependencies);
            serviceInstance.SetInitialized();
            this.Log($"Service <color=#{Logger.GetColorHexFromString(serviceInstance.Service.GetType().Name)}>[{serviceInstance.Service.GetType().Name}]</color> has been initialized");
        }

        private void GetAllServices()
        {
            foreach (Type type in ReflectionUtils.GetAllTypesWithInterface<IService>())
            {
                IService serviceInstance = (IService)Activator.CreateInstance(type);
                serviceInstances.Add(new ServiceInstance(serviceInstance));
            }
        }

        private void SetAllServicesInServiceHub()
        {
            List<IService> services = new();
            foreach (ServiceInstance service in serviceInstances)
            {
                services.Add(service.Service);
            }
            ServiceHub.SetServiceInstances(services);
        }
        
        private class ServiceInstance
        {
            private bool initialized;

            public IService Service { get; }
            public bool Initialized => initialized;

            public ServiceInstance(IService service)
            {
                Service = service;
                initialized = false;
            }

            public void SetInitialized()
            {
                initialized = true;
            }
        }
    }
}