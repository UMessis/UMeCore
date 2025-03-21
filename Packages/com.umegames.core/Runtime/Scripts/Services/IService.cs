namespace UMeGames.Core.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IService : IDisposable
    {
        List<Type> Dependencies { get; }

        IEnumerator Initialize(List<IService> dependencies);
        abstract void IDisposable.Dispose();
    }
}