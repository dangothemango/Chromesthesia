using UnityEngine;
using System.Collections;

abstract public class BeatObject : MonoBehaviour {

    public enum BeatType { None, Kick, Snare, HiHat, Energy};

    public bool debugTriggers = false;

    public BeatType beatTrigger1;
    public BeatType beatTrigger2;
    public BeatType beatTrigger3;
    public BeatType beatTrigger4;


    // Use this for initialization
    protected void Start () {
        BeatSignalManager manager = GameObject.Find("Main Camera").GetComponent<BeatSignalManager>();
        manager.addBeatObject(this);
	}
	
	// Update is called once per frame
	protected void Update () {
        if (debugTriggers)
        {
            if (Input.GetKeyDown("1"))
            {
                beat1();
            }
            if (Input.GetKeyDown("2"))
            {
                beat2();
            }
            if ((Input.GetKeyDown("3")))
            {
                beat3();
            }
            if (Input.GetKeyDown("4"))
            {
                beat4();
            }
        }
	}

    /// <summary>
    /// Each function beat1 through beat4 can be triggered by a different beat type.
    /// 
    /// Only beat1 must be overridden.
    /// </summary>
    protected abstract void beat1();

    void beat2() { }

    void beat3() { }

    void beat4() { }

    /// <summary>
    /// Triggers any functions set to trigger on a Kick beat
    /// </summary>
    public void kick()
    {
        if (beatTrigger1 == BeatType.Kick)
            beat1();

        if (beatTrigger2 == BeatType.Kick)
            beat2();

        if (beatTrigger3 == BeatType.Kick)
            beat3();

        if (beatTrigger4 == BeatType.Kick)
            beat4();
    }

    /// <summary>
    /// Triggers any functions set to trigger on a Snare beat
    /// </summary>
    public void snare()
    {
        if (beatTrigger1 == BeatType.Snare)
            beat1();

        if (beatTrigger2 == BeatType.Snare)
            beat2();

        if (beatTrigger3 == BeatType.Snare)
            beat3();

        if (beatTrigger4 == BeatType.Snare)
            beat4();
    }

    /// <summary>
    /// Triggers any functions set to trigger on a HiHat beat
    /// </summary>
    public void hiHat()
    {
        if (beatTrigger1 == BeatType.HiHat)
            beat1();

        if (beatTrigger2 == BeatType.HiHat)
            beat2();

        if (beatTrigger3 == BeatType.HiHat)
            beat3();

        if (beatTrigger4 == BeatType.HiHat)
            beat4();
    }

    /// <summary>
    /// Triggers any functions set to trigger on an Energy beat
    /// </summary>
    public void energy()
    {
        if (beatTrigger1 == BeatType.Energy)
            beat1();

        if (beatTrigger2 == BeatType.Energy)
            beat2();

        if (beatTrigger3 == BeatType.Energy)
            beat3();

        if (beatTrigger4 == BeatType.Energy)
            beat4();
    }

    
}
