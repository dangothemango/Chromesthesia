using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    //Dynamic enemy variables
    public float speed = 2f;
    public float health = 5f;
    public string type;

    
    //projectile variables
    public bool isAttacking = false;
    private float distance;
    private float distanceFrom;
    public GameObject projectile;
    public GameObject projectileSpawn;
    private Vector3 positiveVector;

    //Shot speed
    public float fireRate = 2.0f;
    public float fireRateConst;

    //Keeps track of waypoints
    public GameObject[] Waypoints;
    public Vector3 waypoint;
    private int WaypointIndex;

    //private Animator anim
    public NavMeshAgent nav;
    public GameObject target;
    private GameObject player;
    private EnemySight enemySight;

    private Vector3 direction;


    //State AI will switch to/from.
    public enum State {
        WANDERING,
        SHOOTING
    }

    //Set this to declare the current state.
    public State state;

    // Use this for initialization
    void Start () {
        //Initialize waypoints
        Waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        WaypointIndex = 0;
        waypoint = Waypoints[WaypointIndex].transform.position;

        //Set up nav mesh agent for movement
        nav = GetComponent<NavMeshAgent>();
        nav.updatePosition = true;
        nav.updateRotation = true;
        player = GameObject.Find("Player");
        enemySight = GetComponent<EnemySight>();
        type = "basic";
        state = EnemyAI.State.WANDERING;
        //projectile
        fireRateConst = fireRate;
        fireRate = 0;
        projectileSpawn = GameObject.Find("ProjectileSpawn");
        projectile = GameObject.Find("projectile1");
        
        //Infinitely loops the finite state machine
        StartCoroutine("FSM");
	}
    //Finite State Machine
    IEnumerator FSM()
    {
        while (true)
        {
            switch (state)
            {
                case State.WANDERING:
                    Wander();
                    break;
                case State.SHOOTING:
                   // Shoot();
                    break;
            }
            yield return null;
        }
    }
    // Update is called once per frame
    void Update () {

        //Checking game variables to determine AI state.
        if (enemySight.playerInSight == false) state = State.WANDERING;
        else if (enemySight.playerInSight == true) state = State.SHOOTING;
        
	}
    void Wander()
    {
        //Set the speed of the AI during this state.
        nav.speed = speed;
        //If waypointindex is out of range, set it back to 0.
        if (WaypointIndex >= Waypoints.Length) WaypointIndex = 0;
            //set the new waypoint.
            waypoint = Waypoints[WaypointIndex].transform.position;
        
        //if enemy near target dest
        if (nav.remainingDistance < 0.5f)
        {
            //Increment index
            //set new dest.
            nav.destination = waypoint;
            WaypointIndex++;
        }
        //Else randomize a new waypoint
        else if (WaypointIndex >= Waypoints.Length)
        {
            WaypointIndex = Random.Range(0,Waypoints.Length);
        }   
    }
    void Shoot()
    {
        
        //Need beat  vars here later
        nav.destination = player.transform.position;
        //Fire projectiles
        if (fireRate > 0.0)
        {
            //modify this to the beat or whatever
            fireRate -= Time.deltaTime;
        }
        else if (fireRate <= 0)
        {
            direction = (player.transform.position - transform.position).normalized;
            
            positiveVector = transform.position + transform.forward;
            // transform.rotation = Quaternion.LookRotation(player.transform.forward);
            Instantiate(projectile, positiveVector, transform.rotation);
            fireRate = fireRateConst;
        }
    }

}
