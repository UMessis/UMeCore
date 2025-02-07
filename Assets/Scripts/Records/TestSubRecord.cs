namespace UMeGames
{
    using UMeGames.Core.Records;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public class TestSubRecord : SubRecord
    {
        [SerializeField] private AssetReference test;
    }
}