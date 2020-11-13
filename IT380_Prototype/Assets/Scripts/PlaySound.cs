using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySound : MonoBehaviour
{
    private TutorialProgress tp;

    // Start is called before the first frame update
    void Start()
    {
        tp = GameObject.FindGameObjectWithTag("UI").GetComponent<TutorialProgress>();

        Button b = GetComponent<Button>();
        AudioSource audio = GetComponent<AudioSource>();
        b.onClick.AddListener(delegate () {
            audio.Play();
            tp.audioPlayed = true;
        }
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
