using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : ISpell
{

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
        GameObject projectile = Instantiate(spell, origin, Quaternion.identity, anchor.transform);
        projectile.transform.localPosition += offset;
        projectile.transform.localScale *= size;
        if (projectile.GetComponent<Damager>() != null ){projectile.GetComponent<Damager>().SetDamager(damage, dmgLayer, element, burnTicks,burnDamagePerTick);}
        if (projectile.GetComponent<AudioSource>() != null) {
            //Debug.Log(GetComponent<ProjectileAudioManager>().sounds);
            projectile.GetComponent<AudioSource>().PlayOneShot(projectile.GetComponent<ProjectileAudioManager>().sounds[0]);
        }
        projectile.transform.parent= null;    
        projectile.transform.LookAt(target);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed * 100);

        Destroy(anchor);
    }
}
