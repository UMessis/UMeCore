namespace UMeGames.Core.Records
{
    using System;
    using Attributes;
    using UnityEngine;

    [Serializable]
    public abstract class RootRecord : ScriptableObject
    {
        [SerializeField, ReadOnly] private string uniqueId = Guid.NewGuid().ToString();

        public string UniqueId => uniqueId;

        public virtual void Initialize() { }

        [Button]
        public void GenerateNewUniqueId()
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                return;
            }

            uniqueId = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        public static bool operator ==(RootRecord r1, RootRecord r2)
        {
            if (r1 == null || r2 == null)
            {
                return false;
            }

            return r1.UniqueId == r2.UniqueId;
        }

        public static bool operator !=(RootRecord r1, RootRecord r2)
        {
            if (r1 == null || r2 == null)
            {
                return true;
            }

            return r1.UniqueId != r2.UniqueId;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return ((RootRecord)obj).UniqueId == UniqueId;
        }
    }
}