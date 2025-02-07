namespace UMeGames.Core.Records
{
    using System;
    using UnityEngine;

    [Serializable]
    public class SubRecord : ScriptableObject
    {
        private Guid id = Guid.NewGuid();
        public Guid Id => id;
    }
}