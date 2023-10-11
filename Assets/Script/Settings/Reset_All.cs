using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reset_All : MonoBehaviour
{
    [SerializeField] private GameObject particleGen;

    public void Reset_Particle ()
    {
        particleGen.GetComponent<ParticleGeneration>().Clear_Particles();

    }

}
