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
    [SerializeField] static public List<GameObject> moleculeList;

    private void Start()
    {
        moleculeList = new List<GameObject>();
    }


    //added gameobject parameter to generate different objects. (for NO2 and N2O4)
    public void InstantiateGameObjects(GameObject prefab)
    {
        
        //Assign random variables to x, y, z rotation axis
        var rV = prefab.transform.rotation.eulerAngles;
        rV.x = Random.Range(-180f, 180f);
        rV.y = Random.Range(-180f, 180f);
        rV.z = Random.Range(-180f, 180f);
        prefab.transform.rotation = Quaternion.Euler(rV);

        //Create new molecule at random position and add it to list
        float randNum = Random.Range(-2.5f, 2f);
        generate = Instantiate(prefab, new Vector3(randNum,randNum,randNum), prefab.transform.rotation);
        moleculeList.Add(generate);

        Debug.Log(moleculeList.Count);
        //Move new molecule in random direction
        generate.transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime));
    }

    //Function to destroy molecules
    //Currently it only destroys last object that was added to list after creating one
    public void DestroyGameObjects()
    {
        int currCount = moleculeList.Count;

        if (currCount != 0)
        {
            Destroy(moleculeList[currCount - 1]);
            moleculeList.RemoveAt(currCount - 1);
            moleculeList.TrimExcess();
        }
        Debug.Log(moleculeList.Count);
    }
    public int ListCount()
    {
        return moleculeList.Count();
    }
}
