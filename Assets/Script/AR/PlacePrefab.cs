using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;


[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class PlacePrefab : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject beaker;
    [SerializeField] private GameObject particleGen;
    //[SerializeField] private GameObject P_Up_Button;
    //[SerializeField] private GameObject P_Down_Button;


    [Header("Ray Cast")]
    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    [Header("Condition Before Placing")]
    bool placed = false;
    bool greeted = false;

    // Start is called before the first frame update
    void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
        
    }

    private void OnEnable()
    {
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

        if (aRRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon) && !placed && !greeted)
        {
            placed = true;
            Pose pose = hits[0].pose;
            Debug.Log("ABOUT TO CREATE");
            GameObject obj = Instantiate(beaker, pose.position, pose.rotation);
            Debug.Log("BEAKER CREATED");
            particleGen.GetComponent<ParticleGeneration>().Set_Spawner(obj);
            GameObject.Find("/Canvas/UI/Neutral").SetActive(true);
            //GameObject.Find("AR Session Origin/Trackables").SetActive(false);


        }
    }
}
