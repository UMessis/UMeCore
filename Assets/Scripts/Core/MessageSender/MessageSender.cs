namespace UMeGames.Core.MessageSender
{
    using System;
    using System.Collections.Generic;

    public static class MessageSender
    {
        static Dictionary<IMessageReceiver, HashSet<Type>> registeredListeners = new();

        public static void Register(IMessageReceiver source, params Type[] types)
        {
            Register(source, new HashSet<Type>(types));
        }

        public static void Register(IMessageReceiver source, HashSet<Type> messageTypes)
        {
            if (messageTypes == null || messageTypes.Count == 0)
            {
                return;
            }

            if (!registeredListeners.ContainsKey(source))
            {
                registeredListeners.Add(source, messageTypes);
            }
            else
            {
                foreach (Type type in messageTypes)
                {
                    registeredListeners[source].Add(type);
                }
            }
        }

        public static void Unregister(IMessageReceiver source, params Type[] types)
        {
            Unregister(source, new HashSet<Type>(types));
        }

        public static void Unregister(IMessageReceiver source, HashSet<Type> messageTypes)
        {
            if (!registeredListeners.ContainsKey(source))
            {
                return;
            }

            foreach (Type type in messageTypes)
            {
                registeredListeners[source].Remove(type);
            }

            if (registeredListeners[source].Count == 0)
            {
                registeredListeners.Remove(source);
            }
        }

        public static void UnregisterFromAll(IMessageReceiver source)
        {
            if (!registeredListeners.ContainsKey(source))
            {
                return;
            }

            registeredListeners.Remove(source);
        }

        public static void Send(object message, object[] data = null)
        {
            if (registeredListeners.Count == 0)
            {
                return;
            }

            foreach ((IMessageReceiver listener, HashSet<Type> types) in registeredListeners)
            {
                foreach (Type type in types)
                {
                    if (type == message.GetType())
                    {
                        listener.ReceiveMessage(message, data);
                        continue;
                    }
                }
            }
        }
    }
}