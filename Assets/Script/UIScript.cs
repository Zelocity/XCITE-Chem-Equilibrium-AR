using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    //Vars
    public GameObject particleGen;

    public GameObject lid;

    public static int numNO2;

    public TextMeshProUGUI n2o4Num;
    public static int numN2O4;

    public TextMeshProUGUI mag_str;
    private static int mag_num;

    public TextMeshProUGUI particleNum;
    public static int conc_select = 0;
    public static List<int> conc_option = new List<int>() { 1, 5, 10, 25, 50 };


    private static float lid_start_pos;
    private static float lid_current_pos;
    private static bool up_lid_pressure;
    private static bool down_lid_pressure;

    private Slider temp_slider;
    private TextMeshProUGUI temp_str;


    private void Start()
    {
        n2o4Num = GetComponent<TextMeshProUGUI>();
        mag_str = GetComponent<TextMeshProUGUI>();
        lid_start_pos = lid.transform.localPosition.z;

        temp_slider.onValueChanged.AddListener((v) =>
        {
            temp_str.text = v.ToString("0.00");
        });
    }

    private void Update()
    {


        //COUNTER
        numNO2 = ParticleGeneration.moleculeList.Count;
        numN2O4 = ParticleGeneration.N2O4List.Count;

        if (tag == "N02 Counter")
        {
            N02Count();
        }
        else if (tag == "N204 Counter")
        {
            N204Count();
        }
        else if (tag == "Magnitude Num")
        {
            MagnitudeNum();
        }


        //PRESSURE 
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

    public void Pressure_Up_Button(bool up) { up_lid_pressure = up; }

    public void Pressure_Down_Button(bool down) { down_lid_pressure = down; }

    public void Temperature_up_Button(bool down) { down_lid_pressure = down; }

    public void Temperature_Down_Button(bool down) { down_lid_pressure = down; }
    
    public void N02Count() { particleNum.text = numNO2.ToString(); }

    public void N204Count() { string str = numN2O4.ToString(); n2o4Num.text = str; }

    public void MagnitudeNum() { string str = conc_option[conc_select].ToString(); mag_str.text = str; }

}
