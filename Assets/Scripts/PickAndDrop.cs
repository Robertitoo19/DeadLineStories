using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickAndDrop : MonoBehaviour
{
    [SerializeField]
    private Transform pickUpPosition;
    [SerializeField] 
    private float pickUpDistance;
    [SerializeField] 
    private float throwForce;

    private PickableObject pickUpObject;

    private Camera cam;
    private PlayerInput playerInput;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        cam = Camera.main;
    }
    private void OnEnable()
    {
        playerInput.actions["PickUp"].started += PickUp;
    }

    private void PickUp(InputAction.CallbackContext ctx)
    {
        if (pickUpObject == null)
        {
            TryPickObject();
        }
        else
        {
            DropObject();
        }
    }
    private void TryPickObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, cam.transform.forward, out hit, pickUpDistance)) 
        {
            Iinteractable interactable = hit.collider.GetComponent<Iinteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
    public void PickObject(PickableObject obj)
    {
        pickUpObject = obj;
        pickUpObject.SetPhysics(false);
        pickUpObject.transform.position = pickUpPosition.position;
        pickUpObject.transform.SetParent(pickUpPosition);
    }
    private void DropObject()
    {
        if (pickUpObject != null)
        {
            pickUpObject.transform.SetParent(null);
            pickUpObject.SetPhysics(true);
            pickUpObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
            pickUpObject = null;
        }
    }
}
