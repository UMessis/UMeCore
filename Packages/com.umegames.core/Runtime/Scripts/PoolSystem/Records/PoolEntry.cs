namespace UMeGames.Core.Pool
{
    using System;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    [Serializable]
    public struct PoolEntry
    {
        [SerializeField] private AssetReference poolItem;
        [SerializeField] private int amount;
        
        public AssetReference PoolItem => poolItem;
        public int Amount => amount;
    }
}