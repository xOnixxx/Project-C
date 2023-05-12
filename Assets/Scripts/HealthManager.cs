using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float resist = 0;
    public float invulnerabilityTime = 0.2f;
    public bool canBeInvulnerable = true;
    private bool isInvulnerable = false;

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
}
