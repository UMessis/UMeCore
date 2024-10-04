namespace UMeGames.Core.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class ServiceInitializer
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

        static List<ServiceInstance> serviceInstances = new();

        public static IEnumerator InitializeServices()
        {
            GetAllServices();
            foreach (var service in serviceInstances)
            {
                yield return InitializeService(service);
            }
        }

        static IEnumerator InitializeService(ServiceInstance serviceInstance)
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
            Debug.Log($"Service {serviceInstance.Service.GetType().Name} has been initialized");
        }

        static void GetAllServices()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (typeof(IService).IsAssignableFrom(type))
                    {
                        foreach (var typeInterface in type.GetInterfaces())
                        {
                            if (typeInterface == typeof(IService))
                            {
                                IService serviceInstance = (IService)Activator.CreateInstance(type);
                                serviceInstances.Add(new ServiceInstance(serviceInstance));
                            }
                        }
                    }
                }
            }
        }
    }
}