using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Manager")]
public class EventManagerSO : ScriptableObject
{
    public event Action<string, string> OnNotificationReceived;
    public event Action OnMessageUpdated;

    public void TriggerNotification(string title, string content)
    {
        OnNotificationReceived?.Invoke(title, content);
        OnMessageUpdated?.Invoke();
    }
}
