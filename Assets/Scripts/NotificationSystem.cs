using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationSystem : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private GameObject notificationPanel;
    [SerializeField] private TMP_Text notificationText;

    private string savedTitle;
    private string savedContent;

    private void OnEnable()
    {
        eventManager.OnNotificationReceived += ShowNotification;
    }
    private void OnDisable()
    {
        eventManager.OnNotificationReceived -= ShowNotification;
    }

    private void ShowNotification(string title, string content)
    {
        savedTitle = title;
        savedContent = content;
        notificationText.text = title;
        notificationPanel.SetActive(true);
    }
}
