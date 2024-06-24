using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGeneration : MonoBehaviour
{
    public GameObject prefabButton;
    public RectTransform ParentPanel;

    // Use this for initialization
    void Start()
    {
        int tempint = 1;
        GameObject goButton = (GameObject)Instantiate(prefabButton);
        Transform childTransform = goButton.transform.Find("Button");
        goButton.transform.SetParent(ParentPanel, false);
        goButton.transform.localScale = new Vector3(1, 1, 1);

        Button tempButton = childTransform.GetComponent<Button>();
        tempButton.onClick.AddListener(() => ButtonClicked(tempint));



        //for (int i = 0; i < 5; i++)
        //{
        //    GameObject goButton = (GameObject)Instantiate(prefabButton);
        //    goButton.transform.SetParent(ParentPanel, false);
        //    goButton.transform.localScale = new Vector3(1, 1, 1);

        //    Button thisButton = goButton.transform.Find("b");

        //    Button tempButton = goButton.GetComponent<Button>();
        //    int tempInt = i;

        //    tempButton.onClick.AddListener(() => ButtonClicked(tempInt));
        //}


    }

    void ButtonClicked(int buttonNo)
    {
        Debug.Log("Button clicked = " + buttonNo);
    }
}