using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private TMP_Text subtitleText;

    private Coroutine subtitleCoroutine;

    private void OnEnable()
    {
        eventManager.OnSubtitleTriggered += ShowSubtitles;
    }

    private void OnDisable()
    {
        eventManager.OnSubtitleTriggered -= ShowSubtitles;
    }

    private void ShowSubtitles(SubtitleDataSO.SubtitleEntry subtitleData)
    {
        if (subtitleData == null || subtitleData.subtitleTexts.Count == 0) return;

        // Si ya hay un subtítulo en progreso, lo detiene
        if (subtitleCoroutine != null)
        {
            StopCoroutine(subtitleCoroutine);
        }

        subtitleCoroutine = StartCoroutine(PlaySubtitles(subtitleData));
    }

    private IEnumerator PlaySubtitles(SubtitleDataSO.SubtitleEntry subtitleData)
    {
        subtitleText.gameObject.SetActive(true);

        foreach (string phrase in subtitleData.subtitleTexts)
        {
            subtitleText.text = phrase;
            yield return new WaitForSeconds(subtitleData.timePerSubtitle);
        }

        subtitleText.gameObject.SetActive(false);
    }
}
