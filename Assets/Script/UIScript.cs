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

    // Update is called once per frame
    void Update()
    {
        //+1 to account for the fact it starts with one
        //Moved molecule object outside of chamber for count to reflect the molecules inside chamber
        particleNum.text = "NO2: " + numNO2 + 1;
    }

    public void SliderController(Slider slider)
    {
        if (slider.value > numNO2)
        {
            GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("NO2"));
            numNO2++;
        }
        if (slider.value < numNO2)
        {
            GetComponent<ParticleGeneration>().DestroyGameObjects();
            numNO2--;
        }
    }
}
