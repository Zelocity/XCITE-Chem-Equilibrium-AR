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
    [SerializeField] static public List<GameObject> N2O4List = null;


    private void Awake()
    {
        moleculeList = new List<GameObject>();
        N2O4List = new List<GameObject>();
    }


    //added gameobject parameter to generate different objects. (for NO2 and N2O4)
    public void InstantiateGameObjects(GameObject prefab, int count) 
    {
        //Debug.Log(prefab);
        //Assign random variables to x, y, z rotation axis
        var rV = prefab.transform.rotation.eulerAngles;
        rV.x = Random.Range(-180f, 180f);
        rV.y = Random.Range(-180f, 180f);
        rV.z = Random.Range(-180f, 180f);
        prefab.transform.rotation = Quaternion.Euler(rV);

        //Create new molecule at random position and add it to list
        for (int i = 0; i < count; i++)
        {
            generate = Instantiate(prefab, new Vector3(Random.Range(1f, 10f), Random.Range(0.5f, 10f), Random.Range(0f, 8f)), prefab.transform.rotation);
            if(generate.CompareTag("NO2"))
            {
                moleculeList.Add(generate);
            }
            else if(generate.CompareTag("N2O4"))
            {
                N2O4List.Add(generate);
            }
            generate.transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime));
        }

       

        //Debug.Log("MOLECULE LIST:" + moleculeList.Count);
        //Debug.Log("N2O4 LIST:" + N2O4List.Count);
        //Move new molecule in random direction

    }

    //Function to destroy molecules
    //Currently it only destroys last object that was added to list after creating one
    public void DestroyGameObjects(string tag)
    {
        int MolcurrCount = moleculeList.Count;
        int N2O4currCount = N2O4List.Count;

        if (tag == "NO2")
        {
            if (MolcurrCount > 0)
            {
                Destroy(moleculeList[MolcurrCount - 1]);
                moleculeList.RemoveAt(MolcurrCount - 1);
                moleculeList.TrimExcess();
            }
            //Debug.Log("Molecule List count after deletion = " + moleculeList.Count);
        }

        if (tag == "N2O4")
        {
            if (N2O4currCount > 0)
            {
                Destroy(N2O4List[N2O4currCount - 1]);
                N2O4List.RemoveAt(N2O4currCount - 1);
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
}
