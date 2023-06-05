using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleCollsion : MonoBehaviour
{
    public GameObject particleGen;
    private GameObject generateN2O4;

    //boolean for checking if particles collided already.
    public bool doNothing;

    private void Start()
    {

    } 

    private void OnCollisionEnter(Collision collision)
    {
        //Gets collider for object that was hit by the particle from molecule
        Collider otherCollider = collision.GetContact(0).otherCollider;

        //Gets collider of particle which collisioned with another object
        Collider thisCollider = collision.GetContact(0).thisCollider;



        //if particles already collided, return and do nothing.
        if (doNothing) return;

        // else continue, and check if nitrogens hit.
        if (thisCollider.CompareTag("Nitrogen") && otherCollider.CompareTag("Nitrogen"))
        {
            collision.gameObject.GetComponent<ParticleCollsion>().doNothing = true;
            Destroy(collision.gameObject);
            particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("N2O4"), 1);
            Destroy(gameObject);

            UpdateN2O4UI.numN2O4++;

            //nums.Molecule_Math(-2, 1);

        }
        //Debug.Log("This collider tag is:" + thisCollider.tag);
        //Debug.Log("Other collider tag is:" + otherCollider.tag);


        if (thisCollider.CompareTag("Nitrogen") && otherCollider.CompareTag("Oxygen"))
        {
            //Debug.Log("Nitrogen Hit Oxygen!");

        }

    }
}
