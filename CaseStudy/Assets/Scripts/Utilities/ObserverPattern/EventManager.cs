using System;
using UnityEngine;

namespace ww.Portfolio.DesignPatterns.ObserverPattern
{
    public static class EventManager
    {
        public static event Action OnPlayerShoot;
        public static event Action OnPlayerDeath;
        public static event Action OnEnemyHit;
        public static event Action OnGameOver;

        public static void PlayerShoot()
        {
            OnPlayerShoot?.Invoke();
        }
      
        public static void PlayerDeath()
        {
            OnPlayerDeath?.Invoke();
        }

        public static void EnemyHit()
        {
            OnEnemyHit?.Invoke();
        }

        public static void GameOver()
        {
            OnGameOver?.Invoke();
        }

        public static void CharacterCreated()
        {
            // just to showcase static classes and their static methods from further scripting examples
            Debug.Log("Character Created");
        }
    }
}
