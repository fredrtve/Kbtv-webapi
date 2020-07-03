using System;
using System.Collections.Generic;

namespace CleanArchitecture.SharedKernel
{
    public class DisposableList<T> : List<T>, IDisposable where T : IDisposable
    {
        public void Dispose()
        {
            foreach (T obj in this)
            {
                obj.Dispose();
            }
        }
    }
}
