using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceOnHit : MonoBehaviour
{
    public GameObject explosionParent;
    private ParticleSystem explosionEffect;
    private float timeToExplode;
    // Start is called before the first frame update
    void Start()
    {
        explosionEffect = explosionParent.GetComponent<ParticleSystem>();
        timeToExplode = explosionEffect.main.duration;
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Spells")
        {
            Debug.Log(collision.gameObject.name);
            StartCoroutine(Die());
        }

    }

    private IEnumerator Die()
    {
        Debug.Log("Here");
        explosionParent.transform.parent = null;
        explosionEffect.Play();
        Destroy(gameObject);

        gameObject.GetComponent<MeshRenderer>().enabled= false;
        gameObject.GetComponent<MeshCollider>().enabled= false;
        yield return new WaitForSeconds(timeToExplode);

    }
}
