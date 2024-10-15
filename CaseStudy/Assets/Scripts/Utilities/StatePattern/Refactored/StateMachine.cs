using Assets.Scripts.DesignPatterns.StatePattern;
using System;

namespace ww.Portfolio.DesignPatterns.StatePattern
{
    [Serializable]
    internal class StateMachine
    {
        public IState CurrentState { get; private set; }

        public PlayState playState;
        public SleepState sleepState;
        public StudyState studyState;

        public event Action<IState> OnStateChange;

        public StateMachine(StudentController student)
        {
            playState = new PlayState(student);
            sleepState = new SleepState(student);
            studyState = new StudyState(student);
        }

        public void Initialize(IState state)
        {
            CurrentState = state;
            state.EnterState();
        }

        public void TransitionToState(IState nextState)
        {
            CurrentState.ExitState();
            CurrentState = nextState;

            nextState.EnterState();

            OnStateChange?.Invoke(nextState);
        }

        public void Execute()
        {
            if(CurrentState != null)
                CurrentState.Execute();
        }



    }
}
