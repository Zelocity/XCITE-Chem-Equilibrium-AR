using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{

    public GameObject particleGen;

    //N2O
    public TextMeshProUGUI NO2_Counter;
    public static int numNO2;

    //N2O4
    public TextMeshProUGUI N2O4_Counter;
    public static int numN2O4;

    //Particle Concentration
    public TextMeshProUGUI mag_str;
    private static List<int> conc_option = new List<int>() { 1, 5, 10, 25, 50 };
    private static int mag_num;
    private static int conc_select = 0;

    //Lid
    public GameObject lid;
    private static float lid_start_pos;
    private static float lid_current_pos;
    private static bool up_lid_pressure;
    private static bool down_lid_pressure;

    //Temperature
    public Slider temp_slider;
    private static bool temp_point_up;
    private static float prev_value = 0f;
    private static float curr_value;
    //public static  TextMeshProUGUI temp_str;


    private void Start()
    {
        switch (tag)
        {
            case "Untagged":
                break;

            case "N02 Counter":
                N02Count();
                return;

            case "N204 Counter":
                N2O4_Counter = GetComponent<TextMeshProUGUI>();
                return;

            case "Magnitude Num":
                mag_str = GetComponent<TextMeshProUGUI>();
                return;

            case "Pressure":
                lid_start_pos = lid.transform.localPosition.z;
                return;
        }

    }

    private void Update()
    {

        
        switch (tag)
        {
            case "Untagged":
                break;

            case "N02 Counter":
                N02Count();
                return;

            case "N204 Counter":
                N204Count();
                return;

            case "Magnitude Num":
                MagnitudeNum();
                return;

            case "Temperature":
                if (temp_point_up)
                {
                    curr_value = temp_slider.value;


                    float temp_difference = curr_value - prev_value;
                    Debug.Log("Curr value: " + curr_value + "prev value: " + prev_value + "temp diff: " + temp_difference);

                    Temperature_Change(temp_difference);
                    
                }
                else
                {
                    prev_value = curr_value;
                }
                return;

            case "Pressure":
                if ((up_lid_pressure || down_lid_pressure)) { Lid_Movement(); }
                return;
        }
        
    }


    public void CreateButton()
    {
        //create NO2 object with specified quantity at random location. IGNORE THIRD PARAMETER HERE, 4th indicates if particle is splitting.
        particleGen.GetComponent<ParticleGeneration>().InstantiateGameObjects(GameObject.Find("NO2"), conc_option[conc_select], new Vector3(0,0,0), false);
        numNO2 += conc_option[conc_select];
    }

    public void DestroyButton()
    {
        if (numNO2 != 0)
        {
            int numConc = conc_option[conc_select];
            if (numConc > numNO2)
            {
                numConc = numNO2;
            }

            for (int i = 0; i < numConc; i++)
            {
                particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects("NO2" ,-1);
            }

        }
        //Debug.Log(numNO2);
    }

    public void Add_Conc()
    {
        if (conc_select == conc_option.Count - 1) return;
        conc_select++;
    }

    public void Subtract_Conc()
    {
        if (conc_select <= 0) return;
        conc_select--;
    }

    public void Clear_Particles()
    {
        //Debug.Log("Clearing Particles");
        List<GameObject> NO2_List = ParticleGeneration.moleculeList;
        List<GameObject> N2O4_List = ParticleGeneration.N2O4List;

        while (NO2_List.Count != 0 || N2O4_List.Count != 0)
        {
            particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects("N2O4", 0);
            particleGen.GetComponent<ParticleGeneration>().DestroyGameObjects("NO2", 0);
        }
    }

    public void Lid_Movement()
    {
        lid_current_pos = lid.transform.localPosition.z;
        float lid_level_diff = lid_start_pos - lid_current_pos;

        if (up_lid_pressure == true && lid_start_pos > lid_current_pos)
        {
            //Debug.Log("go up");
            lid.transform.Translate(Vector3.forward * Time.deltaTime / 2);
        }
        else if (down_lid_pressure == true && lid_level_diff < 412)
        {
            //Debug.Log("go down");
            lid.transform.Translate(Vector3.back * Time.deltaTime / 2);
        }
    }

    public void Temperature_Change(float difference)
    {
        List<GameObject> NO2_List = ParticleGeneration.moleculeList;
        List<GameObject> N2O4_List = ParticleGeneration.N2O4List;

        int i = 0;
        while (i < NO2_List.Count) {
            NO2_List[i].GetComponent<ParticlePhysics>().Modify_Speed(difference);
            i++;
        }

        int j = 0;
        while (j < N2O4_List.Count)
        {
            N2O4_List[j].GetComponent<ParticlePhysics>().Modify_Speed(difference);
            j++;
        }
    }


    public void N02Count()
    {
        numNO2 = ParticleGeneration.moleculeList.Count;
        numN2O4 = ParticleGeneration.N2O4List.Count;
        NO2_Counter.text = numNO2.ToString();
    }

    public void N204Count()
    {
        numNO2 = ParticleGeneration.moleculeList.Count;
        numN2O4 = ParticleGeneration.N2O4List.Count;
        string str = numN2O4.ToString(); N2O4_Counter.text = str;
    }

    public void Pressure_Up_Button(bool up) { up_lid_pressure = up; }

    public void Pressure_Down_Button(bool down) { down_lid_pressure = down; }

    public void Temp_Slider(bool up) { temp_point_up = up; }

    public void MagnitudeNum() { string str = conc_option[conc_select].ToString(); mag_str.text = str; }

}
