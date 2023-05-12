using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : Spell
{
    public GameObject spell;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            RaycastHit closestHit;
            Vector3 origin = cameraHolder.transform.position + new Vector3(0, 0.1f, 3);
            //Debug.Log(origin);
            Vector3 target;
            if (Physics.Raycast(origin, cameraHolder.forward, out closestHit, Mathf.Infinity))
            {
                target = closestHit.point;
            }
            else
            {
                target = transform.position + cameraHolder.forward * 1000;
            }
            Cast(origin, target, 0);
        }
    }

    public override void Cast(Vector3 origin, Vector3 target, float playerMultiplier)
    {
        GameObject icicle = Instantiate(spell, cameraHolder.transform.position, cameraHolder.transform.rotation, cameraHolder.transform);
        icicle.GetComponent<Transform>().localPosition = icicle.GetComponent<Transform>().localPosition + new Vector3(0, 0, 3);
        if (icicle.GetComponent<Damager>() != null )
        {
            icicle.GetComponent<Damager>().SetDamager(damage, dmgLayer);
        }

       
        icicle.transform.parent = null;
        icicle.transform.LookAt(target);
        icicle.GetComponent<Rigidbody>().AddForce(icicle.transform.forward * speed);
    }
}
