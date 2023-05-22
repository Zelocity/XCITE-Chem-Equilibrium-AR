using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI particleNum;
    // Start is called before the first frame update
    void Start()
    {
        particleNum = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        particleNum.text = "NO2: " + SliderScript.GetNumN02();
    }
}
