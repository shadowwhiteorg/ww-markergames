using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ww.Portfolio.DesignPatterns.ObjectPooling.UnityPool
{
    internal class ParticleSpawner : MonoBehaviour
    {
        [SerializeField] IPooledObject<Particle> particlePrefab;
        [SerializeField] ObjectPool<Particle> objectPool;
        public void CreateParticle(Transform muzzleTransform)
        {
            IPooledObject<Particle> particleInstance = objectPool.SharedObjectPool.Get();

            if (particleInstance is MonoBehaviour particle)
                particle.transform.SetPositionAndRotation(muzzleTransform.position, muzzleTransform.rotation);

        }

    }
}
