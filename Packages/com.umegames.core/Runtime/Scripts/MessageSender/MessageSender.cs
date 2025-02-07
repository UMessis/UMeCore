namespace UMeGames.Core.MessageSender
{
    using System;
    using System.Collections.Generic;

    public static class MessageSender
    {
        private static readonly Dictionary<IMessageReceiver, HashSet<Type>> registeredListeners = new();

        public static void Register(IMessageReceiver source, params Type[] types)
        {
            if (types == null || types.Length == 0) { return; }

            if (registeredListeners.TryGetValue(source, out HashSet<Type> listerTypes))
            {
                foreach (Type type in types)
                {
                    listerTypes.Add(type);
                }
            }
            else
            {
                registeredListeners.Add(source, new(types));
            }
        }

        public static void Unregister(IMessageReceiver source, params Type[] types)
        {
            if (!registeredListeners.TryGetValue(source, out HashSet<Type> listenerTypes)) { return; }

            foreach (Type type in types)
            {
                listenerTypes.Remove(type);
            }

            if (registeredListeners[source].Count == 0)
            {
                registeredListeners.Remove(source);
            }
        }

        public static void UnregisterFromAll(IMessageReceiver source)
        {
            if (registeredListeners.ContainsKey(source))
            {
                registeredListeners.Remove(source);
            }
        }

        public static void Send(object message, params object[] data)
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
                        break;
                    }
                }
            }
        }
    }
}