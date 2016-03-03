using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    //Dynamic enemy variables
    public float speed = 2f;
    public float health = 5f;
    
    //projectile variables
    public bool isAttacking = false;
    private float distance;
    private float distanceFrom;
    public GameObject projectile1;
    public GameObject projectile2;
    public GameObject projectile3;
    public GameObject projectile4;    //Create an empty game object and place spawn loc on enemy object.
    private Vector3 positiveVector;

    //Shot speed
    public float fireRate = 2.0f;
    public float fireRateConst;

    //Beat variables
    public GameObject audioBeat;

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
    

    //Catch the beat
    void catchBeatSignals(BeatDetection.EventInfo eventInfo)
    {
        switch (eventInfo.messageInfo)
        {
            case BeatDetection.EventType.Kick:
                Shoot(projectile1);
                Debug.Log("Kick");
                break;
            case BeatDetection.EventType.HitHat:
                Debug.Log("Hithat");
                Shoot(projectile2);

                break;
            case BeatDetection.EventType.Energy:
                Debug.Log("Energy");
                Shoot(projectile3);

                break;
            case BeatDetection.EventType.Snare:
                Debug.Log("Snare");
                Shoot(projectile4);

                break;
        }
    }
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
        
        //Find the player, because it's always useful to know where the player is if you are an enemy.
        player = GameObject.Find("Player");
        enemySight = GetComponent<EnemySight>();
        state = EnemyAI.State.WANDERING;


        //projectile
        fireRateConst = fireRate;
        fireRate = 0;
       // projectileSpawn = GameObject.Find("ProjectileSpawn");
        //projectile = GameObject.Find("projectile1");
        //Get beat
        audioBeat.GetComponent<BeatDetection>().CallBackFunction = catchBeatSignals;


        //Infinitely loops the finite state machine
        StartCoroutine("FSM");
	}
    //Finite State Machine
    //Switches between states based on conditions.
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
    void Shoot(GameObject proj)
    {
        //Fire projectiles
      /*  if (fireRate > 0.0)
        {
            //modify this to the beat or whatever
            fireRate -= Time.deltaTime;
        }
        else if (fireRate <= 0)
        {*/
            direction = (player.transform.position - transform.position).normalized;
            
            positiveVector = transform.position + transform.forward;
            // transform.rotation = Quaternion.LookRotation(player.transform.forward);
            Instantiate(proj, positiveVector, transform.rotation);
            fireRate = fireRateConst;
      //  }
    }

}
