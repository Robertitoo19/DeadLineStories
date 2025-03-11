using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Manager")]
public class EventManagerSO : ScriptableObject
{
    public event Action OnNewInteractuable;
    public event Action OnNoInteractuable;

    public event Action<string, string> OnNotificationReceived;
    public event Action OnMessageUpdated;

    public void NewInteractuable()
    {
        OnNewInteractuable?.Invoke();
    }
    public void NoInteractuable()
    {
        OnNoInteractuable?.Invoke();
    }
    public void TriggerNotification(string title, string content)
    {
        OnNotificationReceived?.Invoke(title, content);
        OnMessageUpdated?.Invoke();
    }
}
