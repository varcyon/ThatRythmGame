using System.Collections;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoteRecording : MonoBehaviour {
    static public NoteRecording NR;
    public float noteTimeThreshhold = 0.1f;
    public Collider noteStriker;
    public SongScoreDB scoreDB;
    public enum Mode {
        Record, Play
    }
    public bool songEnded;
    public float myTime;
    public bool started;
    public string path;
    public string configPath;
    public string extention;
    public AudioSource song;
    public string songName;
    public float songLength;
    public GameObject songScoreTemplate;
    public GameObject songScore;
    public GameObject note;
    public Mode mode;
    public bool isPaused;
    public TMP_InputField songNameInput;
    public string songURL;
    public AudioClip songClip;

    public GameObject recordModeBtn;
    public GameObject playModeBtn;
    public GameObject loadConfig;
    public GameObject recordNoteBtn;
    public GameObject deleteBtn;
    public GameObject saveBtn;
    public GameObject loadSongBtn;

    public GameObject strumBtn;
    void Start() {
        if (NR != this) {
            NR = this;
        }
        //loads the scriptable object from its asset path
        scoreDB = AssetDatabase.LoadAssetAtPath<SongScoreDB>("Assets/ScriptableObjects/SongScoreDB.asset");
        //sets the myTime to 0
        myTime = 0;
    }

    void Update() {
        SetMode();
        //in Playmode, if the song is finished it destroys it and instantiates a new one and sets it to active
        if (songEnded) {
            Destroy(songScore);
            Load();
            songScore.SetActive(true);
        }
        //once started is pressed myTime is being set to the delta time
        if (started) {
            myTime += Time.deltaTime;
        }
        //makes the song start playing
        if (!song.isPlaying && started) {
            song.Play();
        }

        //resets the myTime when song loops
        if (songClip != null) {
            if (myTime > songClip.length) {
                myTime = 0;
            }

        }

        Keyboard();
    }

    private void Keyboard() {   //when the space bar is hit during play mode it activates the collider for the note striker
        if (mode == Mode.Play && Input.GetKey(KeyCode.Space)) {
            noteStriker.enabled = true;

        } else {
            noteStriker.enabled = false;
        }

        // Use space bar to record
        if (mode == Mode.Record && Input.GetKeyDown(KeyCode.Space)) {
            RecordNote();
        }
    }



    private void SetMode() {
        if (mode == Mode.Record) {
            recordModeBtn.GetComponent<Button>().interactable = false;
            playModeBtn.GetComponent<Button>().interactable = true;

            recordNoteBtn.SetActive(true);
            deleteBtn.SetActive(true);
            saveBtn.SetActive(true);
            loadSongBtn.SetActive(true);
            loadConfig.SetActive(true);
            strumBtn.SetActive(false);

        }

        if (mode == Mode.Play) {
            recordModeBtn.GetComponent<Button>().interactable = true;
            playModeBtn.GetComponent<Button>().interactable = false;

            recordNoteBtn.SetActive(false);
            deleteBtn.SetActive(false);
            saveBtn.SetActive(false);
            loadSongBtn.SetActive(false);
            loadConfig.SetActive(true);
            strumBtn.SetActive(true);

        }
    }
    //creates the note on the song score and sets its song time
    public void RecordNote() {
        float time = Time.deltaTime;
        GameObject n = Instantiate(note, new Vector3(time, 7f, 0), Quaternion.identity, songScore.transform);
        n.GetComponent<Note>().SetNote(myTime, false, false);
    }
    //
    public void StartRecording() {   //when start recording is pressed it will create and assign the song score
        if (songScore == null) {
            songScore = Instantiate(songScoreTemplate, new Vector3(0, 10f, 0), Quaternion.identity);
        }
        started = true;
        songScore.SetActive(true);
        //deals with stop and starting the song
        if (isPaused) {
            song.UnPause();
        } else {
            song.Play();
        }

    }
    //for the button to pause ( stop ) recording
    public void PauseRecording() {
        started = false;
        song.Pause();
        isPaused = !isPaused;

    }
    // Load Song button, select the WAV file
    public void SelectSong() {
        path = UnityEditor.EditorUtility.OpenFilePanel("Select a Song", "", "");
        extention = path.Substring(path.IndexOf('.') + 1);
        //starts the loading of the song data
        StartCoroutine(LoadSong(path));
    }

    IEnumerator LoadSong(string p) {
        // gets info from the file path of the song
        string[] splitPath = p.Split('/');
        string temp = splitPath[splitPath.Length - 1];
        string[] temp2 = temp.Split('.');
        songName = temp2[0];
        songNameInput.text = songName;
        songURL = string.Format("file://{0}", p);
        //gets the song
        UnityWebRequest songRequest = UnityWebRequestMultimedia.GetAudioClip(songURL, AudioType.WAV);
        yield return songRequest.SendWebRequest();
        DownloadHandlerAudioClip handler = (DownloadHandlerAudioClip)songRequest.downloadHandler;
        // once its gotten it sets the song clip.
        songClip = handler.audioClip;
        song.clip = songClip;
        songLength = songClip.length;
    }

    public void Save() {   //sets the asset path of the prefab for the song score
        string scorePath = Application.dataPath + "/Resources/Songs/SongScores/" + songName + "_Score.prefab";
        //checks if that file exists, if it does delete the old one
        if (File.Exists(scorePath)) {
            File.Delete(scorePath);
        }
        //creates a prefab of the song score
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(songScore, scorePath);
        //sets a new song entry for the scriptable object database
        SongData data = new SongData(songName, prefab);
        //checks if the song is in the database already
        bool doesNotExist = true;
        foreach (SongData song in scoreDB.songScores) {
            if (song.songTitle == data.songTitle) {
                doesNotExist = false;
            }
        }
        //if it doesn't exist just add it
        if (doesNotExist) {
            Debug.Log("doesn't exist");
            scoreDB.songScores.Add(data);
        } else {
            Debug.Log("Exists");
            // this means it does exist 
            //finds the entry
            SongData exist = scoreDB.songScores.Find(x => x.songTitle == data.songTitle);
            //removes the entry
            scoreDB.songScores.Remove(exist);
            //addes a new entry
            scoreDB.songScores.Add(data);

        }
        // sets the path to the config file and checks if it exists, if it does it deletes the old one
        string configPath = Application.dataPath + "/Resources/Songs/LoadThese/" + songName + "_Config.XML";
        if (File.Exists(configPath)) {
            File.Delete(configPath);
        }
        //saves the config file as XML
        XMLOp.Serialize(new Config(songName, scorePath), configPath);
        Debug.Log("Saved");
    }
    //loads the song data and instantiates the song score and is ready for start to be pressed
    public void Load() {
        if (configPath == "")
            configPath = UnityEditor.EditorUtility.OpenFilePanel("Select a Song config", "", "");
        Config config = XMLOp.Deserialize<Config>(configPath);
        SongData songData = scoreDB.songScores.Find(x => x.songTitle == config.songTitle);
        songClip = (AudioClip)Resources.Load("Songs/Music/" + config.songTitle, typeof(AudioClip));
        song.clip = songClip;
        songName = songData.songTitle;
        songNameInput.text = songData.songTitle;
        GameObject go = Instantiate(songData.songScoreObject, new Vector3(0f, 2.5f, 0f), Quaternion.identity);
        songScore = go;
        songScore.SetActive(false);
    }
    //Delete button, deletes the entry in the scriptable object database
    public void Delete() {

        SongData songData = scoreDB.songScores.Find(x => x.songTitle == songName);
        scoreDB.songScores.Remove(songData);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //sets the modes
    public void SetModeRecording() {
        mode = Mode.Record;
    }

    public void SetModePlay() {
        mode = Mode.Play;
    }

    public void Reset() {
        SceneManager.LoadScene(1);
    }
}
