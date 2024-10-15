namespace UMeGames.Game.Records
{
    using System;
    using System.Collections.Generic;
    using UMeGames.Core.Records;
    using UnityEngine;

    [Serializable]
    public class SampleRecord : Record
    {
        [Header("Data")]
        [SerializeField, Range(0, 100)] private float test;
        [SerializeField] private List<string> names = new();

        public float Test => test;
        public List<string> Names => names;
    }
}