using Assets.Scripts.DesignPatterns.StatePattern;

namespace ww.Portfolio.DesignPatterns.StatePattern
{
    internal class StudyState : IState
    {
        StudentController student;

        public StudyState(StudentController student)
        {
            this.student = student;
        }

        public void EnterState()
        {
            // Student opens the book logic
        }

        public void Execute()
        {
            // Student studies logic
            // e.g. study anim & study meter increases & energy meter decreases & fun meter increases
        }

        public void ExitState()
        {
            // Student closes the book logic
        }
    }
}
