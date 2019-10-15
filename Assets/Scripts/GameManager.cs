using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //cube related variables
    public GameObject box;
    public List<GameObject> cubes;
    public List<List<float>> notes;

    //music related variables
    public AudioSource music;
    private bool stopped;
    public static AudioClip currentSong;
    public float timeTillNoteAnimationEnds = 0;

    //point related variables
    public int score = 0;
    public int missed = 0;

    // Start is called before the first frame update
    void Start()
    {
        //instantiate cubes
        cubes = new List<GameObject>();

        //get aspect ratio
        float vertExtent = GetComponent<Camera>().orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        float minExtent = Mathf.Min(vertExtent, horzExtent);

        //easy way to change gap between cubes
        float distance = minExtent / 2.5f;

        //change box size
        float minValue = Mathf.Min(vertExtent, horzExtent);
        float size = 3;
        box.transform.localScale = new Vector3(minValue / size, minValue / size, 1);

        //Since we have a even number of cubes, we need to shift by half the distance
        for (int i = -2; i <= 1; i++) {
            for (int j = -2; j <= 1; j++) {
                GameObject cube = Instantiate(box, new Vector3(i * distance + distance / 2, j * distance + distance / 2 - 1), Quaternion.identity);
                cubes.Add(cube);
            }
        }

        //generate music
        music = GetComponent<AudioSource>();
        if (currentSong != null)
            music.clip = currentSong;
        //StartCoroutine("PlayMusic", 3);
        music.PlayDelayed(3);
        Debug.Log("Music Starts in 3 Seconds");

        //Start Music
        notes = new List<List<float>>();
        StartCoroutine(PlayNotes(notes, 3f));

        stopped = false;
    }

    IEnumerator PlayNotes(List<List<float>> notes, float count) {
        yield return new WaitForSeconds(count - timeTillNoteAnimationEnds);

        Debug.Log("worked");
    }

    //IEnumerator PlayMusic(float Count) {
    //    yield return new WaitForSeconds(Count); 

    //    //PlayDelayed() didn't work
    //    music.Play();

    //    yield return null;
    //}

    void Update() {
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

    private void HitNote() {
        score++;
    }

    public void MissedNote() {
        missed++;
    }
}
