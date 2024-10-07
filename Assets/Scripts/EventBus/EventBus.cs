using System;
using System.Collections;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    private static EventBus instance;

    public static EventBus Instance
    {
        get { return instance; }
    }

    Hashtable eventHash = new Hashtable();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        if (eventHash == null) eventHash = new Hashtable();
    }

    public static void RegisterEvent<T>(EventName eventName, Action<T> eventAction)
    {
        Action<T> thisEvent = null;
        //  string e = GetKey<T>(eventName);
        if (instance.eventHash.ContainsKey(eventName))
        {
            thisEvent = (Action<T>)instance.eventHash[eventName];
            thisEvent += eventAction;
            instance.eventHash[eventName] = thisEvent;
        }
        else
        {
            thisEvent += eventAction;
            instance.eventHash.Add(eventName,eventAction);
        }
    }
    public static void RegisterEvent(EventName eventName, Action eventAction)
    {
        Action thisEvent = null;
        //  string e = GetKey<T>(eventName);
        if (instance.eventHash.ContainsKey(eventName))
        {
            thisEvent = (Action)instance.eventHash[eventName];
            thisEvent += eventAction;
            instance.eventHash[eventName] = thisEvent;
        }
        else
        {
            thisEvent += eventAction;
            instance.eventHash.Add(eventName,eventAction);
        }
    }
    public static void UnregisterEvent<T>(EventName eventName, Action<T> eventAction)
    {
        if (instance == null) return;
        Action<T> thisEvent = null;
        //  string e = GetKey<T>(eventName);
        if (instance.eventHash.ContainsKey(eventName))
        {
            thisEvent = (Action<T>)instance.eventHash[eventName];
            thisEvent -= (eventAction);
            instance.eventHash[eventName] = thisEvent;
        }
    }
    
    public static void UnregisterEvent(EventName eventName, Action eventAction)
    {
        if (instance == null) return;
        Action thisEvent = null;
        //  string e = GetKey<T>(eventName);
        if (instance.eventHash.ContainsKey(eventName))
        {
            thisEvent = (Action)instance.eventHash[eventName];
            thisEvent -= (eventAction);
            instance.eventHash[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent<T>(EventName eventName, T val)
    {
        Action<T> thisEvent = null;
        if (instance.eventHash.ContainsKey(eventName))
        {
            thisEvent = (Action<T>)instance.eventHash[eventName];
            thisEvent?.Invoke(val);
        }
    }
    public static void TriggerEvent(EventName eventName)
    {
        Action thisEvent = null;
        if (instance.eventHash.ContainsKey(eventName))
        {
            thisEvent = (Action)instance.eventHash[eventName];
            thisEvent?.Invoke();
        }
    }
    private static string GetKey<T>(EventName eventName)
    {
        Type type = typeof(T);
        string key = type.ToString() + eventName.ToString();
        return key;
    }
}

public enum EventName
{
    SelectedBuildingEvent,
    CreateUnitEvent,
    OnClicked,
    OnExit,
    StartPlacement,
    ObjectPlacer
    
}