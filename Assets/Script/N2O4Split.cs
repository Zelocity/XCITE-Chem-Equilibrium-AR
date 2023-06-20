using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class N2O4Split : MonoBehaviour
{

    public GameObject particleGen;
    float timer = 0f;
    private int listSize = 0;
    private List<GameObject> currList = null;

    void Start()
    {
        timer = 0f;
    }
    // Start is called before the first frame update
    void Update()
    {
        currList = particleGen.GetComponent<ParticleGeneration>().GetN2O4List();
        listSize = currList.Count;
        timer += Time.deltaTime;

        if (timer > 10f && listSize > 0)
        {

            Debug.Log("5 SECONDS PASSED. About to delete");
            particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects("N2O4");
            particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("NO2"), 2, new Vector3(0, 0, 0));
            timer = 0f;
            UIScript.numNO2 += 2; 
            UIScript.numN2O4--;
        }

        //StartCoroutine(waiter());
    }

    // Update is called once per frame
    IEnumerator waiter()
    {
       yield return new WaitForSeconds(5);
       Object.Destroy(this.gameObject);
    }
}
