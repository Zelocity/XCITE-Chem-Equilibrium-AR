using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TemperatureManager : MonoBehaviour
{
    [Header("Start Up")]
    [SerializeField] public GameObject particleGen;
    private ParticleGeneration particleManager;

    [Header("Values")]
    private int molQuantity; // NO2
    private int molQuantity2; // N2O4
    [SerializeField] private float dHrxn = -57.2f;
    [SerializeField] private float dSrxn = -175.83f;
    [SerializeField] private float R = 8.314f;
    private double K;
    private double X;

    [Header("Quantity Limits")]
    private int molQuantityLimit; // NO2
    private int molQuantityLimit2; // N2O4

    //Speeds 
    //                       
    private float[] speedList = {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
                               .04f, .08f, .12f, .16f, .20f, .24f, .28f, .32f, .36f, .4f};

    private int tempIndex;

    private int tempValue;

    void Start()
    {
        particleManager = particleGen.GetComponent<ParticleGeneration>();
    }

    // Update is called once per frame
    void Update()
    {
        adjustQuantityLimit();
    }

    private double calculateKValue() {
        return Math.Exp(-((dHrxn*1000)/(R*(tempValue+273.15))) + (dSrxn/R));
    }

    private double calculateXValue() {
        return (1/(8*K)) * (1+4*K*molQuantity - Math.Sqrt(1+16*K*molQuantity2+8*K*molQuantity));
    }

    private void adjustQuantityLimit() {
        molQuantity = particleManager.getParticleCount(particleManager.selectParticleIndex("NO2"));
        molQuantity2 = particleManager.getParticleCount(particleManager.selectParticleIndex("N2O4"));
        K = calculateKValue();
        X = calculateXValue();
        //NO2 
        molQuantityLimit = (int)(molQuantity-(2*X));
        //N2O4
        molQuantityLimit2 = (int)(molQuantity2+X);
    }

    public int getMolQuantityLimit(int select) { 
        if (select == 0) { 
            return molQuantityLimit;
        } else if (select == 1) { 
            return molQuantityLimit2;
        }
        Debug.LogWarning("Error: Type int select is out of bounds.");
        return -1;
    }

    public bool getMolThresholdCheck (int select) {
        if (select == 0) { // NO2
            return molQuantity > molQuantityLimit; // return true if NO2 quantity is above threshold
        } else if (select == 1) { //N2O4
            return molQuantity2 < molQuantityLimit2; // return true if N2O4 quantity is below threshold
        } 
        Debug.LogWarning("Error: Type int select is out of bounds.");
        return false;
        
    }
    public float getSpeed() { 
        return speedList[tempIndex]; 
    }

    public int getTemp() { 
        return tempValue;
    }

    public void setTemp(float num) { 
        tempValue = (int)num;
        tempIndex = (int)(num+100)/10;

        List<List<GameObject>> particleList = particleManager.getParticleIndexList();
        int k = 0;
        for (int i = 0; i < particleList.Count; i++) {
            while (k < particleList[i].Count) {
                particleList[i][k].GetComponent<ParticlePhysics>().modifySpeed(getSpeed());
                k++;
            }
            k = 0;
        }
    }
}
