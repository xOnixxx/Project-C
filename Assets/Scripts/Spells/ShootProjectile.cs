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
        anchor = new GameObject();
        anchor.transform.position = origin;
        anchor.transform.LookAt(target);
        GameObject icicle = Instantiate(spell, origin, Quaternion.identity, anchor.transform);
        icicle.transform.localPosition += offset; 
        if (icicle.GetComponent<Damager>() != null )
        {
            icicle.GetComponent<Damager>().SetDamager(damage, dmgLayer);
        }
        icicle.transform.parent= null;    
        icicle.transform.LookAt(target);
        icicle.GetComponent<Rigidbody>().AddForce(icicle.transform.forward * speed * 100);

        Destroy(anchor);
    }
}
