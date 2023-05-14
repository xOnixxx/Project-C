using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleDestroy : MonoBehaviour
{
    public GameObject[] explosionEffectParents;
    private ParticleSystem[] explosionEffect;
    private float timeToExplode = 0;
    public float timeToDie;
    // Start is called before the first frame update
    void Start()
    {
        if (explosionEffectParents != null) {
            explosionEffect = new ParticleSystem[explosionEffectParents.Length];
            for (int i = 0; i < explosionEffect.Length; i++){
                explosionEffect[i] = explosionEffectParents[i].GetComponent<ParticleSystem>();
            }
        }
        TimedDeath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Spells")
        {
            KillLoud();
        }
    }

    public void KillLoud()
    {
        StartCoroutine(Die(true));
    }

    public void KillSilent()
    {
        StartCoroutine(Die(false));
    }


    private IEnumerator Die(bool loud)
    {
        if (loud && explosionEffect != null)
        {

            for (int i = 0; i < explosionEffect.Length; i++)
            {
                if (explosionEffect[i] != null)
                {
                    ParticleSystem particle = explosionEffect[i];
                    timeToExplode = particle.main.duration > timeToExplode ? particle.main.duration : timeToExplode;
                    particle.transform.parent = null;
                    particle.Play();
                }
            }
        }

        

        gameObject.GetComponent<MeshRenderer>().enabled= false;
        gameObject.GetComponent<MeshCollider>().enabled= false;
        yield return new WaitForSeconds(timeToExplode);

        if (explosionEffect != null)
        {
            for(int i = 0; i < explosionEffect.Length; i++)
            {
                Destroy(explosionEffect[i]);
                Destroy(explosionEffectParents[i]);
            }
        }

        Destroy(gameObject);
    }

    private IEnumerator TimedDeath()
    {
        yield return new WaitForSeconds(timeToDie);
        KillSilent();
    }
}
