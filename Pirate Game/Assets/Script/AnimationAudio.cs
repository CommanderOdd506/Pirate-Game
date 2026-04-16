using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        PlayerMovement.OnJump += PlayJumpAudio;
    }

    private void OnDisable()
    {
        PlayerMovement.OnJump -= PlayJumpAudio;
    }

    // Update is called once per frame
    public void PlayFootstepAudio()
    {
        if (SFXManager.instance)
        {
            SFXManager.instance.AudioPlay("Walk");
        }

    }

    public void PlayDashAudio()
    {
        if (SFXManager.instance)
        {
            SFXManager.instance.AudioPlay("Dash");
        }
    }

    public void PlayJumpAudio()
    {
        if (SFXManager.instance)
        {
            SFXManager.instance.AudioPlay("Jump");
        }
    }

    public void PlayRollAudio()
    {
        if (SFXManager.instance)
        {
            SFXManager.instance.AudioPlay("Roll");
        }
    }
}
