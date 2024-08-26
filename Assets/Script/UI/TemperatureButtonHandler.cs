using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureButtonHandler : MonoBehaviour
{

    public GameObject TempInfoPanel;

    void Start()
    {

        TempInfoPanel.SetActive(false); 
        
    }

    public void OnTempButtonClick()
    {
        TempInfoPanel.SetActive(true); 
    }

    public void CloseInfoPanel()
    {
        TempInfoPanel.SetActive(false); 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
