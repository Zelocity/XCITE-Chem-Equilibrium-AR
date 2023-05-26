using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class N2O4Generation : MonoBehaviour
{
    //Prefab of obbject being generated, aka molecule
    //public GameObject prefab;
    //Gameobject to be created 
    private GameObject generate;
    //List to hold all objects
    [SerializeField] static public List<GameObject> N2O4List = null;


    private void Start()
    {
        N2O4List = new List<GameObject>();
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
        float randNum = Random.Range(-2.5f, 2f);
        generate = Instantiate(prefab, new Vector3(randNum, randNum, randNum), prefab.transform.rotation);
        N2O4List.Add(generate);

        Debug.Log(N2O4List.Count);
        //Move new molecule in random direction
        generate.transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime));
    }

    //Function to destroy molecules
    //Currently it only destroys last object that was added to list after creating one
    public void DestroyGameObjects()
    {
        int currCount = N2O4List.Count;

        if (currCount != 0)
        {
            Destroy(N2O4List[currCount - 1]);
            N2O4List.RemoveAt(currCount - 1);
            N2O4List.TrimExcess();
        }
        Debug.Log(N2O4List.Count);
    }

    public List<GameObject> GetNO2List()
    {
        return N2O4List;
    }
}