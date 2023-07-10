using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePhysics : MonoBehaviour
{
    [Header("Rigidbody")]
    private Rigidbody rb;

    [Header("Speed")]
    private float maxSpeed = 10;
    private float minSpeed = 0;
    private float speedRange = 1;
    private float avgSpeed;




    //[SerializeField] private float speedMultiplier = 1f;
    private Vector3 lastFrameVelocity;
    float currMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Particle is given a random velocity vector at start
        float randNum = Random.Range(1f, 4f);
        rb.velocity = new Vector3(-20, -20, -20);
        //Note: could implement a multiplier for different particle speeds

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Debug.Log("average speed: " + avgSpeed);
        Speed_Range(avgSpeed, speedRange);
        lastFrameVelocity = rb.velocity;

        //if (speedMultiplier != currMultiplier)
        //{
        //    Debug.Log("Speed Multiplier: " + speedMultiplier);
        //    //Debug.Log("Setting/Changing Current Speed");
        //    if (speedMultiplier < 0)
        //    {
        //        rb.velocity *= -speedMultiplier;
        //    }
        //    else if (speedMultiplier > 0)
        //    {
        //        rb.velocity *= speedMultiplier + 1;
        //        //rb.AddForce(rb.velocity.normalized * speedMultiplier);
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}
        //currMultiplier = speedMultiplier;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bounce(collision.GetContact(0).normal);
    }

    private void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        //Debug.Log("Out Direction: " + direction);
        rb.velocity = direction.normalized * Mathf.Max(speed, 2);
         
    }

    public void Speed_Limit(float min, float max) {

        //if (gameObject.name == "NO2(Clone)") {
        //    Debug.Log("Current Speed: " + rb.velocity.magnitude);
        //}
        
        if (rb.velocity.magnitude > max)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, max);
        }
        if (rb.velocity.magnitude <= min)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, min);
        }
        if (gameObject.name == "NO2(Clone)")
        {
            //Debug.Log("Current Speed: " + rb.velocity.magnitude);
        }
    }

    public void Modify_Average_Speed(float value)
    {
        avgSpeed = value;
 
    }

    public void Speed_Range(float avgSpeed, float numFromAvg)
    {

        float min, max;
        min = avgSpeed - numFromAvg;
        max = avgSpeed + numFromAvg;

        if (max > maxSpeed)
        {
            max = maxSpeed;
        }
        if (min < minSpeed)
        {
            min = minSpeed;
        }
        if (gameObject.name == "NO2(Clone)")
        {
            Debug.LogWarning(min + " " + max);
        }
        
        Speed_Limit(min, max);


    }
}

