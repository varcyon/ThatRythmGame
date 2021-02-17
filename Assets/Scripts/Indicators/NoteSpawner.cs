using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public GameObject enemyNotePrefab;
    // Start is called before the first frame update
    void Start()
    {
        Transform noteHolder = GameObject.Find("Notes").transform;

        RhythmTracker rt = GameObject.FindGameObjectWithTag("MusicSource").GetComponent<RhythmTracker>();
        int n = rt.GetNoteCount();
        for (int i = 0; i < n; i++) {
            GameObject note;
            if (!rt.NoteIsEnemy(i)) {
                note = Instantiate(notePrefab, transform.position, Quaternion.identity);
            } else {
                note = Instantiate(enemyNotePrefab, transform.position, Quaternion.identity);
            }

            note.transform.parent = noteHolder;
            note.SetActive(true);

            ScrollingNote sn = note.GetComponent<ScrollingNote>();
            if (sn != null) {
                sn.isEnemy = rt.NoteIsEnemy(i);
                sn.notePos = i;
            }
        }
    }
}
