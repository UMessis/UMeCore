namespace UMeGames
{
    using System;
    using System.Collections.Generic;

    public static class ReflectionUtils
    {
        public static List<Type> GetAllTypesWithInterface<T>()
        {
            var types = new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (typeof(T).IsAssignableFrom(type))
                    {
                        foreach (var typeInterface in type.GetInterfaces())
                        {
                            if (typeInterface == typeof(T))
                            {
                                types.Add(type);
                            }
                        }
                    }
                }
            }
            return types;
        }

        public static List<Type> GetAllTypesWithBaseClass<T>()
        {
            var types = new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(T)) && !type.IsAbstract)
                    {
                        types.Add(type);
                    }
                }
            }
            return types;
        }
    }
}