using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour, Iinteractable
{
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Interact()
    {
        FindObjectOfType<PickAndDrop>().PickObject(this);
    }
    public void SetPhysics(bool isActive)
    {
        rb.isKinematic = !isActive;
    }
}
