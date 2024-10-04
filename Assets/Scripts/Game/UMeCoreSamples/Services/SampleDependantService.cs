namespace UMeGames.Game.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UMeGames.Core.MessageSender;
    using UMeGames.Core.Services;
    using UMeGames.Game.Messages;

    public class SampleDependantService : IService
    {
        public List<Type> Dependencies => new()
        {
            typeof(SampleService)
        };

        public IEnumerator Initialize()
        {
            MessageSender.Send(MessageType.SampleMessage, new object[] { "This is a test message" });
            yield break;
        }

        public void Dispose()
        {
        }
    }
}