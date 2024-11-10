namespace UMeGames
{
    public interface IInputDispatcher
    {
        public void OnActionStarted(BaseInputEvent inputEvent);
        public void OnActionPerformed(BaseInputEvent inputEvent);
        public void OnActionCanceled(BaseInputEvent inputEvent);
    }
}