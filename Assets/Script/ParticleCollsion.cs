using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleCollsion : MonoBehaviour
{
    public GameObject particleGen;
    private void OnCollisionEnter(Collision collision)
    {
        //Gets collider for object that was hit by the particle from molecule
        Collider otherCollider = collision.GetContact(0).otherCollider;

        //Gets collider of particle which collisioned with another object
        Collider thisCollider = collision.GetContact(0).thisCollider;

        //Debug.Log("This collider tag is:" + thisCollider.tag);
        //Debug.Log("Other collider tag is:" + otherCollider.tag);

        //Made it to detect if individual particles clash with each other.
        //NOTE: I think because this script is attached to all NO2 molecules, incl uding the generated ones,
        //the code below runs for each one, thus 2 messages are printed for one collision,
        //thus i think two game objects also call the Destroy method on itself and the other.
        //This isn't a problem now since we just destroy them, but it might be later when we try to generate new N204 molecules
        //We need to make sure we generate 1 and not 2 new ones

        //thisCollider.CompareTag("Nitrogen") && otherCollider.CompareTag("Nitrogen")

        //if (collision.collider.gameObject.CompareTag("Nitrogen"))
        if (thisCollider.CompareTag("Nitrogen") && otherCollider.CompareTag("Nitrogen"))
            {
            Debug.LogWarning("Nitrogens Hit!");
            Destroy(collision.gameObject);
            //Destroy(otherCollider.gameObject);

            
            particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("N2O4"));
        }

        if (thisCollider.CompareTag("Nitrogen") && otherCollider.CompareTag("Oxygen"))
        {
            Debug.Log("Nitrogen Hit Oxygen!");

        }

    }
}
