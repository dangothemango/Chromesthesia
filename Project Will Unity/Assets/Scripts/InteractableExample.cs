using UnityEngine;
using System.Collections;

public class InteractableExample : Interactable {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void onHit(){
		Destroy(this.gameObject);
	}
}
