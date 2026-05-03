using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem jumpVFX;
    [SerializeField] private ParticleSystem doubleJumpVFX;
    [SerializeField] private ParticleSystem[] dashVFX;

    void OnEnable()
    {
        PlayerMovement.OnJumpVFX += PlayJump;
        PlayerMovement.OnDoubleJumpVFX += PlayDoubleJump;
        PlayerMovement.OnDash += PlayDash;
    }

    void OnDisable()
    {
        PlayerMovement.OnJumpVFX -= PlayJump;
        PlayerMovement.OnDoubleJumpVFX -= PlayDoubleJump;
        PlayerMovement.OnDash -= PlayDash;
    }

    void PlayJump() => jumpVFX?.Play();
    void PlayDoubleJump() => doubleJumpVFX?.Play();
    void PlayDash()
    {
        foreach (var vfx in dashVFX)
            vfx?.Play();
    }
}