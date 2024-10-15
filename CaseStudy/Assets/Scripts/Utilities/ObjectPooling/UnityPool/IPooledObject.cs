using UnityEngine;
using UnityEngine.Pool;

namespace ww.Portfolio.DesignPatterns.ObjectPooling.UnityPool
{
    internal interface IPooledObject<T> where T : MonoBehaviour
    {
        IObjectPool<T> ObjectPool { set; }

    }
}
