using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    // swaps the audio clip depending on whether or not the player is in climbing mode
    public AudioClip mapMusic;
    public AudioClip climbMusic;
    private bool isClimbing = false; // start off playing mapmusic

	// Use this for initialization
	void Start ()
    {
        this.audio.loop = true;
        this.audio.playOnAwake = true;
        if (!this.audio.isPlaying)
        {
            this.audio.Play();
        }
    }
	
	// Update is called once per frame
	void Update () { }

    void SwapMusic()
    {
        isClimbing = !isClimbing;
        if (isClimbing)
        {
            this.audio.clip = climbMusic;
            this.audio.Play();
        }
        else
        {
            this.audio.clip = mapMusic;
            this.audio.Play();
        }        
    }
}
