namespace UMeGames
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public static class ReflectionUtils
    {
        public static List<Type> GetAllTypesWithInterface<T>()
        {
            List<Type> types = new List<Type>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(T).IsAssignableFrom(type))
                    {
                        foreach (Type typeInterface in type.GetInterfaces())
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
            List<Type> types = new();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(T)) && !type.IsAbstract)
                    {
                        types.Add(type);
                    }
                }
            }
            return types;
        }
        
        public static List<Type> GetAllTypesWithBaseClass(Type baseClass)
        {
            List<Type> types = new();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(baseClass) && !type.IsAbstract)
                    {
                        types.Add(type);
                    }
                }
            }
            return types;
        }
    }
}