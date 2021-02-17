using UnityEngine;
[System.Serializable]
public class SongData {
    public string songTitle;
    public GameObject songScoreObject;

    public SongData(string songTitle, GameObject songScoreObject) {
        this.songTitle = songTitle;
        this.songScoreObject = songScoreObject;
    }
    public SongData() {
        this.songTitle = "";
        this.songScoreObject = null;
        ;
    }
}
