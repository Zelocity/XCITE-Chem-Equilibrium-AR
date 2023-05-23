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

    public void SliderController(Slider slider)
    {
        if (slider.value > numNO2)
        {
            particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("NO2"));
            numNO2++;
        }
        if (slider.value < numNO2)
        {
            particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects();
            numNO2--;
        }
    }

    public void CreateButton()
    {
        particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("NO2"));
        numNO2++;
    }

    public void DestroyButton()
    {
        particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects();
        numNO2--;
    }

    public void UpdateCount()
    {
        //Moved molecule object outside of chamber for count to reflect the molecules inside chamber
        particleNum.text = "NO2: " + numNO2;
    }
}
