using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float damage = 10;
    public List<int> damageLayer = new List<int>();
    public int burnTicks = 0;
    public float burnDamage = 0;
    public ISpell.Element element = ISpell.Element.None;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDamager(float damage, List<int> damageLayer, int burnTicks = 0, float burnDamage = 0)
    {
        this.damage = damage;
        for (int i = 0; i < damageLayer.Count; i++)
        {
            this.damageLayer.Add(damageLayer[i]);
        }
        this.burnTicks = burnTicks;
        this.burnDamage = burnDamage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        if(damageLayer.Contains(hit.layer))
        {
            HealthManager health = hit.GetComponent<HealthManager>();
            if(health != null)
            {
                health.GetHit(damage, element, burnTicks,burnDamage);
                
            }
        }
    }
    
}
