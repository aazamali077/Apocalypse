using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAndPickUp : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField]
    private Vector3 PickedupLocation;
    private GameObject heldObject;


    private void Start()
    {
        heldObject = null;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                PickUpObject();
            }
            else
            {
                DropObject();
            }
        }

    }
    public void PickUpObject()
    {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Max(4)))
            {
                heldObject = hit.collider.gameObject;

                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                heldObject.GetComponent<Rigidbody>().useGravity = false;
                heldObject.transform.SetParent(transform);
                heldObject.transform.SetLocalPositionAndRotation(PickedupLocation, Quaternion.identity);
            }
    }


    public void DropObject()
    {
        heldObject.transform.parent = null;
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject.GetComponent<Rigidbody>().useGravity = true;
        Vector3 forceDirection = transform.TransformDirection(Vector3.forward);
        heldObject.GetComponent<Rigidbody>().AddForce(speed * Time.deltaTime * forceDirection, ForceMode.Impulse);
        heldObject = null;
    }
}
