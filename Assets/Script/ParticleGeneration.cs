using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleGeneration : MonoBehaviour
{
    
    public GameObject spawner;
    private float spawn_x, spawn_y, spawn_z;



    private GameObject generate;
    private float spawnHeight = 1;
    //List to hold all objects
    [SerializeField] static public List<GameObject> moleculeList = null;
    [SerializeField] static public List<GameObject> N2O4List = null;

    private void Awake()
    {
        moleculeList = new List<GameObject>();
        N2O4List = new List<GameObject>();
    }

    private void Update()
    {
        spawn_x = spawner.transform.position.x;
        spawn_y = spawner.transform.position.y;
        spawn_z = spawner.transform.position.z;
    }

    //function takes in the type of object, the number of object it should spawn, and the position to spawn it at. 
    public void InstantiateGameObjects(GameObject prefab, int count, Vector3 position, bool isSpliting) 
    {
        //Debug.Log("x: " + spawner.transform.position.x);
        //Debug.Log("y: " + spawner.transform.position.y);
        //Debug.Log("z: " + spawner.transform.position.z);
        //Assign random variables to x, y, z rotation axis
        var rV = prefab.transform.rotation.eulerAngles;


        float splitDistance = .035f;
        float newPos_X = position.x;
        float newPos_Y = position.y;
        float newPos_Z = position.z;

        //Create new molecule at random position and add it to list
        for (int i = 0; i < count; i++)
        {
            rV.x = Random.Range(-180f, 180f);
            rV.y = Random.Range(-180f, 180f);
            rV.z = Random.Range(-180f, 180f);
            prefab.transform.rotation = Quaternion.Euler(rV);
            //checks to see if tag is NO2 or N2O4
            if (prefab.CompareTag("NO2"))
            {
                if (!isSpliting)
                {
                    //randPos holds random position

                    newPos_X = Random.Range(spawn_x + 0.03f, spawn_x + 0.48f);
                    newPos_Y = Random.Range(spawn_y - .1f, spawn_y + .1f);
                    newPos_Z = Random.Range(spawn_z - .168f, spawn_z + 0.168f);

                    //newPos_X = Random.Range(spawn_x - 1.2f, spawn_x + 1.2f);
                    //newPos_Y = Random.Range(spawn_y - .2f, spawn_y + (.2f + 4f * spawnHeight));
                    //newPos_Z = Random.Range(spawn_z - 2.4f, spawn_z + 2.4f);

                    //Debug.Log("spawn_y + (.2f + 10f * spawnHeight): " + (spawn_y + (.2f + 10f * spawnHeight)));



                    position = new Vector3(newPos_X, newPos_Y, newPos_Z);
                }
                else
                {
                    position.x = newPos_X;
                    position.y = newPos_Y;
                    position.z = newPos_Z;
                    if (i != 0)
                    {
                        if (position.x < 0)
                        {
                            newPos_X += splitDistance;
                        }
                        else
                        {
                            newPos_X -= splitDistance;
                        }

                        if (position.y < spawnHeight - 5)
                        {
                            newPos_Y += splitDistance;
                        }
                        else
                        {
                            newPos_Y -= splitDistance;
                        }

                        if (position.z < 0)
                        {
                            newPos_Z += splitDistance;
                        }
                        else
                        {
                            newPos_Z -= splitDistance;
                        }
                        position.x = newPos_X;
                        position.y = newPos_Y;
                        position.z = newPos_Z;
                    }
                }
                
                //generate holds an instant of prefab with random position and current rotation
                generate = Instantiate(prefab, position, prefab.transform.rotation);

                //adds instant to the NO2 list.
                moleculeList.Add(generate);

                //Debug.Log("Molecule List count after spawn = " + moleculeList.Count);
            }
            else if(prefab.CompareTag("N2O4"))
            {
                //generate holds an instant of prefab with position from parameter and current rotation.
                generate = Instantiate(prefab, position, prefab.transform.rotation);

                //adds instant to the N2O4 list.
                N2O4List.Add(generate);
            }
            generate.transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime));
        }
        //Debug.Log("MOLECULE LIST:" + moleculeList.Count);
        //Debug.Log("N2O4 LIST:" + N2O4List.Count);
    }

    //Function to destroy molecules
    //Currently it only destroys last object that was added to list after creating one
    public void DestroyGameObjects(string tag, int index)
    {
        int MolcurrCount = moleculeList.Count;
        int N2O4currCount = N2O4List.Count;

        if (tag == "NO2" && MolcurrCount > 0)
        {
            if (index == -1)
            {
                Destroy(moleculeList[MolcurrCount - 1]);
                moleculeList.RemoveAt(MolcurrCount - 1);
                moleculeList.TrimExcess();
            }
            else
            {
                Destroy(moleculeList[index]);
                moleculeList.RemoveAt(index);
                moleculeList.TrimExcess();
            }
            //Debug.Log("Molecule List count after deletion = " + moleculeList.Count);
        }

        if (tag == "N2O4" && N2O4currCount > 0)
        {
            if (index == -1)
            {
                Destroy(N2O4List[N2O4currCount - 1]);
                N2O4List.RemoveAt(N2O4currCount - 1);
                N2O4List.TrimExcess();
            }
            else
            {
                Destroy(N2O4List[index]);
                N2O4List.RemoveAt(index);
                N2O4List.TrimExcess();
            }
            //Debug.Log("N2O4 List count after deletion = " + N2O4List.Count);
        }
    }

    public List<GameObject> GetNO2List()
    {
        return moleculeList;
    }

    public List<GameObject> GetN2O4List()
    {
        return N2O4List;
    }

    public void Spawn_Height(float num)
    {

        spawnHeight = num;
        Debug.LogWarning("this: " + (spawner.transform.position.y + 1.9f) + " SpawnHeight: " + spawnHeight + " num: " + num);
    }

    public float Get_Spawn_Height()
    {
        return spawnHeight;
    }

    public GameObject Get_Spawner()
    {
        return spawner;
    }


}
