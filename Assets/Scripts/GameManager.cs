using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject box;
    public AudioSource music;
    private bool stopped;

    // Start is called before the first frame update
    void Start()
    {
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
                Instantiate(box, new Vector3(i * distance + distance / 2, j * distance + distance / 2 - 1), Quaternion.identity);
            }
        }

        music = GetComponent<AudioSource>();
        StartCoroutine("PlayMusic", 10);
        Debug.Log("Music Starts in 3 Seconds");

        stopped = false;
    }

    IEnumerator PlayMusic(float Count) {
        yield return new WaitForSeconds(Count); //Count is the amount of time in seconds that you want to wait.

        //PlayDelayed() didn't work
        music.Play();

        yield return null;
    }

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
}
