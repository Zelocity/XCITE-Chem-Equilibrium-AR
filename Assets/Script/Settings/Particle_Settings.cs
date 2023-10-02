using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Particle_Settings : MonoBehaviour
{
    [SerializeField] private GameObject particleGen;
    [SerializeField] private Button button;


    public void Start()
    {
        
    }

    public void Activate_Button()
    {
        button.interactable = !button.interactable;
    }

    public void Activate_Trail()
    {


        GameObject trailNO2 = GameObject.Find("NO2");
        GameObject trailN2O4 = GameObject.Find("N2O4/Trail");


        Debug.Log(trailNO2.name);
        //trailNO2.SetActive(!trailNO2.activeInHierarchy);
        //trailN2O4.SetActive(!trailN2O4.activeInHierarchy);

        //List<GameObject> list = particleGen.GetComponent<ParticleGeneration>().GetNO2List();
        //List<GameObject> list2 = particleGen.GetComponent<ParticleGeneration>().GetN2O4List();

        //list.AddRange(list2);

        //for (int i = 0; i < list.Capacity; i++)
        //{
        //    string molecoleName = list[i].gameObject.name;
        //    switch (molecoleName)
        //    {
        //        case "NO2(Clone)":
        //            trailNO2 = GameObject.Find("NO2(Clone)/Trail");
        //            trailNO2.SetActive(!trailNO2.activeInHierarchy);
        //            break;

        //        case "N2O4(Clone)":
        //            trailN2O4 = GameObject.Find("N2O4(Clone)/Trail");
        //            trailN2O4.SetActive(!trailN2O4.activeInHierarchy);
        //            break;

        //    }

    //}


}

}
