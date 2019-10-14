using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GenerateSongs : MonoBehaviour
{
    public Button songTab;

    // Start is called before the first frame update
    void Awake()
    {
        //find the current directory
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Songs");

        //used to seperate buttons
        int idx = 0;
        foreach(FileInfo file in dir.GetFiles("*.mp3")) {
            //Grab the Prefab and get the text element
            Button newSong = Instantiate<Button>(songTab, new Vector3(transform.position.x + 600 * idx, transform.position.y, transform.position.z), Quaternion.identity);
            TextMeshProUGUI[] text = newSong.GetComponentsInChildren<TextMeshProUGUI>();

            //Set the text
            string[] splitText = file.Name.Substring(0, file.Name.Length - 4).Split('_');
            string artist = splitText[0];
            string song = splitText[1];
            text[0].SetText(artist);
            text[1].SetText(song);

            //grab audioclip from Songs folder
            AudioClip specificSong = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Songs/" + file.Name, typeof(AudioClip));

            //add the onclick listener with the specific song
            newSong.onClick.AddListener(delegate { LoadMainGame(specificSong); });

            //make the parent of the newSong is the ScrollPanel
            newSong.transform.SetParent(gameObject.transform, false);

            idx++;
        }
    }

    void LoadMainGame(AudioClip mp3File) {
        //assign the song to the GameManager
        GameManager.currentSong = mp3File;
        Debug.Log("Loading " + mp3File.name);
        SceneManager.LoadScene("Main");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
