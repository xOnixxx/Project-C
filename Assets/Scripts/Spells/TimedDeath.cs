using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeath : MonoBehaviour
{

    public float timeToLive;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimedInnerDeath());
    }

    private IEnumerator TimedInnerDeath()
    {
        yield return new WaitForSeconds(timeToLive);
        gameObject.GetComponent<ParticleDestroy>().KillLoud();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
