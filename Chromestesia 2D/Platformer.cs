using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Platformer : MonoBehaviour {

    // v_0 = 2h/t_h
    // g = -2h/t_h)^2

    public bool debug = false;  // Set to true in editor if you want to edit the jump parameters while the game is running

    public AnimationCurve curveIn;
    public AnimationCurve curveOut;

    public float jump_height = 10f;
    public float jump_time = 1f;
    public float speed = 1f;
    public float fallFactor = 2f;   // Multiple of gravity that used for Mario weight.

    float gravity;
    float baseGrav;
    float vertAccel;
    float jump_burst;
    float yVel;
    float startJumpTime;

    float groundSpeed;
    float maxGroundSpeed;

    bool moving;
    bool pressed;
    bool rotated;
    int rotation;
    float startTime;

    // Init for physics variables
    void CalcGravAndBurst()
    {
        jump_burst = (2 * jump_height) / jump_time;
        baseGrav = (-2 * jump_height) / Mathf.Pow(jump_time, 2);
    }
    
    // Use this for initialization
	void Start () {
        // Just a tiny optimization
        if (!debug)
            CalcGravAndBurst();
        yVel = 0f;
        groundSpeed = 0f;
        startTime = -1f;
	}
	
	// Update is called once per frame
	void Update () {
        // Just a tiny optimization
        if (debug)
            CalcGravAndBurst();

        // Variable height jump and Mario weight
        if (yVel < 0 || (yVel != 0 && !CrossPlatformInputManager.GetButton("Jump")))
            gravity = fallFactor * baseGrav;
        else
            gravity = baseGrav;

        // Tracks jumping
        if (CrossPlatformInputManager.GetButtonDown("Jump") && transform.position.y == 0)
        {
            vertAccel = jump_burst;
        }
        else
        {
            vertAccel += transform.position.y > 0 ? gravity * Time.deltaTime : 0;
            if (transform.position.y < 0)
            {
                vertAccel = 0f;
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            }          
        }

        yVel = vertAccel == 0f ? 0f : vertAccel * Time.deltaTime + gravity / 2 * Mathf.Pow(Time.deltaTime, 2);

        // 8 Directional XZ Movement
        // Southwest
        if (CrossPlatformInputManager.GetAxis("Horizontal") == -1 && CrossPlatformInputManager.GetAxis("Vertical") == -1)
        {
            if (rotation != 225)
            {
                rotated = false;
                rotation = 225;
            }

            if (!moving && transform.position.y == 0)
            {
                startTime = Time.time;
                moving = true;
            }
        }
        // Southeast
        else if (CrossPlatformInputManager.GetAxis("Horizontal") == 1 && CrossPlatformInputManager.GetAxis("Vertical") == -1)
        {
            if (rotation != 135)
            {
                rotated = false;
                rotation = 135;
            }

            if (!moving && transform.position.y == 0)
            {
                startTime = Time.time;
                moving = true;
            }
        }

        // Northwest
        else if (CrossPlatformInputManager.GetAxis("Horizontal") == -1 && CrossPlatformInputManager.GetAxis("Vertical") == 1)
        {
            if (rotation != 315)
            {
                rotated = false;
                rotation = 315;
            }

            if (!moving && transform.position.y == 0)
            {
                startTime = Time.time;
                moving = true;
            }
        }

        // Northeast
        else if (CrossPlatformInputManager.GetAxis("Horizontal") == 1 && CrossPlatformInputManager.GetAxis("Vertical") == 1)
        {
            if (rotation != 45)
            {
                rotated = false;
                rotation = 45;
            }

            if (!moving && transform.position.y == 0)
            {
                startTime = Time.time;
                moving = true;
            }
        }

        // West
        else if (CrossPlatformInputManager.GetAxis("Horizontal") == -1)
        {
            if (rotation != 270)
            {
                rotated = false;
                rotation = 270;
            }

            if (!moving && transform.position.y == 0)
            {
                startTime = Time.time;
                moving = true;
            }
        }
        // East
        else if (CrossPlatformInputManager.GetAxis("Horizontal") == 1)
        {
            if (rotation != 90)
            {
                rotated = false;
                rotation = 90;
            }

            if (!moving && transform.position.y == 0)
            {
                startTime = Time.time;
                moving = true;
            }
        }

        // South
        else if (CrossPlatformInputManager.GetAxis("Vertical") == -1)
        {
            if (rotation != 180)
            {
                rotated = false;
                rotation = 180;
            }

            if (!moving && transform.position.y == 0)
            {
                startTime = Time.time;
                moving = true;
            }
        }

        // North
        else if (CrossPlatformInputManager.GetAxis("Vertical") == 1)
        {
            if (rotation != 0)
            {
                rotated = false;
                rotation = 0;
            }

            if (!moving && transform.position.y == 0)
            {
                startTime = Time.time;
                moving = true;
            }
        }
        else
        {
            if (moving)
            {
                startTime = Time.time;
                moving = false;
            }
        }

        // Ensures that the object neither accelerates nor decelerates in the XZ plane while airborne. Castlevania-style jumping, basically.
        if (transform.position.y > 0)
            startTime += Time.deltaTime;


        if (!rotated && transform.position.y <= 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, rotation, 0);
            rotated = true;
        }

        if (moving)
        {
            groundSpeed = transform.position.y <= 0 ? speed * Mathf.Clamp01(curveIn.Evaluate(Time.time - startTime)) : groundSpeed;
            maxGroundSpeed = groundSpeed;   // Ensures that the release curve doesn't increase the velocity of the object.
        }
        else
        {
            groundSpeed = maxGroundSpeed * Mathf.Clamp01(curveOut.Evaluate(Time.time - startTime));
        }
        
        // Final movement operation
        transform.Translate(0, yVel, groundSpeed);
	}
}
