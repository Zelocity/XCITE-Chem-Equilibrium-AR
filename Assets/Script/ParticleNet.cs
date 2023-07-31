using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ParticleNet : MonoBehaviour
{
    public GameObject particleGen;

    private void OnTriggerEnter(Collider other)
    {


        GameObject spawner = GameObject.Find("ParticleGeneration").GetComponent<ParticleGeneration>().Get_Spawner();
        float spawnHeight = particleGen.GetComponent<ParticleGeneration>().Get_Spawn_Height();

        float spawn_x = spawner.transform.position.x;
        float spawn_y = spawner.transform.position.y;
        float spawn_z = spawner.transform.position.z;

        float newPos_X = Random.Range(spawn_x + 0.02f, spawn_x + 0.32f);
        float newPos_Y = Random.Range(spawn_y - .13f, spawn_y + .13f);
        float newPos_Z = Random.Range(spawn_z - 0.08f, spawn_z + 0.08f);


        //Debug.LogWarning("Particle Leakage! Sending back to container. - " + other.gameObject.name);

        Vector3 position = new Vector3(newPos_X, newPos_Y, newPos_Z);
        other.gameObject.transform.position = position;


    }

}
