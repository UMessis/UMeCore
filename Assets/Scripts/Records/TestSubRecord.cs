namespace UMeGames
{
    using System;
    using Core.Attributes;
    using UMeGames.Core.Records;
    using UnityEngine;

    public class TestSubRecord : SubRecord
    {
        [SerializeField, EnumString(typeof(TestEnum))] private string test;

        [Serializable]
        private enum TestEnum
        {
            Test1,
            Test2,
            Test3
        }
    }
}