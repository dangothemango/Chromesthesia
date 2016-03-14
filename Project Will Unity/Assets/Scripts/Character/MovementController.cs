using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

    public float maxSpeed;
    public float minMovement;
    public float acceleration;
    public float jumpHeight;
	public float hitDistance;
    Rigidbody r;

	// Use this for initialization
	void Start () {
        r = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        float newZ = r.velocity.z ;
        float newX = r.velocity.x;
        float newY = r.velocity.y;
        int zAccFactor = 1;
        int xAccFactor = 1;

        if (Mathf.Abs(r.velocity.z - vert * maxSpeed)>maxSpeed/4)
        {
            zAccFactor = 2;
        }

        if (Mathf.Abs(r.velocity.x - horiz * maxSpeed) > maxSpeed / 4)
        {
            xAccFactor = 2;
        }

        if (Mathf.Abs(vert) > minMovement){
            if (Mathf.Abs(r.velocity.z) < Mathf.Abs(maxSpeed*vert))
                newZ = r.velocity.z + acceleration * zAccFactor * vert;
        }
        else
        {
            if (r.velocity.z >0){
                newZ=(Mathf.Max(r.velocity.z-acceleration * zAccFactor,0));
            }
            else if (r.velocity.z < 0)
            {
                newZ=(Mathf.Min(r.velocity.z+acceleration * zAccFactor,0));
            }
        }
        if (Mathf.Abs(horiz) > minMovement)
        {
            if (Mathf.Abs(r.velocity.x) < Mathf.Abs(maxSpeed*horiz))
                newX = r.velocity.x + acceleration * horiz * xAccFactor;
        }
        else
        {
            if (r.velocity.x > 0)
            {
                newX = (Mathf.Max(r.velocity.x - acceleration * xAccFactor, 0));
            }
            else if (r.velocity.x < 0)
            {
                newX = (Mathf.Min(r.velocity.x + acceleration * xAccFactor, 0));
            }
        }


        if (Input.GetAxis("Jump") == 1)
        {
            if (Mathf.Abs(r.velocity.y) < .1)
            {
                newY = jumpHeight;
            }
        }

		if (Input.GetAxis("Fire1") == 1){
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward,out hit, hitDistance)){
				Interactable[] il = hit.collider.gameObject.GetComponents<Interactable>();
				if (il.Length>0){
					foreach(Interactable i in il){
						i.onHit();
					}
				}
			}

		}

        r.velocity = new Vector3(newX, newY, newZ);
	}

}
