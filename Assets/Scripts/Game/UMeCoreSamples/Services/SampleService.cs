namespace UMeGames.Game.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UMeGames.Core.Services;

    public class SampleService : IService
    {
        public List<Type> Dependencies => null;

        public IEnumerator Initialize()
        {
            yield break;
        }

        public void Dispose()
        {
        }
    }
}