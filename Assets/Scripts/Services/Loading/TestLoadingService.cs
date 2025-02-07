namespace UMeGames
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Core.Messages;
    using Core.MessageSender;
    using Core.Services;
    using Core.Views;

    public class TestLoadingService : IService, IMessageReceiver
    {
        private readonly Type[] subscribedMessages =
        {
            typeof(BootMessages)
        };
        
        public List<Type> Dependencies => null;
        
        public IEnumerator Initialize()
        {
            MessageSender.Register(this, subscribedMessages);
            yield break;
        }

        public void Dispose()
        {
            MessageSender.Unregister(this, subscribedMessages);
        }

        public void ReceiveMessage(object message, object[] data)
        {
            if (message is BootMessages bootMessage)
            {
                switch (bootMessage)
                {
                    case BootMessages.InitializationComplete:
                        ViewManager.Instance.OpenView<TestView>();
                        break;
                }
            }
        }
    }
}