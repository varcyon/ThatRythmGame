using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingNote : MonoBehaviour
{
    public Vector3 scrollDirection = Vector3.left;
    public int notePos;
    public bool isEnemy;
    private Vector3 home;
    RhythmTracker rt;
    void Start()
    {
        home = transform.position;
        rt = GameObject.FindGameObjectWithTag("MusicSource").GetComponent<RhythmTracker>();
        UpdatePos();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePos();
    }

    private void UpdatePos() {
        transform.position = home + scrollDirection * rt.GetRawAccuracy(notePos);
    }
}
