namespace ww.Portfolio.DesignPatterns.StatePattern
{
    internal interface IState
    {
        public abstract void EnterState();

        public void Execute() { }

        public void ExitState() { }
    }
}
