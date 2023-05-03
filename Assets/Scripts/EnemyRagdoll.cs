using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRagdoll : MonoBehaviour
{
    Animator anim;
    Rigidbody[] childrenRb;

    void Start()
    {
        anim = GetComponent<Animator>();
        childrenRb = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in childrenRb)
        {
            rb.isKinematic = true;
            rb.tag = "enemy ragdoll";
        }
    }

    public void Dead(bool gravity)
    {
        foreach (Rigidbody rb in childrenRb)
        {
            rb.isKinematic = false;
            rb.useGravity = gravity;
        }
        anim.enabled = false;
    }

    public void OffTelekinesis()
    {
        foreach (Rigidbody rb in childrenRb)
        {
            rb.useGravity = true;
        }
    }

}
