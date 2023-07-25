using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ParticleNet : MonoBehaviour
{
    public GameObject particleGen;

    private void OnTriggerEnter(Collider other)
    {

        Debug.LogWarning("Particle Leakage! Sending back to container. - " + other.gameObject.name);

        float spawnHeight = particleGen.GetComponent<ParticleGeneration>().Get_Spawn_Height();
        Vector3 position = new Vector3(Random.Range(-3.2f, 3.2f), Random.Range(-5f, spawnHeight), Random.Range(-3.2f, 3.2f));
        other.gameObject.transform.position = position;


    }

}
