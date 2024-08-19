using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Particles; 

public class ParticleGeneration : MonoBehaviour
{
    [Header("Particle")]
    
    public List<List<GameObject>> particleList;

    //List of spawnable gameobjects.
    public GameObject[] molecules;

    private List<Particle> particles;

    private List<GameObject> tempList;

    private Particle particle;

    private Particle selectedParticle;

    private GameObject generate;
    //private float splitDistance = .03f;

    [Header("Spawn")]
    public GameObject spawn;
    private float spawn_x, spawn_y, spawn_z;
    private float spawnHeight;

    [Header("Temperature")] 

    [SerializeField] private GameObject tempGen;

    private TemperatureManager temperatureManager;

    private void Awake()
    {
        temperatureManager = tempGen.GetComponent<TemperatureManager>();

        particleList = new List<List<GameObject>>();
        particles = new List<Particle>();

        for (int i = 0; i < molecules.Length; i++)
        {
            tempList = new List<GameObject>();
            particleList.Add(tempList);

            particle = new Particle(molecules[i].name, molecules[i], false);
            particles.Add(particle);
        }


        List<Particle> temp = new List<Particle>();

        temp.Add(selectParticle("NO2"));

        selectParticle("N2O4").setSplitable(true);

    }

    private void Update()
    {
        spawn_x = spawn.transform.position.x;
        spawn_y = spawn.transform.position.y;
        spawn_z = spawn.transform.position.z;
    }

    //function takes in the type of object, the number of object it should spawn, and the position to spawn it at. 
    public void InstantiateGameObjects(string particleName, int count, Vector3 position)
    {
        selectedParticle = selectParticle(particleName);
        int particleIndex = selectParticleIndex(particleName);
        if (selectedParticle == null || particleIndex == -1) return;

        // particle's gameobject
        GameObject selectedParticleObj = selectedParticle.getGameObject();
        bool selectedParticleSplit = selectedParticle.getSplitable();

        //////Assign random variables to x, y, z rotation axis
        var rV = selectedParticleObj.transform.rotation.eulerAngles;

        float newPos_X = position.x;
        float newPos_Y = position.y;
        float newPos_Z = position.z;

        ////Create new molecule at random position and add it to list
        for (int i = 0; i < count; i++)
        {
            rV.x = Random.Range(-180f, 180f);
            rV.y = Random.Range(-180f, 180f);
            rV.z = Random.Range(-180f, 180f);
            selectedParticleObj.transform.rotation = Quaternion.Euler(rV);

            //Debug.Log("spawn_y + (.2f + 10f * spawnHeight): " + (spawn_y + (.2f + 10f * spawnHeight)));
            generate = Instantiate(selectedParticleObj, position, selectedParticleObj.transform.rotation);

            particleList[particleIndex].Add(generate);

        }
    }

    public void randomParticleSpawn (string name, int num) { 
        for (int i = 0; i < num; i++) { 
            float newPos_X = Random.Range(spawn_x - .168f, spawn_x + 0.168f);
            float newPos_Y = Random.Range(spawn_y + 0.03f, spawn_y + (0.19f + (.29f * spawnHeight)));
            float newPos_Z = Random.Range(spawn_z - .1f, spawn_z + .1f);
            Vector3 position = new Vector3(newPos_X, newPos_Y, newPos_Z);
            InstantiateGameObjects(name, 1, position);     
        }
        
    }
    public void splitParticleSpawn (int index, Vector3 position) { 
        DestroyGameObjects("N2O4", index);
        randomParticleSpawn("NO2", 2);
        // InstantiateGameObjects("NO2", 2, position);    
    }

    //Function to destroy molecules
    public void DestroyGameObjects(string particleName, int index)
    {
        int particleIndex = selectParticleIndex(particleName);
        int particleCount = particleList[particleIndex].Count;
        int particleListCount = particleList.Count;

        // Debug.Log("index: " + particleIndex);
        // Debug.Log("count: " + particleCount);
        // Debug.Log("pcount: " + particleListCount);

        if (particleCount > 0 && index == -1)
        {
            Destroy(particleList[particleIndex][particleCount - 1]);
            particleList[particleIndex].RemoveAt(particleCount - 1);
            particleList[particleIndex].TrimExcess();
        } else if (particleCount > 0 && index >= 0) { 
            Destroy(particleList[particleIndex][index]);
            particleList[particleIndex].RemoveAt(index);
            particleList[particleIndex].TrimExcess();
        }
    }


    public Particle selectParticle(string name)
    {
        //reset particleObj
        selectedParticle = null;

        // if the particleList is empty then return;
        if (particles.Count <= 0) return null;

        // find particle in particleName and set gameObject
        for (int i = 0; i < particles.Count; i++)
        {
            if (name == particles[i].getName())
            {
                selectedParticle = particles[i];
            }
        }

        // if cannot find particleObj then return;
        if (selectedParticle == null) return null;

        return selectedParticle;
    }


    public int selectParticleIndex(string name)
    {
        //reset particleObj
        int index = -1;

        // if the particleList is empty then return;
        if (particles.Count <= 0) { return -1; }

        // find particle in particleName and set gameObject
        for (int i = 0; i < particles.Count; i++)
        {
            if (name == particles[i].getName())
            {
                index = i;
            }
        }
        if (index == -1) { return -1;}

        return index;
    }


    public void Spawn_Height(float num)
    {

        spawnHeight = num;
        Debug.LogWarning("this: " + (spawn.transform.position.y + 1.9f) + " SpawnHeight: " + spawnHeight + " num: " + num);
    }

    public void Set_Spawner(GameObject newSpawn)
    {
        spawn = newSpawn;
        Debug.Log("SPAWNER SET!!");
    }

    public void Clear_Particles()
    {
        for (int i = 0; i < particleList.Count; i++)
        {
            List<GameObject> objs = particleList[i];
            while(objs.Count != 0)
            {
                DestroyGameObjects(molecules[i].name, 0);
            }

        }
    }

    public float Get_Spawn_Height()
    {
        return spawnHeight;
    }

    public GameObject Get_Spawner()
    {
        return spawn;
    }

    public string getParticleName(int index)
    {
        return particles[index].getName();
    }

    public List<List<GameObject>> getParticleIndexList() { 
        return particleList;
    }

    public List<GameObject> getParticleList(int index)
    {
        return particleList[index];
    }

    public int getParticleCount(int index)
    {
        return particleList[index].Count;
    }
}
