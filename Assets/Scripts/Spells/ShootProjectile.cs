using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : ISpell
{
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Cast(Vector3 origin, Vector3 target, float playerMultiplier)
    {
        GameObject icicle = Instantiate(spell, origin, Quaternion.identity);
        if (icicle.GetComponent<Damager>() != null )
        {
            icicle.GetComponent<Damager>().SetDamager(damage, dmgLayer);
        }      
        icicle.transform.LookAt(target);
        icicle.GetComponent<Rigidbody>().AddForce(icicle.transform.forward * speed);
    }
}
