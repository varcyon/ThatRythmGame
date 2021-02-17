[System.Serializable]
public class Config {
    public string songScoreLoc;
    public string songPath;
    public string songTitle;
    public Config(string SongTitle, string songScoreLoc) {
        this.songScoreLoc = songScoreLoc;
        this.songTitle = SongTitle;
    }

    public Config() {
        this.songScoreLoc = "";
        this.songTitle = "";
    }
}
