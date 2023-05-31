using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleCollsion : MonoBehaviour
{
    public GameObject particleGen;
    private GameObject generateN2O4;

    //creates list that holds only N2O4. (the other list holds only NO2)
    //[SerializeField] static public List<GameObject> N2O4List;

    private void Start()
    {
        //N2O4List = new List<GameObject>();
    } 

    private void OnCollisionEnter(Collision collision)
    {

        //Gets collider for object that was hit by the particle from molecule
        Collider otherCollider = collision.GetContact(0).otherCollider;

        //Gets collider of particle which collisioned with another object
        Collider thisCollider = collision.GetContact(0).thisCollider;

        //Debug.Log("This collider tag is:" + thisCollider.tag);
        //Debug.Log("Other collider tag is:" + otherCollider.tag);

     
        if (thisCollider.CompareTag("Nitrogen") && otherCollider.CompareTag("Nitrogen"))
        {

            Debug.LogWarning("Nitrogens Hit!");
            Destroy(collision.gameObject);
            particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("N2O4"), 1);
            
        }

        if (thisCollider.CompareTag("Nitrogen") && otherCollider.CompareTag("Oxygen"))
        {
            Debug.Log("Nitrogen Hit Oxygen!");

        }

    }
}
