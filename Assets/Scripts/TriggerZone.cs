using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private MessageDataSO messageData;

    [SerializeField] private string messageID;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var message = messageData.GetMessageByID(messageID);
            if (message != null && !message.isTriggered)
            {
                eventManager.TriggerNotification(message.title, message.content);
                message.isTriggered = true;
            }
        }
    }
}
