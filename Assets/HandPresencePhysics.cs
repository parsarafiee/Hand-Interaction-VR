using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;
    public Renderer nonPhysicalHand;
    public float showNonPhysicalHand= 0.05f;
    private Collider[] handColliders;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        handColliders = GetComponentsInChildren<Collider>();
    }
    public void EnableHandCollider() { 
        foreach (var collider in handColliders) {collider.enabled = true;}
    }
    public void DisableHandCollider()
    {
        foreach (var collider in handColliders) { collider.enabled = false; }
    }

    public void EnableColliderDelay(float delay)
    {
        Invoke("EnableHandCollider", delay);
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        nonPhysicalHand.enabled =(distance > showNonPhysicalHand) ?  true :  false;

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = (target.position - transform.position)/Time.fixedDeltaTime;

        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDgree, out Vector3 rotationAxis);

        Vector3 rotationDifferenceInDgree = angleInDgree * rotationAxis;
        rb.angularVelocity = rotationDifferenceInDgree*Mathf.Deg2Rad /Time.fixedDeltaTime;

    }
}
