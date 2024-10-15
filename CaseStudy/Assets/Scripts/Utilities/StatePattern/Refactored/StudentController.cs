using System;
using UnityEngine;
using ww.Portfolio.DesignPatterns.StatePattern;

namespace Assets.Scripts.DesignPatterns.StatePattern
{
    internal class StudentController : MonoBehaviour
    {
        private StateMachine studentStateMachine;
        public StateMachine StudentStateMachine => studentStateMachine; // or { get  => studentStateMachine; }

        private void Awake()
        {
            studentStateMachine = new StateMachine(this);
        }

        private void Start()
        {
            studentStateMachine.Initialize(studentStateMachine.studyState);
        }

        private void Update()
        {
            studentStateMachine.CurrentState.Execute();
        }


    }
}
