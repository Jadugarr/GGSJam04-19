using System.Collections.Generic;
using GameEvents;
using UnityEngine;

public enum GameEvent
{
    Death,
    UseItem,
    AddHazard,

}
//send parameters when event gets evoked
// invoike each event
//map event types to listeners
public delegate void GameEventListener(IGameEvent eventParameters);

public static class EventManager
{

    private static Dictionary<GameEvent, List<GameEventListener>> GameEventDictionary = new Dictionary<GameEvent, List<GameEventListener>>();

    public static void AddListener(GameEvent gameEvent, GameEventListener gameEventListener)
    {
        List<GameEventListener> listeners;
        bool key = GameEventDictionary.TryGetValue(gameEvent, out listeners);
        if (key)
        {
            listeners.Add(gameEventListener);
        }
        else
        {
            GameEventDictionary.Add(gameEvent, new List<GameEventListener>() { gameEventListener });
        }

    }

    public static void RemoveListener(GameEvent gameEvent, GameEventListener gameEventListener)
    {
        List<GameEventListener> listeners;
        bool key = GameEventDictionary.TryGetValue(gameEvent, out listeners);
        if (key)
        {
            listeners.Remove(gameEventListener);
        }
    }

    public static void CallEvent(GameEvent gameEvent, IGameEvent eventParameters)
    {
        List<GameEventListener> listeners;
        bool key = GameEventDictionary.TryGetValue(gameEvent, out listeners);
        if (key)
        {
            foreach (GameEventListener item in listeners)
            {
                item(eventParameters);
            }
        }

    }
}
