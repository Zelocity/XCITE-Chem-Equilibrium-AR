using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderScript : MonoBehaviour
{

    [SerializeField] private Slider slider;

    public static int numNO2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        //slider.onValueChanged.AddListener(delegate { GetComponent<ParticleGeneration>().InstantiateGameObjects(); });

        
    }

    // Update is called once per frame
    void Update()
    {
       //Debug.Log(slider.value);

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

    //getter and setter for N02 Number count
    public static int GetNumN02 () {return numNO2;}
    public static void SetNumN02(int num) {numNO2 = num;}
}
