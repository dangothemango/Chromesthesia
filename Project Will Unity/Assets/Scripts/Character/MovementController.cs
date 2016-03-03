using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

    public float maxSpeed;
    public float acceleration;
    public float jumpHeight;
    Rigidbody r;

	// Use this for initialization
	void Start () {
        r = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horiz*acceleration, r.velocity.y, vert*acceleration);
        r.velocity = movementDirection;

        if (Input.GetAxis("Jump")!=0 && Mathf.Abs(r.velocity.y) < 0.01f)
        {
            movementDirection.y = jumpHeight;
        }
        r.velocity = movementDirection;
	}
}
