using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
#if UNITY_EDITOR
    using UnityEditor;
#endif


[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class PlacePrefab : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject beaker;
    [SerializeField] private GameObject particleGen;
    [SerializeField] private GameObject pressureManager;
    [SerializeField] private GameObject testBeaker;
    [SerializeField] private GameObject UIDisplay;
    //[SerializeField] private GameObject P_Up_Button;
    //[SerializeField] private GameObject P_Down_Button;


    [Header("Ray Cast")]
    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    [Header("Condition Before Placing")]
    bool placed = false;
    bool greeted = false;

    void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
        
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            UIDisplay.SetActive(true);
            testBeaker.SetActive(true);
        }
#endif
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    private void FingerDown(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0) return;

        //detect finger press
        if (aRRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon) && !placed && !greeted)
        {
            //set flag and create beaker object
            placed = true;
            Pose pose = hits[0].pose;
            GameObject obj = Instantiate(beaker, pose.position, pose.rotation);
            //set spawner component from beaker
            particleGen.GetComponent<ParticleGeneration>().Set_Spawner(obj);
            //turn on ui
            UIDisplay.SetActive(true);
            //find lid and set it for pressure manager
            GameObject lid = obj.transform.GetChild(3).gameObject;
            pressureManager.GetComponent<Pressure_Manager>().Set_Lid(lid);
            //GameObject.Find("AR Session Origin/Trackables").SetActive(false);


        }
    }
}
