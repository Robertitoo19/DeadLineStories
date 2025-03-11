using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectInteract : MonoBehaviour
{
    [SerializeField]
    private EventManagerSO eventManager;
    [SerializeField]
    private RawImage fillCircle;

    private void OnEnable()
    {
        eventManager.OnNewInteractuable += CircleOn;
        eventManager.OnNoInteractuable += CircleOff;
    }
    private void OnDisable()
    {
        eventManager.OnNewInteractuable -= CircleOn;
        eventManager.OnNoInteractuable -= CircleOff;
    }
    private void CircleOn()
    {
        fillCircle.enabled = true;
    }
    private void CircleOff()
    {
        fillCircle.enabled = false;
    }

}
