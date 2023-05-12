using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public float damage = 10;
    public float strength = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        if(hit.layer == 6 || hit.layer == 7)
        {
            HealthManager health = hit.GetComponent<HealthManager>();
            if(health != null)
            {
                health.GetHit(damage);
            }
        }
        Destroy(gameObject);
    }
    
}
