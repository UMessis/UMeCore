namespace UMeGames.Core.Records
{
    using System;
    using UnityEngine;

    [Serializable]
    public class RecordData
    {
        [Header("Base")] // todo : versioning
        [SerializeField] private string key;
        [SerializeField] private int version = 1;
    }
}