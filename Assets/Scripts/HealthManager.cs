using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private enum Element
    { 
        None,
        Ice,
        Fire,
        Light,
        Earth
    }
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float resist = 0;
    public float invulnerabilityTime = 0.2f;
    public bool canBeInvulnerable = true;
    private bool isInvulnerable = false;
    public Character charController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHit(float damage)
    {
        if(!isInvulnerable)
        {
            if(canBeInvulnerable)
            {
                //isInvulnerable = true;
                //StartCoroutine(InvulnerabilityFrames());
            }
            currentHealth -= (damage - resist * damage);
            StartCoroutine(DamageOverTime(0.2f, 1, 0, 20));
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator InvulnerabilityFrames()
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }

    private IEnumerator Stun(float stunLength)
    {
        charController.enabled = false;
        yield return new WaitForSeconds(stunLength);
        charController.enabled = true;
    }
    private IEnumerator DamageOverTime(float tickRate, float tickDamage,int depth, int maxDepth)
    {
        GetHit(tickDamage);
        yield return new WaitForSeconds(tickRate);
        if(depth < maxDepth)
        {
            StartCoroutine(DamageOverTime(tickRate, tickDamage, depth + 1, maxDepth));
        }
    }
}
