using System;
using System.Collections.Generic;

namespace CodeBase.Utils.Disposables
{
    public class CompositeDisposables : IDisposable
    {
        private readonly List<IDisposable> disposables = new List<IDisposable>();

        public void Retain(IDisposable disposable)
        {
            disposables.Add(disposable);
        }

        public void Dispose()
        {
            foreach(var disposable in disposables)
            {
                disposable.Dispose();
            }
            disposables.Clear();
        }
    }
}
