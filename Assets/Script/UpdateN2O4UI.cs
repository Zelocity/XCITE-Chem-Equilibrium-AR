using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateN2O4UI : MonoBehaviour
{
    public TextMeshProUGUI n2o4Num;
    public static int numN2O4 = 0;

    private void Start()
    {
        n2o4Num = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        string str = "N2O4: " + numN2O4.ToString();
        n2o4Num.text = str;
    }
}