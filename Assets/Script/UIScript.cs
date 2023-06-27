using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    //Vars
    public static int numNO2 = 0;
    public TextMeshProUGUI particleNum;
    public GameObject particleGen;

    public TextMeshProUGUI n2o4Num;
    public static int numN2O4 = 0;

    private void Start()
    {
        n2o4Num = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (tag == "N02 Counter") {
            N02Count();
        }
        
        if (tag == "N204 Counter")
        {
            N204Count();

        }   
    }


    public void CreateButton()
    {
        //create NO2 object with specified quantity at random location. IGNORE THIRD PARAMETER HERE
        particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("NO2"), 25, new Vector3(0,0,0));
        numNO2 += 25;
    }

    public void DestroyButton()
    {
        if (numNO2 != 0)
        {
            particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects("NO2");
            numNO2--;
        }

        //Debug.Log(numNO2);
    }


    //Moved molecule object outside of chamber for count to reflect the molecules inside chamber
    public void N02Count() { particleNum.text = numNO2.ToString(); }

    public void N204Count() { string str = numN2O4.ToString(); n2o4Num.text = str; }

}
