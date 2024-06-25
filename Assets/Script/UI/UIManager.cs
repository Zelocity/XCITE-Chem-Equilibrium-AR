using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Particles;

public class UIManager: MonoBehaviour
{

    [Header("Particle Generation")]
    
    public GameObject particleGen;
    private string particleName;
    private ParticleGeneration particleManager;
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
    private float currTempSpeed = 0.15f;

    [Header("Particle List Panel")]
    public GameObject prefabButton;
    public RectTransform ParentPanel;
    private int selectedParticleByUser = 0;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI particleNameText;
    [SerializeField] TextMeshProUGUI particleCountText;



    private void Awake()
    {
        particleManager = particleGen.GetComponent<ParticleGeneration>();
        
    }

    private void Start()
    {
        Generate_ParticleList();
        Temperature_Change(currTempSpeed);
    }

    private void FixedUpdate()
    {
        particleName = particleManager.getParticleName(selectedParticleByUser);
        setParticleNameText();
        setParticleCountText();

        if (temp_point_up)
        {
           Temperature_Change(temp_slider.value);
        }

        if (upLidActive)
        {
           pressureManager.GetComponent<Pressure_Manager>().Lid_Up();
        }
        else if (downLidActive)
        {
           pressureManager.GetComponent<Pressure_Manager>().Lid_Down();

        }
    }

    public void CreateButton()
    {
        particleManager.InstantiateGameObjects(particleName, conc_num, new Vector3(0, 0, 0), false);        
    }

    public void DestroyButton()
    {
        particleManager.DestroyGameObjects(particleName, -1);
    }


    public void Clear_Button()
    {
        particleManager.Clear_Particles();
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////
    ///


    public void Temperature_Change(float value)
    {
       List<List<GameObject>> particleList = particleManager.getParticleIndexList();
       int k = 0;
       for (int i = 0; i < particleList.Count; i++) {
            while (k < particleList[i].Count) {
                currTempSpeed = value *.05f;
                particleList[i][k].GetComponent<ParticlePhysics>().Modify_Average_Speed(currTempSpeed);
                k++;
            }
            k = 0;
       }
    }


    public void Lid_Up(bool up) { upLidActive = up; }

    public void Lid_Down(bool down) { downLidActive = down; }

    public void Temp_Slider(bool up) { temp_point_up = up; }

    public void MagnitudeNum() { conc_num = (int)conc_slider.value; conc_str.text = conc_num.ToString();  }

    public void Set_Lid(GameObject newLid) { pressureManager.GetComponent<Pressure_Manager>().Set_Lid(newLid); }

    public void Dismiss_Welcome()
    {
        GameObject.Find("AR Session Origin").GetComponent<PlacePrefab>().enabled = true;

    }

    public float Get_CurrentTemp()
    {
        return currTempSpeed;
    }



    private void Generate_ParticleList()
    {
        int offset = 0; 
        List<List<GameObject>> particleList = particleManager.getParticleIndexList();

        for (int i = 0; i < particleList.Count; i++)
        {
            GameObject goButton = (GameObject)Instantiate(prefabButton);
            Transform childTransform = goButton.transform.Find("Button");
            goButton.transform.SetParent(ParentPanel, false);
            goButton.transform.localScale = new Vector3(1, 1, 1);
            goButton.transform.Translate(new Vector3(0, offset, 0));



            int tempint = i;
            Button tempButton = childTransform.GetComponent<Button>();
            tempButton.onClick.AddListener(() => ButtonClicked(tempint));
            offset -= 50;
        }
    }

    void ButtonClicked(int buttonNo)
    {
        selectedParticleByUser = buttonNo;
    }

    void setParticleNameText()
    {
        particleNameText.text = particleName;
    }

    void setParticleCountText()
    {
        particleCountText.text = particleManager.getParticleCount(selectedParticleByUser).ToString();
    }
}


