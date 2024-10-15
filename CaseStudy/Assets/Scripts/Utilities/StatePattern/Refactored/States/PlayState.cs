using Assets.Scripts.DesignPatterns.StatePattern;
using Unity.VisualScripting;

namespace ww.Portfolio.DesignPatterns.StatePattern
{
    internal class PlayState : IState
    {
        private StudentController student;

        //constructor to pass studentcontroller init
        public PlayState(StudentController student)
        {
            this.student = student;
        }

        public void EnterState()
        {
            // Student picks up the phone
        }

        public void Execute()
        {
            // Student plays with phone
            // e.g. playing w/ phone anim & study meter decreases & energy meter decreases & fun meter increases
        }

        public void ExitState()
        {
            // Student puts down the phone logic
        }
    }
}
