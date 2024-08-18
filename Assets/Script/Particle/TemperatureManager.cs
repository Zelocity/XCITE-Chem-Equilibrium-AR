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

    [Header("Slider")]
    [SerializeField] private Slider tempSlider;
    private float currTemp;

    [Header("Values")]
    private int molQuantity;
    private int molQuantity2;
    [SerializeField] private float dHrxn = -57.2f;
    [SerializeField] private float dSrxn = -175.83f;
    [SerializeField] private float R = 8.314f;
    private double K;
    private double X;

    [Header("Quantity Limits")]
    private int molQuantityLimit;
    private int molQuantityLimit2;

    void Start()
    {
        particleManager = particleGen.GetComponent<ParticleGeneration>();
    }

    // Update is called once per frame
    void Update()
    {
        molQuantity = particleManager.getParticleCount(particleManager.selectParticleIndex("NO2"));
        molQuantity2 = particleManager.getParticleCount(particleManager.selectParticleIndex("N2O4"));
        K = calculateKValue();
        X = calculateXValue();
        adjustQuantityLimit();
    }

    private double calculateKValue() {
        return Math.Exp(-((dHrxn*1000)/(R*(currTemp+273.15))) + (dSrxn/R));
    }

    private double calculateXValue() {
        return (1/(8*K)) * (1+4*K*molQuantity - Math.Sqrt(1+16*K*molQuantity2+8*K*molQuantity));
    }

    private void adjustQuantityLimit() { 
        molQuantityLimit = (int)(molQuantity-(2*X));
        molQuantityLimit2 = (int)(molQuantity2+X);
    }

    public int getMolQuantityLimit() { 
        return molQuantity;
    }
    public int getMolQuantityLimit2() { 
        return molQuantity2; 
    }
}
