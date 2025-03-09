namespace UMeGames.Core.Saves
{
    using Records;
    using UnityEngine;

    public class SaveSystemRootRecord : RootRecord
    {
        [SerializeField] private float saveInterval = 10f;
        
        public float SaveInterval => saveInterval;

        public override void Initialize()
        {
        }
    }
}