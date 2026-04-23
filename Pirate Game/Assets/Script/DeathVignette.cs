using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVignette : MonoBehaviour
{
    [SerializeField] private RectTransform vignetteTransform;
    [SerializeField] private GameObject vignetteCap;

    [SerializeField] private float startingScale = 280f;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float desiredScale;

    
    // Start is called before the first frame update
    void Start()
    {
        desiredScale = startingScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (vignetteTransform.localScale.x != desiredScale)
        {
            float newScale = Mathf.MoveTowards(vignetteTransform.localScale.x, desiredScale, Time.deltaTime * speed);
            vignetteTransform.localScale = new Vector3(newScale, newScale, vignetteTransform.localScale.z);
        }

        if (vignetteTransform.localScale.x == 1)
        {
            vignetteCap.SetActive(true);
        }
        else if(vignetteCap.activeSelf)
        {
            vignetteCap.SetActive(false);
        }
    }

    public void FadeIn()
    {
        desiredScale = 1;
    }

    public void FadeOut() 
    {
        desiredScale = startingScale;
    }
}
