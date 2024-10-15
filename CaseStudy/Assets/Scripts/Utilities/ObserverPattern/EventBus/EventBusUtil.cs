using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class EventBusUtil
{
    public static IReadOnlyList<Type> EventTypes { get; set; }
    public static IReadOnlyList<Type> EventBusType { get; set; }

#if UNITY_EDITOR
    public static PlayModeStateChange PlayModeState { get; set; }

    [InitializeOnLoadMethod]
    public static void IntializeEditor()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange change)
    {
        PlayModeState = change;
        if(change == PlayModeStateChange.ExitingPlayMode)
        {
            ClearAllBusses();
        }
    }

#endif

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        EventTypes = PredefinedAsseblyUtil.GetTypes(typeof(IEvent));
        EventBusType = InitializeAllBusses();
    }

    private static List<Type> InitializeAllBusses()
    {
        List<Type> eventBusTypes = new List<Type>();

        var typedef = typeof(EventBus<>);

        foreach (var eventType in eventBusTypes)
        {
            var busType = typedef.MakeGenericType(eventType);
            eventBusTypes.Add(busType);
            Debug.Log($"EventBus<{eventType.Name}> initialized");
        }

        return eventBusTypes;
    }

    public static void ClearAllBusses()
    {
        Debug.Log("Clearing all busses");
        foreach (var busType in EventBusType)
        {
            var method = busType.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            method.Invoke(null, null);
        }
    }
}