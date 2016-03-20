using UnityEngine;
using System.Collections;

abstract public class OldBeatObject : MonoBehaviour {

    public bool kick;
    public bool snare;
    public bool hitHat;
    public bool energy;

    float beatTimer = 0f;

    public GameObject AudioBeat;

    /// <summary>
    /// This is defined by children of the BeatObject class, and contains the behaviors to be executed on a beat.
    /// </summary>
    abstract public void onBeat();

    /// <summary>
    /// Checks if the beat is triggering again sooner than expected (as defined by maxBPM)
    /// </summary>
    /// <returns>
    /// True if the beat an erreneous double trigger, false if the beat is valid.
    /// </returns>
    bool doubleTrigger()
    {
        print( beatTimer);
        return beatTimer < .25;  
    }

    /// <summary>
    /// This method is in charge of catching signals being sent by BeatDetection and checking if they are the correct type of signal (as defined by BeatType beat)
    /// </summary>
    /// <param name="eventInfo">The event being sent by BeatDetection</param>
    public void catchBeatSignals(BeatDetection.EventInfo eventInfo)
    {
        switch (eventInfo.messageInfo)
        {
            case BeatDetection.EventType.Kick:
                // print("The conditions are borked");
                if (kick && !doubleTrigger())
                {
                    beatTimer = 0f;
                    onBeat();
                }
                break;
            
            case BeatDetection.EventType.Snare:
                if (snare && !doubleTrigger())
                {
                    beatTimer = 0f;
                    onBeat();
                }
                break;
            case BeatDetection.EventType.HitHat:
                if (hitHat && !doubleTrigger())
                {
                    beatTimer = 0f;
                    onBeat();
                }
                break;
            case BeatDetection.EventType.Energy:
                if (energy && !doubleTrigger())
                {
                    beatTimer = 0f;
                    onBeat();
                }
                break;
                /*
                if (beatTimer > .25)
                {
                    beats++;
                    if (beats >= BPF)
                    {
                        beats = 0;
                        ToggleActive();
                    }
                }
                break;
                */
        }
    }

    // Use this for initialization
    public virtual void Start()
    {
        AudioBeat.GetComponent<BeatDetection>().CallBackFunction = catchBeatSignals;
    }
	
	// Update is called once per frame
	public virtual void Update () {
        beatTimer += Time.deltaTime;
	}
}
