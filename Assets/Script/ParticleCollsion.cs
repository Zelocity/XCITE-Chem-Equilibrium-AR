using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleCollsion : MonoBehaviour
{
    //public Rigidbody rb;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NO2") {
            Debug.Log("Particle Hit!");
            
        }

    }
}
