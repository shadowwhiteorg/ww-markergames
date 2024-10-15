using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ww.Portfolio.DesignPatterns.ObjectPooling.UnityPool
{
    internal class Particle : MonoBehaviour, IPooledObject<Particle>
    {
        [SerializeField] float deactivationDelay;
        private IObjectPool<Particle> objectPool;
        public IObjectPool<Particle> ObjectPool { set => objectPool = value; }

        private IEnumerator DeactivateCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            objectPool.Release(this);
        }

        public void Deactivate()
        {
            StartCoroutine(DeactivateCoroutine(deactivationDelay));
        }


    }
}
