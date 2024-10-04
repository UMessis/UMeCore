namespace UMeGames.Game.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UMeGames.Core.Logger;
    using UMeGames.Core.MessageSender;
    using UMeGames.Core.Services;
    using UMeGames.Game.Messages;

    public class SampleService : IService, IMessageReceiver
    {
        public List<Type> Dependencies => null;

        public IEnumerator Initialize()
        {
            MessageSender.Register(this, typeof(MessageType));
            yield break;
        }

        public void Dispose()
        {
            MessageSender.Unregister(this, typeof(MessageType));
        }

        public void ReceiveMessage(object message, object[] data)
        {
            switch (message)
            {
                case MessageType messageType:
                    switch (messageType)
                    {
                        case MessageType.SampleMessage:
                            this.Log($"Received message with data [{(string)data[0]}]");
                            break;
                    }
                    break;
            }
        }
    }
}