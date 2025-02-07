namespace UMeGames
{
    using System;
    using Core.Saves;
    using Newtonsoft.Json;
    using UnityEngine;

    [Serializable]
    public class TestSaveComponentData : SaveComponentData
    {
        public float testFloat = 3f;
    }
}