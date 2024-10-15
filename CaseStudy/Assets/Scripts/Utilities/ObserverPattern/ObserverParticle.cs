using System;
using UnityEngine;
namespace ww.Portfolio.DesignPatterns.ObserverPattern
{
    internal class ObserverParticle : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.OnPlayerShoot += CreateParticle;
        }

        private void OnDisable()
        {
            EventManager.OnPlayerShoot -= CreateParticle;
        }

        private void CreateParticle()
        {
            //Create particle logic
            Debug.Log("Particle Created");
        }
    }
}
