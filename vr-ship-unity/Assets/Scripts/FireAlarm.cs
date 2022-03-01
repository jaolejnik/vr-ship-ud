using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAlarm : MonoBehaviour
{
    public AudioClip alarmClip;

    private AudioSource audio;
    private AudioClip originalClip;
    private float originalVolume;
    private ControlPanel controlPanel;
    private bool isPlaying = false;
    private bool emergencyMode = false;

    void Start()
    {
        GameObject cp = GameObject.Find("ControlPanel");
        controlPanel = cp.GetComponent<ControlPanel>();

        audio = GetComponent<AudioSource>();
        originalClip = audio.clip;
        originalVolume = audio.volume;
    }

    void Update()
    {
        // quick solution to create "event" like behaviour
        // if enough time
        // TODO change to proper events 
        if (controlPanel.emergency != emergencyMode)
        {
            emergencyMode = controlPanel.emergency;
            audio.Stop();
            if (emergencyMode)
            {
                audio.clip = alarmClip;
                audio.volume = 0.05f;
            }
            else
            {
                audio.clip = originalClip;
                audio.volume = originalVolume;
            }
            audio.Play();
        }

    }
}
