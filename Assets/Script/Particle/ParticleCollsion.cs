using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleCollsion : MonoBehaviour
{
    [Header ("Particle")]
    public GameObject particleGen;

    int index = 0;

    [SerializeField] private GameObject tempGen;

    private TemperatureManager temperatureManager;
    void Start () { 
        tempGen = GameObject.Find("TemperatureManager");
        temperatureManager = tempGen.GetComponent<TemperatureManager>();
        particleGen = GameObject.Find("ParticleGeneration");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if N2O4 goes above the threshold then cancel and return.
        if (temperatureManager.getMolThresholdCheck(1)) return;

        index = particleGen.GetComponent<ParticleGeneration>().selectParticleIndex("NO2");

        //Gets collider for object that was hit by the particle from molecule
        Collider otherCollider = collision.GetContact(0).otherCollider;

        //Gets collider of particle which collisioned with another object
        Collider thisCollider = collision.GetContact(0).thisCollider;

        List<List<GameObject>> particleList = particleGen.GetComponent<ParticleGeneration>().getParticleIndexList();

        // else continue, and check if nitrogens hit.
        if (thisCollider.CompareTag("Nitrogen") && otherCollider.CompareTag("Nitrogen"))
        {
            //Save the position of collision
            Vector3 position = collision.contacts[0].point;
            //collision.gameObject.GetComponent<ParticleCollsion>().doNothing = true;

            //Save index of this gameObject from the NO2 List
            int otherIndex = particleList[index].IndexOf(collision.gameObject);
            //Program crashes since thisIndex sometimes returns -1. I believe it happens when there are <2 collisions
            if (otherIndex == -1) return;
            //Debug.Log("Delete other guy, otherIndex: " + otherIndex);


            particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects("NO2", otherIndex);


            //instantiate one N2O4 object 
            particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects("N2O4", 1, position);

            //Save index of this gameObject from the NO2 List
            int thisIndex = particleList[index].IndexOf(gameObject);
            
            //Program crashes since thisIndex sometimes returns -1. I believe it happens when there are <2 collisions
            if (thisIndex == -1) return;
            //Debug.Log("Delete me, thisIndex: " + thisIndex);
            particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects("NO2", thisIndex);
            
            // ParticleGeneration.moleculeList.RemoveAt(thisIndex);
            // Destroy(gameObject);

        }
        //Debug.Log("This collider tag is:" + thisCollider.tag);
        //Debug.Log("Other collider tag is:" + otherCollider.tag);

        if (thisCollider.CompareTag("Nitrogen") && otherCollider.CompareTag("Oxygen"))
        {
            //Debug.Log("Nitrogen Hit Oxygen!");

        }

    }
}