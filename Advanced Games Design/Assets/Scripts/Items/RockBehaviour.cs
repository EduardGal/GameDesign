using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //Perhaps let the rock roll a little then freeze contraints?
            rb.constraints = RigidbodyConstraints.FreezeAll;
							FindObjectOfType<AudioManager>().Play("RockThrow");
        }
    }
}
