using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnDelay : MonoBehaviour
{
    [SerializeField] float delayTimer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTimer);

        Destroy(this.gameObject);
    }
}
