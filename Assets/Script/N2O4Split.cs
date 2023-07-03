using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class N2O4Split : MonoBehaviour
{

    public GameObject particleGen;
    float timer = 0f;
    private int listSize = 0;
    private List<GameObject> currList = null;

    private static int thisIndex;


    void Start()
    {
        timer = 0f;
    }

    // Start is called before the first frame update
    void Update()
    {
        currList = particleGen.GetComponent<ParticleGeneration>().GetN2O4List();
        listSize = currList.Count;
        thisIndex = ParticleGeneration.N2O4List.IndexOf(gameObject);
        
        //Debug.LogWarning("Number of N2O4List: " + particleGen.GetComponent<ParticleGeneration>().GetN2O4List().Count);
        if (listSize > 0 && gameObject.name == "N2O4(Clone)")
        {
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                
                //Debug.Log("5 SECONDS PASSED. About to delete " + thisIndex + " This Object: " + gameObject.name + " N2O4 Count: " + listSize);
                particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects("N2O4", thisIndex);
                particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("NO2"), 2, transform.localPosition);
                timer = 0f;
            }
        } else
        {
            timer = 0;
        }

        //StartCoroutine(waiter());
    }

    //// Update is called once per frame
    //IEnumerator waiter()
    //{


    //    yield return new WaitForSeconds(5);

    //}
}