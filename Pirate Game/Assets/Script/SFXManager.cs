using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    //public PlayerMovement playerMovement;
    //public CollectibleSystem collectible;
    public SoundLibrary[] sounds;

    //private int jumpCount = 0;
    
    public static SFXManager instance;

    void Awake()
    {
        if(instance == null)
            instance = this;

        //checks for copies and destroys if there are any copies
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);
        Init();
    }

    public void Update()
    {
        /*
        if(playerMovement.IsMoving && playerMovement.IsGrounded)
            AudioPlay("Walk");

        
        else if(!playerMovement.IsMoving || !playerMovement.IsGrounded || playerMovement.IsSprinting)
            AudioStop("Walk");
        


        if(playerMovement.IsSprinting)
            AudioPlay("Dash");


        else if(!playerMovement.IsSprinting)
        AudioStop("Run");


        if(playerMovement.IsDashing && playerMovement.IsGrounded && playerMovement.dashPressed);
        {
        AudioPlay("Dash");
        }


        if (playerMovement.IsRolling)
        {
            AudioPlay("Roll");
            AudioStop("Walk");
            AudioStop("Run");
        }

        if(playerMovement.jumpPressed)
        {
            if(jumpCount < 2)
            {
                AudioPlay("Jump");
                jumpCount++;
            }
        }
        s

        if (playerMovement.IsGrounded)
        {
            jumpCount = 0;
        }

        
        if(collectible.hasCollected)
        {
            AudioPlay("CoinPickup");
        }
        */
    }

    void Init()
    {
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

    //play function to be called from any other script
    //passed by name of clip so it is easy to code and add more audio clips
    public void AudioPlay(string name)
    {
        SoundLibrary t = GetSound(name);

        if(t == null)
            return;
        
        if(!t.source.isPlaying)
        {
            t.source.Play();
        }

        else if(t.source.isPlaying && name == "CoinPickup")
        {
            t.source.Play();
        }
    }

    public void AudioPlayOneShot(string name)
    {
        SoundLibrary t = GetSound(name);

        if (t == null)
            return;
        t.source.Stop();
        t.source.PlayOneShot(t.clip);
    }

    public void AudioStop(string name)
    {
        SoundLibrary t = GetSound(name);

        if(t == null)
            return;

        t.source.Stop();
    }

    public SoundLibrary GetSound(string name)
    {
        SoundLibrary t = null;
        
        foreach(SoundLibrary s in sounds)
        {
            if(s.name == name)
            {
                t = s;
                break;
            }
        }

        return t;
    }
}
