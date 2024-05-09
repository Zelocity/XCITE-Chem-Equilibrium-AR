using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleCollsion : MonoBehaviour
{
    // [Header ("Particle")]
    // public GameObject particleGen;

    // private void OnCollisionEnter(Collision collision)
    // {
    //     //Gets collider for object that was hit by the particle from molecule
    //     Collider otherCollider = collision.GetContact(0).otherCollider;

    //     //Gets collider of particle which collisioned with another object
    //     Collider thisCollider = collision.GetContact(0).thisCollider;


    //     List<List<GameObject>> moleculeList = particleGen.GetComponent<ParticleGeneration>().getParticleList();



    //     // else continue, and check if nitrogens hit.
    //     if (thisCollider.CompareTag("Nitrogen") && otherCollider.CompareTag("Nitrogen"))
    //     {
    //         //Save the position of collision
    //         Vector3 position = collision.contacts[0].point;
    //         //collision.gameObject.GetComponent<ParticleCollsion>().doNothing = true;

    //         //Save index of this gameObject from the NO2 List
    //         int otherIndex = ParticleGeneration.moleculeList.IndexOf(collision.gameObject);
    //         //Program crashes since thisIndex sometimes returns -1. I believe it happens when there are <2 collisions
    //         if (otherIndex == -1) return;
    //         //Debug.Log("Delete other guy, otherIndex: " + otherIndex);
    //         ParticleGeneration.moleculeList.RemoveAt(otherIndex);
    //         Destroy(collision.gameObject);

    //         //instantiate one N2O4 object 
    //         particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("N2O4"), 1, position, false);

    //         //Save index of this gameObject from the NO2 List
    //         int thisIndex = ParticleGeneration.moleculeList.IndexOf(gameObject);
    //         //Program crashes since thisIndex sometimes returns -1. I believe it happens when there are <2 collisions
    //         if (thisIndex == -1) return;
    //         //Debug.Log("Delete me, thisIndex: " + thisIndex);
    //         ParticleGeneration.moleculeList.RemoveAt(thisIndex);
    //         Destroy(gameObject);

    //     }
    //     //Debug.Log("This collider tag is:" + thisCollider.tag);
    //     //Debug.Log("Other collider tag is:" + otherCollider.tag);

    //     if (thisCollider.CompareTag("Nitrogen") && otherCollider.CompareTag("Oxygen"))
    //     {
    //         //Debug.Log("Nitrogen Hit Oxygen!");

    //     }

    // }
}