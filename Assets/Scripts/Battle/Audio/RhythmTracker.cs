using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RhythmTracker : MonoBehaviour
{
    public int beatsPerMinute = 120;
    public RhythmMeasure[] patterns;
    public int[] measures;
    private float beatDelta;
    private float targetBeatTime = 0f;
    private int targetBeat = 0;
    private float maxDelta;
    private float[] noteChart;
    private int[] enemyNotes;
    private AudioSource musicPlayer;
    void Start()
    {
        beatDelta = (60f / (float)beatsPerMinute);
        musicPlayer = GetComponent<AudioSource>();
        BuildFullChart();
        maxDelta = beatDelta / 3f;
    }

    // Update is called once per frame
    void Update()
    {
        //targetBeat = Mathf.RoundToInt(musicPlayer.time / beatDelta);
        //Debug.Log(beatDelta);
        while (targetBeat < noteChart.Length && musicPlayer.time - targetBeatTime > maxDelta) {
            targetBeat++;
            targetBeatTime = noteChart[targetBeat];
        }
    }

    public float GetRawAccuracy() {
        //float beatTime = targetBeat * beatDelta;
        return musicPlayer.time - targetBeatTime;
    }

    public float GetRawAccuracy(int note) {
        return musicPlayer.time - noteChart[note];
    }

    public float GetNormalizedAccuracy() {
        float raw = GetRawAccuracy();
        float norm = Mathf.Max(0, Mathf.Abs(raw / maxDelta) * -1f + 1f);
        return norm;
    }

    public float GetNormalizedAccuracy(int note) {
        float raw = GetRawAccuracy(note);
        float norm = Mathf.Max(0, Mathf.Abs(raw / maxDelta) * -1f + 1f);
        return norm;
    }

    private void BuildFullChart() {
        List<float> tempChart = new List<float>();
        List<int> tempEChart = new List<int>();
        float measurePos = 0f;
        int notePos = 0;
        foreach (int i in measures) {
            RhythmMeasure p = patterns[i];
            for (int fi = 0; fi < p.beatPos.Length; fi++) {
                float f = p.beatPos[fi];
                tempChart.Add((f + measurePos) * beatDelta);
                if (p.enemyBeat == fi) {
                    tempEChart.Add(notePos);
                }
                notePos++;
            }
            measurePos += p.totalDelta;
        }
        noteChart = tempChart.ToArray();
        enemyNotes = tempEChart.ToArray();
    }

    public void ResetTargetBeat() {
        targetBeat = 0;
        targetBeatTime = noteChart[targetBeat];

        foreach (ScrollingNote note in GameObject.Find("Notes").GetComponentsInChildren<ScrollingNote>(true)) {
            note.gameObject.SetActive(true);
        }
    }

    public int GetNoteCount() {
        return noteChart.Length;
    }

    public int[] GetEnemyNotes() {
        return enemyNotes;
    }

    public int GetTargetBeat() {
        return targetBeat;
    }

    public void DeleteCurrentNote(bool isEnemy) {
        int notePosition = GetTargetBeat();

        foreach (ScrollingNote note in GameObject.Find("Notes").GetComponentsInChildren<ScrollingNote>()) {
            if (note.notePos == notePosition && note.isEnemy == isEnemy) {
                note.gameObject.SetActive(false);
            }
        }
    }

    public bool NoteIsEnemy() {
        int n = GetTargetBeat();

        foreach (int i in enemyNotes) {
            if (n == i) {
                return true;
            }
        }

        return false;
    }

    public bool NoteIsEnemy(int n) {
        foreach (int i in enemyNotes) {
            if (n == i) {
                return true;
            }
        }
        return false;
    }
}
