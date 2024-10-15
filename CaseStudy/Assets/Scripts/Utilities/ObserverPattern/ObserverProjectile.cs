using System;
using UnityEngine;

namespace ww.Portfolio.DesignPatterns.ObserverPattern
{
    internal class ObserverProjectile : MonoBehaviour
    {

        private void OnEnable()
        {
            EventManager.OnPlayerShoot += SendProjectile;
        }

        private void OnDisable()
        {
            EventManager.OnPlayerShoot -= SendProjectile;
        }

        private void SendProjectile()
        {
            //Send Projectile Logic
            Debug.Log("ProjectileCreated!");
        }
    }
}
