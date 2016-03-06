using UnityEngine;
using System.Collections;

public class PhasePlatform : BeatObject {
    public bool solid;
    MeshRenderer render;
    Collider collide;

    override public void onBeat()
    {
        print("beat triggered");
        solid = !solid;
        render.enabled = collide.enabled = solid;
    }

	// Use this for initialization
	void Start () {
        base.Start();

        render = gameObject.GetComponent<MeshRenderer>();
        collide = gameObject.GetComponent<Collider>();
        render.enabled = collide.enabled = solid;
        // AudioBeat.GetComponent<BeatDetection>().CallBackFunction = catchBeatSignals;
    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}
}
