namespace UMeGames.Core.MessageSender
{
    public interface IMessageReceiver
    {
        abstract void ReceiveMessage(object message, object[] data);
    }
}