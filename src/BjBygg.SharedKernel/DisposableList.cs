﻿using System;
using System.Collections.Generic;

namespace BjBygg.SharedKernel
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
