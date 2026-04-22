using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    public bool isPaused = false;

	public float slowEffect = 0.5f;
	public bool isSlowed;

	//lower transition speed = longer time in between 1 and slowEffect
	public float transitionSpeed = 4f;
	private float targetTimeScale = 1f;

    private void OnEnable()
    {
        PauseMenu.OnPause += Pause;
    }

    private void OnDisable()
    {
        PauseMenu.OnResume += Resume;
    }


    void Awake()
	{
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }

        Instance = this;

    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

		if (PlayerInput.Instance.stasisSearchPressed && !PauseMenu.Instance.IsPaused)
		{
			targetTimeScale = slowEffect;
			isSlowed = true;
		}
		else
		{
			targetTimeScale = 1f;
			isSlowed = false;
		}

		if(!PauseMenu.Instance.IsPaused && Time.timeScale != targetTimeScale)
		{
			//using a lerp to smooth in and out of slowed time
			Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, Time.unscaledDeltaTime * transitionSpeed);
			//adjusts objects physics and scales them based on our current time scale
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
		}
		
    }

	private void SetTimeScale(float timeScale)
	{
		targetTimeScale = timeScale;
	}

    public void Pause()
	{
		Time.timeScale = 0f;
		isPaused = true;
	}

	public void Resume()
	{
		Time.timeScale = 1;
		isPaused = false;
	}
}
