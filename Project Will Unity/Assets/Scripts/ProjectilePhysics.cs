﻿using UnityEngine;
using System.Collections;

public class ProjectilePhysics : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        this.transform.position = this.transform.position + (speed * forward);
	}
}