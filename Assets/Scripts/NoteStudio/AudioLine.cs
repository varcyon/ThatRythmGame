using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class AudioLine : MonoBehaviour {
    //most of this is from https://www.youtube.com/watch?v=VuOzLhnajhE
    #region --- helper ---
    private class PointData {
        public int idx = -1;
        public float time = -1;
        public int startSample = -1;
        public int endSample = -1;
        public float val = 0;
        public Vector3 vec3;
    }
    private struct clipData {
        public float[] samples;
        public List<PointData> point;
        public PointData cp;
    }
    #endregion
    public LineRenderer line;
    public int samplesPerPoint = 1024;
    public float meterScale = 100;
    private clipData song;
    public AudioSource AS = null;
    public float PosY = 0;


    private void Start() {
        AS = NoteRecording.NR.song;
        line = GetComponent<LineRenderer>();
        song.samples = new float[AS.clip.samples * AS.clip.channels];
        AS.clip.GetData(song.samples, 0);
        float sum = 0;
        int cnt = 0;
        song.point = new List<PointData>();
        song.point.Capacity = song.samples.Length / samplesPerPoint + 1;
        for (int s = 0; s < song.samples.Length; s += samplesPerPoint) {
            PointData pd = new PointData();
            pd.idx = cnt++;
            pd.startSample = pd.idx * samplesPerPoint;
            pd.endSample = ((pd.idx + 1) * samplesPerPoint) - 1;
            pd.time = (pd.startSample / (float)AS.clip.channels) / (float)AS.clip.frequency;
            sum = 0;
            if (AS.clip.channels == 1) {
                for (int i = pd.startSample; i < pd.endSample; i += 1) {
                    if (i > song.samples.Length - 1)
                        break;
                    sum += song.samples[i];
                }
            } else {
                for (int i = pd.startSample; i < pd.endSample; i += AS.clip.channels) {
                    if (i > song.samples.Length - 1)
                        break;

                    sum += song.samples[i] + song.samples[i + 1] * 0.5f;
                }
            }
            pd.val = (sum / samplesPerPoint) * meterScale;
            pd.vec3 = new Vector3(pd.idx, pd.val, 0);
            song.point.Add(pd);

            line.positionCount = song.point.Count();
            line.SetPositions(song.point.Select(x => x.vec3).ToArray());
        }
        NoteRecording.NR.songEnded = false;
    }

    private void Update() {
        float timeSample = AS.timeSamples * AS.clip.channels;
        song.cp = song.point.Single(x => x.startSample <= timeSample && x.endSample > timeSample);
        this.transform.position = new Vector3(-song.cp.vec3.x, PosY);
        //This bit below checks if the song had finished by checking the line position and the X postition 
        if (NoteRecording.NR.mode == NoteRecording.Mode.Play && Mathf.Abs(this.transform.position.x) >= line.positionCount - 2) {
            NoteRecording.NR.songEnded = true;
        }
    }
}
