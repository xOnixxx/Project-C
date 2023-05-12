using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class IcicleSpawner : Spell
{
    public GameObject spell;
    public int numInBurst;
    private shootIcicle shooter;
    // Start is called before the first frame update
    void Start()
    {
        shooter = new shootIcicle();
        shooter.spell = spell;
        shooter.cameraHolder = gameObject.transform;
        shooter.damage = damage;
        shooter.dmgMultiplier = dmgMultiplier;
        shooter.dmgLayer = dmgLayer;
        Vector3 origin = cameraHolder.transform.position + new Vector3(0, 0.1f, 3);
        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 200);
        Cast(origin, new Vector3(0, 0, 0), dmgMultiplier); 
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.O))
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
        */
    }

    public override void Cast(Vector3 origin, Vector3 target, float playerMultiplier)
    {
        StartCoroutine(ShootRandom(origin, new Vector3(0,0,0), playerMultiplier));
    }

    private IEnumerator ShootRandom(Vector3 origin, Vector3 target, float playerMultiplier)
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            {
                for (int i = 0; i < numInBurst; i++)
                {
                    shooter.Cast(origin, Random.insideUnitSphere*500, playerMultiplier);
                }

            }
        }

    }

}
