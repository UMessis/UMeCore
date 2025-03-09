namespace UMeGames
{
    using Core.Records;
    using UnityEngine;

    public class TestRootRecord : RootRecord
    {
        [SerializeField] private int test;

        public override void Initialize()
        {
        }
    }
}