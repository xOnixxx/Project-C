using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    public GameObject[] explosionEffectParents;
    private ParticleSystem[] explosionEffect;
    private float timeToExplode = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (explosionEffectParents != null) {
            explosionEffect = new ParticleSystem[explosionEffectParents.Length];
            for (int i = 0; i < explosionEffect.Length; i++){
                explosionEffect[i] = explosionEffectParents[i].GetComponent<ParticleSystem>();
            }
        }
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
        if (loud)
        {
            foreach (ParticleSystem particle in explosionEffect)
            {
                timeToExplode = particle.main.duration > timeToExplode ? particle.main.duration : timeToExplode;
                particle.transform.parent = null;
                particle.Play();
            }
        }

        

        gameObject.GetComponent<MeshRenderer>().enabled= false;
        gameObject.GetComponent<MeshCollider>().enabled= false;
        yield return new WaitForSeconds(timeToExplode);

        for(int i = 0; i < explosionEffect.Length; i++)
        {
            Destroy(explosionEffect[i]);
            Destroy(explosionEffectParents[i]);
        }
        Destroy(gameObject);
    }
}
