using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ww.Portfolio.DesignPatterns.ObjectPooling.ManualPool
{
    internal class Shooter : MonoBehaviour
    {
        [SerializeField] ObjectPool objectPool;
        [SerializeField] Transform muzzleTransform;
        [SerializeField] float shootingForce;

        private Vector3 shootingDirection;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            PooledObject pooledProjectile = objectPool.GetFromPool();
            pooledProjectile.transform.position = muzzleTransform.position;
            shootingDirection = this.transform.position - muzzleTransform.position;
            pooledProjectile.GetComponent<Projectile>().Launch(shootingForce, shootingDirection);
            pooledProjectile.GetComponent<Projectile>().Deactivate();
        }
    }
}
