using System;
using UnityEditor;
using UnityEngine;

namespace ww.Portfolio.DesignPatterns.ObjectPooling.ManualPool
{
    internal class PooledObject : MonoBehaviour
    {
        private ObjectPool objectPool;
        public ObjectPool ObjectPool { get => objectPool; set => objectPool = value; }

        public void Release()
        {
            objectPool.ReleasePool(this);
        }
    }
}
