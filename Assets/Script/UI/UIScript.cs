using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    [Header("N2O")]
    public TextMeshProUGUI NO2_Counter;
    public static int numNO2;

    [Header("N2O4")]
    public TextMeshProUGUI N2O4_Counter;
    public static int numN2O4;

    [Header("Particle Generation")]
    public GameObject particleGen;
    public GameObject spawn;
    public TextMeshProUGUI conc_str;
    public Slider conc_slider;
    private static int conc_num = 1;

    [Header("Lid")]
    public GameObject pressureManager;
    public GameObject lid;
    private static bool upLidActive;
    private static bool downLidActive;

    [Header("Temperature")]
    public Slider temp_slider;
    private static bool temp_point_up;
    //public static  TextMeshProUGUI temp_str;


    private void Start()
    {
        switch (tag)
        {
            case "Untagged":
                break;

            case "N02 Counter":
                N02Count();
                break;

            case "N204 Counter":
                N2O4_Counter = GetComponent<TextMeshProUGUI>();
                break;

            case "Temperature":
                Temperature_Change(0.15f);
                break;
        }

    }

    private void FixedUpdate()
    {
        switch (tag)
        {
            case "Untagged":
                break;

            case "N02 Counter":
                N02Count();
                break;

            case "N204 Counter":
                N204Count();
                break;

            case "Magnitude Num":
                MagnitudeNum();
                break;

            case "Temperature":
                if (temp_point_up)
                {
                    Temperature_Change(temp_slider.value);
                }
                break;
            case "Pressure":
                if (upLidActive)
                {
                    pressureManager.GetComponent<Pressure_Manager>().Lid_Up();
                }
                else if (downLidActive)
                {
                    pressureManager.GetComponent<Pressure_Manager>().Lid_Down();
                }
            break;
        }

    }

    public void CreateButton()
    {
        //create NO2 object with specified quantity at random location. IGNORE THIRD PARAMETER HERE, 4th indicates if particle is splitting.
        particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("NO2"), conc_num, new Vector3(0,0,0), false);
        numNO2 += conc_num;
    }

    public void DestroyButton()
    {
        if (numNO2 != 0)
        {
            int numConc = conc_num;
            if (numConc > numNO2)
            {
                numConc = numNO2;
            }

            for (int i = 0; i < numConc; i++)
            {
                particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects("NO2" ,-1);
            }

        }

    }

    public void Clear_Button()
    {
        particleGen.GetComponent<ParticleGeneration>().Clear_Particles();
    }

    public void Temperature_Change(float value)
    {
        List<GameObject> NO2_List = ParticleGeneration.moleculeList;
        List<GameObject> N2O4_List = ParticleGeneration.N2O4List;

        int i = 0;
        while (i < NO2_List.Count) {
            NO2_List[i].GetComponent<ParticlePhysics>().Modify_Average_Speed(value);
            i++;
        }

        int j = 0;
        while (j < N2O4_List.Count)
        {
            N2O4_List[j].GetComponent<ParticlePhysics>().Modify_Average_Speed(value);
            j++;
        }
    }


    public void N02Count()
    {
        numNO2 = ParticleGeneration.moleculeList.Count;
        NO2_Counter.text = numNO2.ToString();
    }

    public void N204Count()
    {
        numN2O4 = ParticleGeneration.N2O4List.Count;
        string str = numN2O4.ToString();
        N2O4_Counter.text = str;
    }

    public void Lid_Up(bool up) { upLidActive = up; }

    public void Lid_Down(bool down) { downLidActive = down; }

    public void Temp_Slider(bool up) { temp_point_up = up; }

    public void MagnitudeNum() { conc_num = (int)conc_slider.value; conc_str.text = conc_num.ToString(); }

    public void Set_Lid(GameObject newLid) { pressureManager.GetComponent<Pressure_Manager>().Set_Lid(newLid); }

    public void Dismiss_Welcome()
    {
        GameObject.Find("AR Session Origin").GetComponent<PlacePrefab>().enabled = true;

    }

}
