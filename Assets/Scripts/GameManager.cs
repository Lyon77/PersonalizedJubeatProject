using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //UI
    public Camera mainCamera;
    public Text scoreText;
    public Text missedText;

    //cube related variables
    public GameObject box;
    public List<CubeScript> cubes;
    public List<List<float>> notes;

    //music related variables
    private AudioSource music;
    private bool stopped;
    public static AudioClip currentSong;
    public float timeTillNoteAnimationEnds = 0;

    //point related variables
    public int score = 0;
    public int missed = 0;

    //make class Singleton
    public static GameManager instance = null;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        //get aspect ratio
        float vertExtent = mainCamera.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        float minExtent = Mathf.Min(vertExtent, horzExtent);

        //easy way to change gap between cubes
        float distance = minExtent / 2.5f;

        //change box size
        float minValue = Mathf.Min(vertExtent, horzExtent);
        float size = 3;
        box.transform.localScale = new Vector3(minValue / size, minValue / size, 1);

        //Since we have a even number of cubes, we need to shift by half the distance
        cubes = new List<CubeScript>();
        
        for (int i = -2; i <= 1; i++) {
            for (int j = -2; j <= 1; j++) {
                GameObject cube = Instantiate(box, new Vector3(i * distance + distance / 2, j * distance + distance / 2 - 1), Quaternion.identity);
                cubes.Add(cube.GetComponent<CubeScript>());
            }
        }

        //generate music
        music = GetComponent<AudioSource>();
        if (currentSong != null)
            music.clip = currentSong;
        music.PlayDelayed(3);
        Debug.Log("Music Starts in 3 Seconds");

        //Start Music
        GrabNotes();
        StartCoroutine(PlayNotes(3f));

        stopped = false;
    }

    public void GrabNotes() {
        //Notes are contructed in this format:
        //[ note1, note2, note3, ..., timeWhenNoteHit ]
        //[ note1, note2, note3, ..., timeWhenNoteHit ]
        //[ note1, note2, note3, ..., timeWhenNoteHit ]
        //timeWhenNoteHit accounts for the time for the animationt to end
        notes = new List<List<float>>();

        List<float> note1 = new List<float>();
        note1.Add(0);
        note1.Add(0);
        notes.Add(note1);

        List<float> note2 = new List<float>();
        note2.Add(1);
        note2.Add(7);
        note2.Add(2);
        notes.Add(note2);
    }

    IEnumerator PlayNotes(float count) {
        yield return new WaitForSeconds(count);
        Debug.Log("Begin Song");

        //initalize time
        float lastTime = 0;
        foreach (List<float> note in notes) {
            //wait till note is played
            float time = note[note.Count - 1];
            yield return new WaitForSeconds(time - lastTime);

            //so that next time is accurate
            lastTime = time;
            
            //iterate through each note and activate it
            for (int i = 0; i < note.Count - 1; i++) {
                Debug.Log("Selected Cube " + note[i]);
                cubes[(int)note[i]].ActivateAnimation();
            }

        }

    }

    void Update() {
        //pause and unpause when necessary
        if (Input.GetKeyDown("p")) {
            if (stopped) {
                music.UnPause();
                stopped = false;
                Debug.Log("Music Unpaused");
            } else {
                music.Pause();
                stopped = true;
                Debug.Log("Music Paused");
            }
        }
    }

    public void HitNote() {
        //increment score
        score++;

        scoreText.text = "Score: " + score;
    }

    public void MissedNote() {
        //increment missed
        missed++;

        missedText.text = "Missed: " + missed;
    }

    
}
