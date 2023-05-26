using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleGeneration : MonoBehaviour
{
    //Prefab of obbject being generated, aka molecule
    //public GameObject prefab;
    //Gameobject to be created 
    private GameObject generate;
    //List to hold all objects
    [SerializeField] static public List<GameObject> moleculeList = null;


    private void Start()
    {
        moleculeList = new List<GameObject>();
    }


    //added gameobject parameter to generate different objects. (for NO2 and N2O4)
    public void InstantiateGameObjects(GameObject prefab) 
    {
        Debug.Log(prefab);
        //Assign random variables to x, y, z rotation axis
        var rV = prefab.transform.rotation.eulerAngles;
        rV.x = Random.Range(-180f, 180f);
        rV.y = Random.Range(-180f, 180f);
        rV.z = Random.Range(-180f, 180f);
        prefab.transform.rotation = Quaternion.Euler(rV);

        //Create new molecule at random position and add it to list
        for (int i = 0; i < 25; i++)
        {
            generate = Instantiate(prefab, new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f)), prefab.transform.rotation);
            moleculeList.Add(generate);
            generate.transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime));
        }

       

        Debug.Log(moleculeList.Count);
        //Move new molecule in random direction
        
    }

    //Function to destroy molecules
    //Currently it only destroys last object that was added to list after creating one
    public void DestroyGameObjects()
    {
        int currCount = moleculeList.Count;

        if (currCount >= 0)
        {
            Destroy(moleculeList[currCount - 1]);
            moleculeList.RemoveAt(currCount - 1);
            moleculeList.TrimExcess();
        }
        Debug.Log("List count = " + moleculeList.Count);
    }

    public List<GameObject> GetNO2List()
    {
        return moleculeList;
    }
}
