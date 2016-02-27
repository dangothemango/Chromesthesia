using UnityEngine;
using System.Collections;

public class PhasePlatform : MonoBehaviour {
    public bool isSolid;
    public GameObject AudioBeat;
    MeshRenderer visual;
    Collider collide;
    float beatTimer = 0f;

    void catchBeatSignals(BeatDetection.EventInfo eventInfo)
    {
        switch (eventInfo.messageInfo)
        {
            case BeatDetection.EventType.Kick:
                if (beatTimer > .25)
                {
                    ToggleActive();
                }
                break;
        }
    }


	// Use this for initialization
	void Start () {
        visual = gameObject.GetComponent<MeshRenderer>();
        collide = gameObject.GetComponent<Collider>();

        visual.enabled = isSolid;
        collide.enabled = isSolid;

        AudioBeat.GetComponent<BeatDetection>().CallBackFunction = catchBeatSignals;
    }
	
	// Update is called once per frame
	void Update () {
        beatTimer += Time.deltaTime;
	}

    void ToggleActive() {
        isSolid = !isSolid;
        visual.enabled = collide.enabled = isSolid;
        print("Time between beats:" + beatTimer);
        beatTimer = 0;
    }
}
