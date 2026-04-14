using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public SoundLibrary[] sounds;
    

    public static SFXManager instance;

    void Awake()
    {
        if(instance == null)
            instance = this;

        //checks for copies and destroys if there are any copies
        else
            Destroy(gameObject);

        //for each clip pasted into the array, attach these components to them
        foreach (SoundLibrary s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Update()
    {
        if(playerMovement.isMoving && playerMovement.IsGrounded)
        {
            AudioPlay("Walk");
        }
        else if(!playerMovement.isMoving || !playerMovement.IsGrounded || playerMovement.IsSprinting)
        {
            AudioStop("Walk");
        }

        if(playerMovement.IsSprinting)
        {
            AudioPlay("Run");
        }

        else if(!playerMovement.IsSprinting)
        {
            AudioStop("Run");
        }
    }

    //play function to be called from any other script
    //passed by name of clip so it is easy to code and add more audio clips
    public void AudioPlay(string name)
    {
        SoundLibrary s = System.Array.Find(sounds, SoundLibrary => SoundLibrary.name == name);
        if(s == null)
            return;
        
        if(!s.source.isPlaying)
        {
            s.source.loop = true;
            s.source.Play();
        }
    }

    public void AudioStop(string name)
    {
        SoundLibrary s = System.Array.Find(sounds, SoundLibrary => SoundLibrary.name == name);
        if(s == null)
            return;

        s.source.Stop();
    }
}
