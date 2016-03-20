using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeatSignalManager : MonoBehaviour {

    public int maxKickPerMin = 240;
    public int maxSnarePerMin = 240;
    public int maxHiHatPerMin = 240;
    public int maxEnergyPerMin = 240;

    float minKickInterval;
    float minSnareInterval;
    float minHiHatInterval;
    float minEnergyInterval;

    float kickTimer;
    float snareTimer;
    float hiHatTimer;
    float energyTimer;

    List<BeatObject> beatObjects = new List<BeatObject>();

    GameObject audioBeat;

    public void addBeatObject(BeatObject bObj)
    {
        beatObjects.Add(bObj);
    }

    public void catchBeatSignals(BeatDetection.EventInfo eventInfo)
    {
        switch (eventInfo.messageInfo)
        {
            case BeatDetection.EventType.Kick:
                if (kickTimer >= minKickInterval)
                {
                    sendKick();
                    kickTimer = 0;
                }
                break;
            case BeatDetection.EventType.Snare:
                if(snareTimer >= minSnareInterval)
                {
                    sendSnare();
                    snareTimer = 0;
                }
                break;
            case BeatDetection.EventType.HitHat:
                if(hiHatTimer >= minHiHatInterval)
                {
                    sendHiHat();
                    hiHatTimer = 0;
                }
                break;
            case BeatDetection.EventType.Energy:
                if(energyTimer >= minEnergyInterval)
                {
                    sendEnergy();
                    energyTimer = 0;
                }
                break;
                
        }
    }

    /// <summary>
    /// Activates an onKick() method in every beatObject in the scene
    /// 
    /// NOT FUNCTIONAL YET
    /// </summary>
    void sendKick()
    {
        foreach(BeatObject obj in beatObjects)
        {
            obj.kick();
        }
    }

    /// <summary>
    /// Activates an onSnare() method in every beatObject in the scene
    /// 
    /// NOT FUNCTIONAL YET
    /// </summary>
    void sendSnare()
    {
        foreach (BeatObject obj in beatObjects)
        {
            obj.snare();
        }
    }

    /// <summary>
    /// Activates an onHiHat() method in every beatObject in the scene
    /// 
    /// NOT FUNCTIONAL YET
    /// </summary>
    void sendHiHat()
    {
        foreach (BeatObject obj in beatObjects)
        {
            obj.hiHat();
        }
    }

    /// <summary>
    /// Activates an onEnergy() method in every beatObject in the scene
    /// 
    /// NOT FUNCTIONAL YET
    /// </summary>
    void sendEnergy()
    {
        foreach (BeatObject obj in beatObjects)
        {
            obj.energy();
        }
    }

    // Use this for initialization
    void Start () {

        kickTimer = snareTimer = hiHatTimer = energyTimer = 0;

        minKickInterval = 60f / maxKickPerMin;
        minSnareInterval = 60f / maxSnarePerMin;
        minHiHatInterval = 60f / maxHiHatPerMin;
        minEnergyInterval = 60f / maxEnergyPerMin;

        audioBeat = gameObject;
        audioBeat.GetComponent<BeatDetection>().CallBackFunction = catchBeatSignals;
    }
	
	// Update is called once per frame
	void Update () {
        kickTimer += Time.deltaTime;
        snareTimer += Time.deltaTime;
        hiHatTimer += Time.deltaTime;
        energyTimer += Time.deltaTime;
	}
}
