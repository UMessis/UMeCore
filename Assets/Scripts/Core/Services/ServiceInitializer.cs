namespace UMeGames.Core.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UMeGames.Core.Logger;

    public class ServiceInitializer
    {
        class ServiceInstance
        {
            IService service;
            bool initialized;

            public IService Service => service;
            public bool Initialized => initialized;

            public ServiceInstance(IService service)
            {
                this.service = service;
                initialized = false;
            }

            public void SetInitialized()
            {
                initialized = true;
            }
        }

        List<ServiceInstance> serviceInstances = new();

        public IEnumerator InitializeServices()
        {
            GetAllServices();
            SetAllServicesInServiceHub();

            foreach (var service in serviceInstances)
            {
                yield return InitializeService(service);
            }
        }

        IEnumerator InitializeService(ServiceInstance serviceInstance)
        {
            if (serviceInstance.Initialized)
            {
                yield break;
            }

            if (serviceInstance.Service.Dependencies != null)
            {
                foreach (var dependency in serviceInstance.Service.Dependencies)
                {
                    yield return InitializeService(serviceInstances.Find(x => x.Service.GetType() == dependency));
                }
            }

            yield return serviceInstance.Service.Initialize();
            serviceInstance.SetInitialized();
            this.Log($"Service <color=#{Logger.GetColorHexFromString(serviceInstance.Service.GetType().Name)}>[{serviceInstance.Service.GetType().Name}]</color> has been initialized");
        }

        void GetAllServices()
        {
            foreach (var type in ReflectionUtils.GetAllTypesWithInterface<IService>())
            {
                IService serviceInstance = (IService)Activator.CreateInstance(type);
                serviceInstances.Add(new ServiceInstance(serviceInstance));
            }
        }

        void SetAllServicesInServiceHub()
        {
            var services = new List<IService>();
            foreach (var service in serviceInstances)
            {
                services.Add(service.Service);
            }
            ServiceHub.SetServiceInstances(services);
        }
    }
}