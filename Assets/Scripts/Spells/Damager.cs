using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float damage;
    public int dmgLayer;

    public void SetDamager(float damage, int dmgLayer)
    {
        this.damage = damage;
        this.dmgLayer = dmgLayer;
    }

    public void OnCollisionEnter(Collision collision)
    {
        //TODO if collision is enemy
        /*
        {
            float resist = collision.resist();
            collision.hp.dealdmg(dmgMuliplier * resist * damage);
        }
        */
    }

}
