using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicUI : MonoBehaviour
{
    public Image birdCurr;
    public Sprite birdStill;
    public Sprite birdSing;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    private AudioSource audio1;
    private AudioSource audio2;
    private AudioSource audio3;

    private bool playing1;
    private bool playing2;
    private bool playing3;

    IEnumerator ChangeSprite()
    {
        float time;
        birdCurr.sprite = birdSing;

        if (playing1)
            time = audio1.clip.length;
        else if (playing2)
            time = audio2.clip.length;
        else
            time = audio3.clip.length;

        yield return new WaitForSeconds(time);
        birdCurr.sprite = birdStill;
    }

    // Start is called before the first frame update
    void Start()
    {
        birdStill = birdCurr.sprite;
        audio1 = button1.GetComponent<AudioSource>();
        audio2 = button2.GetComponent<AudioSource>();
        audio3 = button3.GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        birdCurr.sprite = birdStill;
    }

    public void pressedMusic1()
    {
        playing1 = true;
        birdCurr.sprite = birdStill;

        if (playing2 == true)
        {
            StopCoroutine(ChangeSprite());
            playing2 = false;
        }
        else if (playing3 == true)
        {
            StopCoroutine(ChangeSprite());
            playing3 = false;
        }

        audio1.Play();
        audio2.Stop();
        audio3.Stop();
        StartCoroutine(ChangeSprite());
    }

    public void pressedMusic2()
    {
        playing2 = true;
        birdCurr.sprite = birdStill;

        if (playing1 == true)
        {
            StopCoroutine(ChangeSprite());
            playing1 = false;
        }
        else if (playing3 == true)
        {
            StopCoroutine(ChangeSprite());
            playing3 = false;
        }

        audio1.Stop();
        audio2.Play();
        audio3.Stop();
        StartCoroutine(ChangeSprite());
    }

    public void pressedMusic3()
    {
        playing3 = true;
        birdCurr.sprite = birdStill;

        if (playing1 == true)
        {
            StopCoroutine(ChangeSprite());
            playing1 = false;
        }
        else if (playing2 == true)
        {
            StopCoroutine(ChangeSprite());
            playing2 = false;
        }

        audio1.Stop();
        audio2.Stop();
        audio3.Play();
        StartCoroutine(ChangeSprite());
    }

    public void backButton()
    {
        birdCurr.sprite = birdStill;
        audio1.Stop();
        audio2.Stop();
        audio3.Stop();
        gameObject.SetActive(false);
    }
}
