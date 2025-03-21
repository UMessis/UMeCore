namespace UMeGames.Core.Services
{
    using System.Collections.Generic;
    using UMeGames.Core.Logger;

    public static class ServiceHub
    {
        private static List<IService> serviceInstances;

        public static void SetServiceInstances(List<IService> services)
        {
            serviceInstances = services;
        }

        public static T GetService<T>() where T : class
        {
            foreach (IService service in serviceInstances)
            {
                if (service.GetType() == typeof(T))
                {
                    return service as T;
                }
            }

            Logger.LogError(typeof(ServiceHub), $"Failed to get service of type {typeof(T).Name}");
            return null;
        }
    }
}