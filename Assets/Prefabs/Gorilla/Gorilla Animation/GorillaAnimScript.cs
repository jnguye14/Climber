using UnityEngine;
using System.Collections;

public class GorillaAnimScript : MonoBehaviour
{
    public GameObject rightEyeLight;
    public GameObject leftEyeLight;
    public GameObject fireworks; // JFF! =D
    private bool isHowling = false;

    // SFX from: http://www.youtube.com/watch?v=2kwFYsZ4PH4
    // by Professional Sound Effects | ProFX http://www.youtube.com/user/profxsounds
    // downloaded at: https://mega.co.nz/#!TU8FXYwR!fvo54iOw7NcRi8U4IV1HyStUAMR9E2DEfNUwbkGhV7M

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isHowling && !this.animation.isPlaying) // no longer howling
        {
            fireworks.SetActive(false);
            isHowling = false;
            rightEyeLight.light.color = Color.white;
            leftEyeLight.light.color = Color.white;
        }

        if (!this.audio.isPlaying) // no sound is currently playing
        {
            // start breathing
            this.audio.Play();
            this.audio.time = 0.4f * this.audio.clip.length;
        }

        if (Input.GetKeyDown(KeyCode.R)) // for testing
        {
            Howl();
        }
	}

    void OnMouseDown(){
        Howl();
    }

    // changes the eye lights from white to red and vice-versa
    void Howl()
    {
        if (isHowling) // already howling
        {
            return;
        }

        fireworks.SetActive(true);

        // make eyes glow red
        rightEyeLight.light.color = Color.red;
        leftEyeLight.light.color = Color.red;

        // play the howling animation
        isHowling = true;
        this.animation.PlayQueued("Howl");

        // stop breathing SFX
        this.audio.Stop();

        // play the audio clip right now at the beginning until the end of the animation
        this.audio.time = 0f;
        this.audio.PlayScheduled(AudioSettings.dspTime);
        this.audio.SetScheduledEndTime(AudioSettings.dspTime + this.animation["Howl"].length);
    }
}
