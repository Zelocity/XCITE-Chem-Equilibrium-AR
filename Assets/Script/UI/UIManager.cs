using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Particles;

public class UIManager: MonoBehaviour
{
    [Header("Particle Generation")]
    [SerializeField] private GameObject particleGen;
    private ParticleGeneration particleManager;
    public GameObject spawn;
    public TextMeshProUGUI quantityNumText;
    public Slider quantitySlider;
    private static int quantityNum = 1;

    [Header("Lid")]
    public GameObject pressureManager;
    public GameObject lid;
    private static bool upLidActive;
    private static bool downLidActive;

    [Header("Temperature")]
    [SerializeField] private GameObject tempGen;
    private TemperatureManager temperatureManager;
    public Slider tempSlider;
    private float tempValue;
    private static bool tempPointerUp;
    public TextMeshProUGUI eqNumText;
    public TextMeshProUGUI tempText;

    [Header("Particle List Panel")]
    public GameObject prefabButton;
    public RectTransform ParentPanel;
    private int selectedParticleByUser = 0;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI particleNameText;
    [SerializeField] TextMeshProUGUI particleCountText;
    private string particleName;

    private void Awake()
    {
        particleManager = particleGen.GetComponent<ParticleGeneration>();
        temperatureManager = tempGen.GetComponent<TemperatureManager>();
        
    }

    private void Start()
    {
        tempValue = tempSlider.value;
        temperatureManager.setTemp(tempValue);
        Generate_ParticleList();
    }

    private void FixedUpdate()
    {
        particleName = particleManager.getParticleName(selectedParticleByUser);
        setParticleNameText();
        setParticleCountText();
        setParticleEqText();
        setTemperatureText();
        particleQuantity();

        if (tempPointerUp)
        {
           setTempValue();
           temperatureManager.setTemp(tempValue);
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

    

    public void Dismiss_Welcome()
    {
        GameObject.Find("AR Session Origin").GetComponent<PlacePrefab>().enabled = true;

    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //PARTICLE FUNCTIONS
    
    public void CreateButton()
    {
        particleManager.randomParticleSpawn(particleName, quantityNum);
           
    }

    public void DestroyButton()
    {
        particleManager.DestroyGameObjects(particleName, -1);
    }


    public void Clear_Button()
    {
        particleManager.Clear_Particles();
    }

     private void Generate_ParticleList()
    {
        int offset = 0; 
        List<List<GameObject>> particleList = particleManager.getParticleIndexList();

        Object[] spriteList  = Resources.LoadAll("Texture2D", typeof(Sprite));

        for (int i = 0; i < particleList.Count; i++)
        {
            //Spawning Prefab
            GameObject goButton = (GameObject)Instantiate(prefabButton);
            Transform childTransform = goButton.transform.Find("Button");
            goButton.transform.SetParent(ParentPanel, false);
            goButton.transform.localScale = new Vector3(1, 1, 1);
            goButton.transform.Translate(new Vector3(0, offset, 0));

            //Button
            int tempint = i;
            Button tempButton = childTransform.GetComponent<Button>();
            tempButton.onClick.AddListener(() => setSelectedParticle(tempint));
            offset -= 150;

            //Name
            childTransform = goButton.transform.Find("Button").transform.Find("Name");
            TextMeshProUGUI particlePanelName = childTransform.GetComponent<TextMeshProUGUI>();
            particlePanelName.text = particleManager.getParticleName(tempint);

            childTransform = goButton.transform.Find("Button").transform.Find("Image");
            Image image = childTransform.GetComponent<Image>();

            Sprite sprite = (Sprite)spriteList[tempint];

            image.sprite = sprite; 
            //Texture2D tex = Resources.Load<Texture2D>("NO2");
            //image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            
        }
    }
    void setParticleNameText()
    {
        particleNameText.text = particleName;
    }

    void setParticleCountText()
    {
        particleCountText.text = particleManager.getParticleCount(selectedParticleByUser).ToString();
    }

    public void setParticleCountTextObj(TextMeshProUGUI newParticleCountText) { 
        particleCountText = newParticleCountText;
        
    }

    void setParticleEqText()
    {
        eqNumText.text = temperatureManager.getMolQuantityLimit(selectedParticleByUser).ToString();
    }

    public void particleQuantity() { quantityNum = (int)quantitySlider.value; quantityNumText.text = quantityNum.ToString();  }

    public void setSelectedParticle(int buttonNo)
    {
        selectedParticleByUser = buttonNo;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    //TEMPERATURE FUNCTIONS
    public void setTempSlider(Slider newTempSlider) {
        tempSlider = newTempSlider;
    }

    void setTempValue() { 
        tempValue = tempSlider.value;
    }

    public void setTemperatureOBJ(TextMeshProUGUI newTempText) { 
        tempText = newTempText;
    }
    void setTemperatureText()
    {
        tempText.text = temperatureManager.getTemp().ToString() + "°";
    }
     public void Temp_Slider(bool up) { tempPointerUp = up; }

    ///////////////////////////////////////////////////////////////////////////////////////////////

    //PRESSURE FUNCTIONS
    public void Set_Lid(GameObject newLid) { pressureManager.GetComponent<Pressure_Manager>().Set_Lid(newLid); }

    public void Lid_Up(bool up) { upLidActive = up; }

    public void Lid_Down(bool down) { downLidActive = down; }

    ///////////////////////////////////////////////////////////////////////////////////////////////
}




