namespace UMeGames.Core.Saves
{
    public abstract class BaseSaveComponent
    {
        public bool IsDirty { get; protected set; }

        public abstract void Initialize(string saveFolderPath);
        public abstract void Save();
    }
}