using UnityEngine;
using System.Collections;

public class OrbitPlayer : MonoBehaviour {
    public Transform target;
    public float orbitDistance = 10.0f;
    public float orbitDegreesPerSec = 180.0f;
    public GameObject projectileSpawn;
    public GameObject projectile;
    //Shot speed
    public float fireRate = 2.0f;
    public float fireRateConst;

    //position in front of object.
    private Vector3 positiveVector;

    // Use this for initialization
    void Start () {
        target = GameObject.Find("Player").transform;
        //projectile
        fireRateConst = fireRate;
        fireRate = 0;
       // projectileSpawn = GameObject.Find("ProjectileSpawn");
        projectile = GameObject.Find("projectile1");
    }
    //Orbit player function
    void Orbit()
    {
        if (target != null)
        {
            transform.position = target.position + (transform.position - target.position).normalized * orbitDistance;
            transform.RotateAround(target.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
        }
    }
    void Shoot()
    {

        //Fire projectiles
        if (fireRate > 0.0)
        {
            //modify this to the beat or whatever
            fireRate -= Time.deltaTime;
        }
        else if (fireRate <= 0)
        {
            //Place it a little bit in front of the enemy
            target = GameObject.Find("Player").transform;
          
            positiveVector = transform.position + transform.forward;
            Instantiate(projectile, positiveVector, transform.rotation);
            transform.LookAt(target.transform);
            fireRate = fireRateConst;
        }
    }
    // Update is called once per frame
    void Update () {
        Shoot();
	}
    void LateUpdate()
    {

        Orbit();

    }


    
}
