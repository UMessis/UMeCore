namespace UMeGames.Core.Pool
{
    using System.Collections.Generic;
    using Records;
    using UnityEngine;

    public class PoolContext : SubRecord
    {
        [SerializeField] private string poolContextName; // TODO : Make this an enum or something
        [SerializeField] private List<PoolEntry> poolEntries = new();

        public string PoolContextName => poolContextName;
        public List<PoolEntry> PoolEntries => poolEntries;
    }
}