using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlaySound : MonoBehaviour
{
    private TutorialProgress tp;

    //public AudioMixerGroup pitchBendGroup;

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
        changeSpeed();
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
        changeSpeed();
    }

    /// <summary>
    /// Change tempo of prounication based on Settings.
    /// </summary>
    public void changeSpeed()
    {
        AudioSource audio = GetComponent<AudioSource>();
        var mixer = Resources.Load<AudioMixerGroup>("Pronunciation");

        audio.outputAudioMixerGroup = mixer;
        Settings settingRef = GameObject.Find("Settings").GetComponent<Settings>();
        audio.pitch = settingRef.speedP;
        mixer.audioMixer.SetFloat("pitchBend", 1f / settingRef.speedP);
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
