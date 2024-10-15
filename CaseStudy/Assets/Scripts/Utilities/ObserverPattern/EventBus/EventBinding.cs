using System;

public interface IEventBinding<T>
{
    public Action<T> OnEvent { get; set; }
    public Action OnEventNoArgs { get; set; }
}

public class EventBinding<T> : IEventBinding<T> where T : IEvent
{
    // Fields
    #region Long Form
    //Action<T> onEvent;
    //Action onEventNoArgs;

    //To make sure that the delegate is never null, we can assign a delegate that does nothing.
    //The underscore _ is used as a convention to indicate that the parameter is not being used in the method body.
    //Action<T> onEvent = delegate (T _) { };
    //Action onEventNoArgs = delegate () { }; 
    #endregion

    //In short form
    Action<T> onEvent = _ => { };
    Action onEventNoArgs = () => { };

    Action<T> IEventBinding<T>.OnEvent { get => onEvent; set => onEvent = value; }
    Action IEventBinding<T>.OnEventNoArgs { get => onEventNoArgs; set => onEventNoArgs = value; }

    // Constructors
    #region Long Form
    //public EventBinding(Action<T> onEvent)
    //{
    //    this.onEvent = onEvent;
    //}
    //public EventBinding(Action onEventNoArgs)
    //{
    //    this.onEventNoArgs = onEventNoArgs;
    //} 
    #endregion

    //In short form
    public EventBinding(Action<T> onEvent) => this.onEvent = onEvent;
    public EventBinding(Action onEventNoArgs) => this.onEventNoArgs = onEventNoArgs;

    // Methods
    public void Add(Action<T> action) => onEvent += action;
    public void Add(Action action) => onEventNoArgs += action;

    public void Remove(Action<T> action) => onEvent -= action;
    public void Remove(Action action) => onEventNoArgs -= action;

}