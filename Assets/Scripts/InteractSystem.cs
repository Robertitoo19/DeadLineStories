using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private float interactionDistance;

    private Camera cam;
    private Iinteractable currentInteractuable;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractuable != null)
            {
                currentInteractuable.Interact();
            }
        }
    }
    private void FixedUpdate()
    {
        DetectInteractuable();
    }

    private void DetectInteractuable()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, interactionDistance))
        {
            if (hit.transform.TryGetComponent(out Iinteractable interactable))
            {
                currentInteractuable = interactable;
                eventManager.NewInteractuable();
            }
            else
            {
                currentInteractuable = null;
                eventManager.NoInteractuable();
            }
        }
        else
        {
            currentInteractuable = null;
            eventManager.NoInteractuable();
        }
    }
}
