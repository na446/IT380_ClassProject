using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//This scripts set the volume sliders in the settings panel.
public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public float currentValue;

    public void SetLevel (float sliderVal)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderVal) * 20);//takes value and turns it into a logarithmic scale
        currentValue = Mathf.Log10(sliderVal) * 20;
    }

    private void Start()
    {
        dictionaryprogress prog = GameObject.Find("Progress").GetComponent<dictionaryprogress>();
        SetLevel(prog.returnVolume());
    }
}
