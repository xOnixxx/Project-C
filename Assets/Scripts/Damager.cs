using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float damage = 10;
    public int damageLayer = 7;
    public ISpell.Element element = ISpell.Element.None;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDamager(float damage, int damageLayer)
    {
        this.damage = damage;
        this.damageLayer = damageLayer;
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        if(hit.layer == damageLayer)
        {
            HealthManager health = hit.GetComponent<HealthManager>();
            if(health != null)
            {
                health.GetHit(damage,element);
            }
        }
    }
    
}
