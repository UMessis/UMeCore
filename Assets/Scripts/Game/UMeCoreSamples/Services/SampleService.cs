namespace UMeGames.Game.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UMeGames.Core.Logger;
    using UMeGames.Core.MessageSender;
    using UMeGames.Core.Records;
    using UMeGames.Core.Services;
    using UMeGames.Game.Messages;
    using UMeGames.Game.Records;

    public class SampleService : IService, IMessageReceiver
    {
        public List<Type> Dependencies => null;

        public IEnumerator Initialize()
        {
            MessageSender.Register(this, typeof(MessageType));
            foreach (var record in RecordHub.GetRecordsOfType<SampleRecord>())
            {
                this.Log($"Sample record {record.Id} has test value of {record.Test}");
            }
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