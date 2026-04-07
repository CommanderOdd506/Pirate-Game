using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public bool isPaused = false;

	public float slowEffect = 0.5f;
	public bool isSlowed;

	//lower transition speed = longer time in between 1 and slowEffect
	public float transitionSpeed = 4f;
	private float targetTimeScale = 1f;

	public GameObject returnButton;
	

    // Start is called before the first frame update
    void Start()
    {
        returnButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInput.pausePressed && !isSlowed)
		{
			if(isPaused)
				Resume();

			else
				Pause();
		}

		if (playerInput.timePressed && !isPaused)
		{
			targetTimeScale = slowEffect;
			isSlowed = true;
		}
		else
		{
			targetTimeScale = 1f;
			isSlowed = false;
		}

		if(!isPaused)
		{
			//using a lerp to smooth in and out of slowed time
			Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, Time.unscaledDeltaTime * transitionSpeed);
			//adjusts objects physics and scales them based on our current time scale
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
		}
		
    }

    public void Pause()
	{
		Time.timeScale = 0f;
		isPaused = true;

		returnButton.SetActive(true);

		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}

	public void Resume()
	{
		Time.timeScale = 1;
		isPaused = false;

		returnButton.SetActive(false);

		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
}
