using System;
using System.Collections;
using UnityEngine;

namespace ww.Portfolio.DesignPatterns.ObjectPooling.ManualPool
{
    [RequireComponent(typeof(PooledObject))]
    [RequireComponent(typeof(Rigidbody))]
    internal class Projectile : MonoBehaviour
    {
        [SerializeField] float deactivationDelay;

        private PooledObject pooledObject;
        private Rigidbody rb;


        private void Awake()
        {
            pooledObject = GetComponent<PooledObject>();
            rb = GetComponent<Rigidbody>();
        }

        private IEnumerator DeactivationCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            pooledObject.Release();
        }

        public void Deactivate()
        {
            StartCoroutine(DeactivationCoroutine(deactivationDelay));
        }

        public void Launch(float force, Vector3 direction)
        {
            rb.AddForce(force * direction, ForceMode.Impulse);
        }
    }
}
