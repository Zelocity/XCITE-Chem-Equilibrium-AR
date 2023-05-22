using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderScript : MonoBehaviour
{

    [SerializeField] private Slider slider;

    int num = 0;

    // Start is called before the first frame update
    void Start()
    {
        //slider.onValueChanged.AddListener(delegate { GetComponent<ParticleGeneration>().InstantiateGameObjects(); });

        
    }

    // Update is called once per frame
    void Update()
    {
       //Debug.Log(slider.value);

       if (slider.value > num)
        {
            GetComponent<ParticleGeneration>().InstantiateGameObjects();
            num++;
        }
       if (slider.value < num)
        {
            GetComponent<ParticleGeneration>().DestroyGameObjects();
            num--;
        }

    }
}
