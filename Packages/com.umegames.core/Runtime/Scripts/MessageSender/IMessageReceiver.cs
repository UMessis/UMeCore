namespace UMeGames.Core.MessageSender
{
    public interface IMessageReceiver
    {
        void ReceiveMessage(object message, object[] data);
    }
}