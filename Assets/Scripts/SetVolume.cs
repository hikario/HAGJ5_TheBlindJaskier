using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    private void Start()
    {
        var slider = gameObject.GetComponent<Slider>();
        if (slider)
            mixer.SetFloat("MusicVol", Mathf.Log10(slider.value) * 20);
    }
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat ("MusicVol", Mathf.Log10 (sliderValue) * 20);
    }
}
