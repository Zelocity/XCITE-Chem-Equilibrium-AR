using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class N2O4Split : MonoBehaviour
{
    [Header("Particle")]
    public GameObject particleGen;

    [Header ("Time")]
    float timer = 0f;
    private int time_to_split = 5;

    [Header("Particle List")]

    private List<List<GameObject>> particleList;
    private int listSize = 0;
    private static int thisIndex;

    private int index = -1;

    void Start()
    {
        particleGen = GameObject.Find("ParticleGeneration");
       timer = 0f;
    }

    void Update()
    {
        particleList = particleGen.GetComponent<ParticleGeneration>().getParticleIndexList();
        index = particleGen.GetComponent<ParticleGeneration>().selectParticleIndex("N2O4");
        listSize = particleList[index].Count;
        thisIndex = particleList[index].IndexOf(gameObject);
        
       //Debug.LogWarning("Number of N2O4List: " + particleGen.GetComponent<ParticleGeneration>().GetN2O4List().Count);
       if (listSize > 0 && gameObject.name == "N2O4(Clone)")
       {
           timer += Time.deltaTime;
           if (timer >= time_to_split)
           {
               //Debug.Log("5 SECONDS PASSED. About to delete " + thisIndex + " This Object: " + gameObject.name + " N2O4 Count: " + listSize);
               particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects("N2O4", thisIndex);

               //Debug.Log("X: " + transform.localPosition.x + " Y: " + transform.localPosition.y + " Z: " + transform.localPosition.z);

               particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects("NO2", 2, transform.position, true);
               //particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("NO2"), 2, help, true);
               timer = 0f;
           }
       } else
       {
           timer = 0;
       }
    }
}