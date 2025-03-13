using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleTrigger : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private SubtitleDataSO subtitleData;
    [SerializeField] private string subtitleId;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var subtitleDatas = subtitleData.GetSubtitleById(subtitleId);
            eventManager.TriggerSubtitle(subtitleDatas);
        }
    }
}
