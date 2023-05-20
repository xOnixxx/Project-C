using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float resist = 0;
    //public float invulnerabilityTime = 0.2f;
    public bool canBeInvulnerable = false;
    private bool isInvulnerable = false;
    private bool alreadyTakingDoT = false;
    private bool isPlayer;
    public Character charController;
    public HurtUI hurtImages;
    private ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        if(charController is PlayerController)
        {
            isPlayer = true;
        }
        else
        {
            particles = GetComponent<ParticleSystem>();
            isPlayer = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHit(float damage, ISpell.Element element, int burnTicks, float burnDamage)
    {
        if(!isInvulnerable)
        {
            /*if(canBeInvulnerable)
            {
                isInvulnerable = true;
                StartCoroutine(InvulnerabilityFrames());
            }*/
            currentHealth -= (damage - resist * damage);
            switch (element)
            {
                case ISpell.Element.None:
                    if (!isPlayer)
                    {
                        particles.Play();
                    }
                    break;
                case ISpell.Element.Ice:
                    if (!alreadyTakingDoT)
                    {
                        alreadyTakingDoT = true;
                        if(isPlayer)
                        {
                            StartCoroutine(hurtImages.ShowHurtScreen(0, burnTicks * 0.2f + 0.3f));
                        }
                        if (!isPlayer)
                        {
                            particles.Play();
                        }
                        StartCoroutine(DamageOverTime(0.2f, burnDamage, 0, burnTicks));
                    }
                    break;
                case ISpell.Element.Fire:
                    if (!alreadyTakingDoT)
                    {
                        if (isPlayer)
                        {
                            StartCoroutine(hurtImages.ShowHurtScreen(1, burnTicks * 0.2f + 0.3f));
                        }
                        if (!isPlayer)
                        {
                            particles.Play();
                        }
                        alreadyTakingDoT = true;
                        StartCoroutine(DamageOverTime(0.2f ,burnDamage, 0, burnTicks));
                    }
                    break;
                case ISpell.Element.Light:
                    if (isPlayer)
                    {
                        StartCoroutine(hurtImages.ShowHurtScreen(2, 0.3f));
                    }
                    if (!isPlayer)
                    {
                        particles.Play();
                    }
                    break;
                case ISpell.Element.Earth:
                    if (isPlayer)
                    {
                        StartCoroutine(hurtImages.ShowHurtScreen(3, 1.3f));
                    }
                    if (!isPlayer)
                    {
                        particles.Play();
                    }
                    StartCoroutine(Stun(1));
                    break;
                default:
                    break;
            }
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }
    }

    public void Die()
    {
        charController.Die();
    }

    /*private IEnumerator InvulnerabilityFrames()
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }*/

    private IEnumerator Stun(float stunLength)
    {
        charController.enabled = false;
        yield return new WaitForSeconds(stunLength);
        charController.enabled = true;
    }
    private IEnumerator DamageOverTime(float tickRate,float tickDamage,int depth, int maxDepth)
    {
        GetHit(tickDamage, ISpell.Element.None,0,0);
        yield return new WaitForSeconds(tickRate);
        if(depth < maxDepth)
        {
            StartCoroutine(DamageOverTime(tickRate ,tickDamage, depth + 1, maxDepth));
        }
        else
        {
            alreadyTakingDoT = false;
        }
    }
}
