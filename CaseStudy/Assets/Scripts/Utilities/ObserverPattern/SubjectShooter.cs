using System;
using UnityEngine;

namespace ww.Portfolio.DesignPatterns.ObserverPattern
{
    internal class SubjectShooter : MonoBehaviour
    {

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PressSpaceKey();
            }
        }

        private void PressSpaceKey()
        {
            EventManager.PlayerShoot();
        }
    }
}
