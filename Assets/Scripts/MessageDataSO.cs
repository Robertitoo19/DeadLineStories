using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MessageData")]
public class MessageDataSO : ScriptableObject
{
    [System.Serializable]
    public class MessageData
    {
        public string messageID; // ID del mensaje
        public string title;
        public string content;
        public bool isTriggered = false; // Para evitar que se active más de una vez
    }

    public List<MessageData> messages = new List<MessageData>();

    public MessageData GetMessageByID(string id)
    {
        return messages.Find(m => m.messageID == id);
    }
}
