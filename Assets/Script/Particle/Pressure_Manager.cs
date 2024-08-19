using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure_Manager : MonoBehaviour
{
    [SerializeField] private GameObject Lid;
    private float lidCurrPos;
    private float lidStartPos;
    private float spawnHeight;


    public void Start()
    {
        lidStartPos = Lid.transform.localPosition.y;
        lidCurrPos = lidStartPos;
    }


    public void Lid_Up()
    {
        Debug.Log("lidStartPos: " + lidStartPos + " currPos: " + lidCurrPos);
        if(lidStartPos >= lidCurrPos)
        {
            lidCurrPos++;
            Lid.transform.Translate(Vector3.right * Time.deltaTime * .2f);
        }
    }

    public void Lid_Down()
    {
        //float levelDiff = Get_Lid_Level_Diff()
        Debug.Log("lidStartPos: " + lidStartPos + " currPos: " + lidCurrPos);
        if(lidCurrPos > 617)
        {
            lidCurrPos--;
            Lid.transform.Translate(Vector3.left * Time.deltaTime * .2f);
        }
    }


    public void Lid_Reset()
    {
        while(lidStartPos > lidCurrPos)
        {
            Lid_Up();
        }
    }

    //GETTERS
    public float Get_Lid_Pos() { return lidCurrPos; }

    public float Get_Spawn_Height() { return spawnHeight; }

    //SETTERS
    public void Set_Lid(GameObject newLid)
    {
        Lid = newLid;
        lidStartPos = Lid.transform.localPosition.y;
        lidCurrPos = lidStartPos;
        Debug.Log("LID SET");
    }

    public void Set_Spawn_Height()
    {
        spawnHeight = 1 - (lidStartPos - lidCurrPos) / 412f; 
    }



}
