using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleGeneration : MonoBehaviour
{
    //Prefab of obbject being generated, aka molecule
    public GameObject prefab;
    //Gameobject to be created 
    private GameObject generate;
    //List to hold all objects
    [SerializeField] static public List<GameObject> moleculeList;

    private void Start()
    {
        moleculeList = new List<GameObject>();
    }

    public void InstantiateGameObjects()
    {
        float randNum = Random.Range(-2f, 2f);
        //Create new molecule at random position and add it to list
        generate = Instantiate(prefab, new Vector3(randNum,randNum,randNum), transform.rotation);
        moleculeList.Add(generate);
        //Debug.Log(moleculeList.Count);
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
}
