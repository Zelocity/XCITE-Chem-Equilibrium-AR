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
    [SerializeField] private GameObject P_Up_Button;
    [SerializeField] private GameObject P_Down_Button;


    [Header("Ray Cast")]
    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    bool placed = false;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject.Find("/Canvas/UI").SetActive(true);
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

        if (aRRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon) && !placed)
        {
            placed = true;
            Pose pose = hits[0].pose;
            GameObject obj = Instantiate(beaker, pose.position, pose.rotation);
            particleGen.GetComponent<ParticleGeneration>().Set_Spawner(GameObject.Find("/Regular Beaker(Clone)/Particle_Spawner"));
            P_Down_Button.GetComponent<UIScript>().Set_Lid(GameObject.Find("/Regular Beaker(Clone)/Lid"));
            P_Up_Button.GetComponent<UIScript>().Set_Lid(GameObject.Find("/Regular Beaker(Clone)/Lid"));



            GameObject.Find("/Canvas/UI").SetActive(true);
            
        }
    }


    public bool Get_Placed()
    {
        return placed;
    }
}
