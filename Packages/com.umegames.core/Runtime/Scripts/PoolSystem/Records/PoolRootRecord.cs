namespace UMeGames.Core.Pool
{
    using System.Collections.Generic;
    using Records;
    using UnityEngine;

    public class PoolRootRecord : RootRecord
    {
        [SerializeField] private List<PoolContext> poolContexts = new();

        public List<PoolContext> PoolContexts => poolContexts;

        public override void Initialize()
        {
        }
    }
}