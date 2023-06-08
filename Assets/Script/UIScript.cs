using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    //Vars
    private static int numNO2 = 0;
    private static int numN2O4 = 0;
    public TextMeshProUGUI NO2Text;
    public TextMeshProUGUI N2O4Text;
    public GameObject particleGen;
    public int particleCreationNum;
    static private List<GameObject> currN2O4List = null;

    /* FROM TEMPERATURE BRANCH
    private static int temp = 0;
    private float initSpeed = 1;
    public GameObject NO2;
    public GameObject N204;
    */

    // Update is called once per frame
    void Update()
    {
        currN2O4List = particleGen.GetComponent<ParticleGeneration>().GetN2O4List();
        numN2O4 = currN2O4List.Count;
        N2O4Count();

    }


    public void SliderController(Slider slider)
    {
        List<GameObject> list = GetComponent<ParticleGeneration>().GetNO2List();
        if (slider.value > numNO2)
        {
            particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("NO2"), particleCreationNum, new Vector3(0,0,0));
            numNO2 += particleCreationNum;
        }
        if (slider.value < numNO2)
        {

            particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects("NO2");
            numNO2--;
        }
    }

    /*FROM TEMPERATURE BRANCH
    public void TempSliderController(Slider slider)
    {
        if (slider.value > temp)
        {
            NO2.GetComponent<ParticlePhysics>().SetSpeed(initSpeed + 1);
            N204.GetComponent<ParticlePhysics>().SetSpeed(initSpeed + 1);
            temp++;
        }
        if (slider.value < temp)
        {
            NO2.GetComponent<ParticlePhysics>().SetSpeed(initSpeed - 1);
            N204.GetComponent<ParticlePhysics>().SetSpeed(initSpeed - 1);
            temp--;
        }
    }
    */

    public void CreateButton()
    {
        particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("NO2"), particleCreationNum, new Vector3(0, 0, 0));
        numNO2 += particleCreationNum;
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
    public void N02Count() { NO2Text.text = numNO2.ToString(); }

    //Moved molecule object outside of chamber for count to reflect the molecules inside chamber
    public void N2O4Count() { N2O4Text.text = numN2O4.ToString(); }

    public void Molecule_Math(int NO2_num, int N2O4_num) { numNO2 += NO2_num; numN2O4 += N2O4_num;}
}
