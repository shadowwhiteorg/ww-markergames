using Assets.Scripts.DesignPatterns.StatePattern;
using System;

namespace ww.Portfolio.DesignPatterns.StatePattern
{
    internal class SleepState
    {
        private StudentController student;
        public SleepState(StudentController student)
        {
            this.student = student;
        }

        public void EnterState()
        {
            // student rests their hands on the desk and drifts off logic :) 
        }
        public void Execute()
        {
            // student sleeps logic
        }

        public void ExitState()
        {
            // Student wakes up logic
        }
    }
}
