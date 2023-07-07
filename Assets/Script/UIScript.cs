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
    private Slider temp_slider;
    private TextMeshProUGUI temp_str;


    private void Start()
    {
        N2O4_Counter = GetComponent<TextMeshProUGUI>();
        mag_str = GetComponent<TextMeshProUGUI>();
        lid_start_pos = lid.transform.localPosition.z;

        //temp_slider.onValueChanged.AddListener((v) =>
        //{
        //    temp_str.text = v.ToString("0.00");
        //});
    }

    private void Update()
    {
        //COUNTER


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
        }    

        //PRESSURE
        if ((up_lid_pressure || down_lid_pressure)) { Lid_Movement();}
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
        ParticleGeneration.moleculeList.Clear();
        ParticleGeneration.N2O4List.Clear();

    }

    public void Lid_Movement()
    {
        lid_current_pos = lid.transform.localPosition.z;
        float lid_level_diff = lid_start_pos - lid_current_pos;

        if (up_lid_pressure == true && lid_start_pos > lid_current_pos)
        {
            //Debug.Log("go up");
            lid.transform.Translate(Vector3.forward * Time.deltaTime / 5);
        }
        else if (down_lid_pressure == true && lid_level_diff < 412)
        {
            //Debug.Log("go down");
            lid.transform.Translate(Vector3.back * Time.deltaTime / 5);
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
    
    public void MagnitudeNum() { string str = conc_option[conc_select].ToString(); mag_str.text = str; }

}
