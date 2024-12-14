namespace UMeGames.Core.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IService : IDisposable
    {
        List<Type> Dependencies { get; }

        abstract IEnumerator Initialize();
        abstract void IDisposable.Dispose();
    }
}