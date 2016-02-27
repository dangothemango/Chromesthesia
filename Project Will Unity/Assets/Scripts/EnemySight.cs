using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
    //field of view variables
    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting;
    
    //nav agent for movement
    private NavMeshAgent nav;

    //trigger detection
    private SphereCollider col;

    //player variables
    private GameObject player;
    private Vector3 playerPos;
    private float distToPlayer;
    private Vector3 previousSighting;

    // UNCOMMENT THESE WHEN DOING ENEMY ANIMATIONS
    //private Animator anim;
    //private LastPlayerSighting lastplayerSighting;
    //private HashIDs hash;

    // Use this for initialization
    void Start()
    {
        //For updated the nav agent
        nav = GetComponent<NavMeshAgent>();
        nav.updatePosition = true;
        nav.updateRotation = true;
        col = GetComponent<SphereCollider>();

        //anim = GetComponent<Animator>();
        //lastPlayerSighting = GameObject.FindGameObjectWithTags(Tags.gameController).GetComponent<LastPlayerSighting>()
        //obtaining player reference
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;
        distToPlayer = Vector3.Distance(player.transform.position, transform.position);

        //hash = GameObject.FindGameObjectsWithTag("gameController").GetComponent<HashIDs>();

    }

    // Update is called once per frame
    void Update()
    {
        //Update player position
        playerPos = player.transform.position;
        if (distToPlayer < 10)
        {
            playerInSight = true;
            nav.destination = player.transform.position;
        }

    }
    //If player enters the trigger zone.
    void OnTriggerStay(Collider other)
    {
        //Check if its a player.

        if (other.gameObject == player)
        {
            //Init player in sight to false
            playerInSight = false;
            //Check direction from enemy to player.
            Vector3 direction = other.transform.position - transform.position;
            //Compare direction angle to FoV angle.
            float angle = Vector3.Angle(direction, transform.forward);
            if (angle < fieldOfViewAngle * 0.5f)
            {

                //Cast a ray from enemy loc to player loc
                RaycastHit hit;
                //Check if it hits the player
             
                    if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                    {
                        if (hit.collider.gameObject == player)
                        {
                        //Check true if player within FoV.
                        playerInSight = true;
                        //transform.LookAt(playerPos);
                        previousSighting = playerPos;
                        }
                    }
                
            }
        }
    }

    //We don't see the player if they leave.
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player) playerInSight = false;
    }

}
