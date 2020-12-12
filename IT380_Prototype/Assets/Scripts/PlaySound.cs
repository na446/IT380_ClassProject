using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySound : MonoBehaviour
{
    private TutorialProgress tp;

    public void changeSound(string letter)
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (letter == null)
        {
            audio.clip = null;
            gameObject.SetActive(false);
        }
        else
        {
            audio.clip = Resources.Load<AudioClip>("Sounds/Dictionary/" + letter);
            gameObject.SetActive(true);
        }
    }
    
    public void changeLetter(string letter)
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (letter == null)
        {
            audio.clip = null;
            gameObject.SetActive(false);
        }
        else
        {
            audio.clip = Resources.Load<AudioClip>("Sounds/" + letter);
            gameObject.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "02TutorialScene")
            tp = GameObject.FindGameObjectWithTag("UI").GetComponent<TutorialProgress>();

        Button b = GetComponent<Button>();
        AudioSource audio = GetComponent<AudioSource>();
        b.onClick.AddListener(delegate () {
            audio.Play();
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "02TutorialScene")
                tp.audioPlayed = true;
        }
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
