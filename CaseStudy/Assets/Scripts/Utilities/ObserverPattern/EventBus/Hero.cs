using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Hero : MonoBehaviour
{
    int health;
    int mana;

    EventBinding<TestEvent> testEventBinding;
    EventBinding<PlayerEvent> playerEventBinding;

    private void Awake()
    {
        health = 100;
        mana = 10;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            EventBus<TestEvent>.Raise(new TestEvent());
        }
        if(Input.GetKeyUp(KeyCode.P))
        {
            EventBus<PlayerEvent>.Raise(new PlayerEvent 
            { 
                health = health, 
                mana = mana 
            });
        }
    }

    private void OnEnable()
    {
        testEventBinding = new EventBinding<TestEvent>(HandleTestEvent);
        EventBus<TestEvent>.Register(testEventBinding);

        playerEventBinding = new EventBinding<PlayerEvent>(HandlePlayerEvent);
        EventBus<PlayerEvent>.Register(playerEventBinding);
    }

    private void OnDisable()
    {
        EventBus<TestEvent>.Deregister(testEventBinding);
        EventBus<PlayerEvent>.Deregister(playerEventBinding);
    }
    void HandleTestEvent()
    {
        Debug.Log("Test event raised!");
    }

    void HandlePlayerEvent(PlayerEvent playerEvent )
    {
        Debug.Log($"Player event raised! Health: {playerEvent.health}, Mana: {playerEvent.mana}");
    }

}
