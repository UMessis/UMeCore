namespace UMeGames.Core.Records
{
    using System;
    using System.Collections.Generic;
    using Attributes;
    using UnityEngine;

    public abstract class RootRecordCollection : RootRecord
    {
        [SerializeField, TypeDropdown(typeof(SubRecord))] private List<string> subRecordTypes = new();
        
        private readonly Dictionary<Type, SubRecord> subRecordCollection = new();
    }
}