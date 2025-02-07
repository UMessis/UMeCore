namespace UMeGames
{
    using System;
    using Core.Saves;
    using Newtonsoft.Json;
    using UnityEngine;

    [Serializable]
    public class TestSaveComponentData : SaveComponentData
    {
        [SerializeField, JsonProperty] public float TestFloat = 3f;
    }
}