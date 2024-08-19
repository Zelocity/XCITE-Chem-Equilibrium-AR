using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N2O4Split : MonoBehaviour
{
    [Header("Particle")]
    private GameObject particleGen;

    private ParticleGeneration particleManager;

    [Header ("Time")]
    float timer = 0f;
    private int time_to_split = 5;

    [Header("Particle List")]

    private List<List<GameObject>> particleList;
    private int listSize = 0;
    private static int thisIndex;

    private int index = -1;

    [Header("Temperature")]
    [SerializeField] public GameObject tempGen;
    private TemperatureManager temperatureManager;

    void Start()
    {
       particleGen = GameObject.Find("ParticleGeneration");
       particleManager = particleGen.GetComponent<ParticleGeneration>();

       tempGen = GameObject.Find("TemperatureManager");
       temperatureManager = tempGen.GetComponent<TemperatureManager>();
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
           if ((timer >= time_to_split && temperatureManager.getMolThresholdCheck(0)))
           {
                particleManager.splitParticleSpawn(thisIndex, transform.position);
                timer = 0f;
           }
       } else
       {
           timer = 0;
       }
    }
}