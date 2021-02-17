using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RhythmTracker))]
[RequireComponent(typeof(AudioSource))]
public class MusicLooper : MonoBehaviour
{
    public int timeSignature = 4;
    public int loopStartMeasure;
    public int loopEndMeasure;
    private float loopDelta;
    private float loopEndTime;
    private RhythmTracker rhythm;
    private AudioSource musicPlayer;
    void Start()
    {
        rhythm = GetComponent<RhythmTracker>();
        musicPlayer = GetComponent<AudioSource>();
        float loopStartTime = loopStartMeasure * timeSignature * 60f / rhythm.beatsPerMinute;
        loopEndTime = loopEndMeasure * timeSignature * 60f / rhythm.beatsPerMinute;
        loopDelta = loopEndTime - loopStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (musicPlayer.time > loopEndTime) {
            musicPlayer.time -= loopDelta;
            rhythm.ResetTargetBeat();
        }
    }
}
