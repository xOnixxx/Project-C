using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileSpread : ISpell
{
    public float radius;
    public float delayBeforeShooting;
    public float delayBetweenShots;
    public float delayBetweenSpawn;

    public bool wait;

    public int minNumShots;
    public int maxNumShots;

    private GameObject[] icicles;

    //TEMP
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public override void Cast(Vector3 origin, Vector3 target, float Multiplier)
    {
        StartCoroutine(RevolverCast(origin, target, Multiplier));
    }

    public IEnumerator RevolverCast(Vector3 origin, Vector3 target, float Multiplier)
    {
        anchor = new GameObject();
        anchor.transform.position = origin;
        anchor.transform.LookAt(target);
        int numberOfShots = Random.Range(minNumShots, maxNumShots);
        icicles= new GameObject[numberOfShots];

        StartCoroutine(Spawn(origin, target, numberOfShots));
        yield return new WaitForSeconds(delayBeforeShooting*numberOfShots + delayBetweenSpawn*numberOfShots + delayBetweenShots*numberOfShots + 1);

        Destroy(anchor);
    }

    private IEnumerator Spawn(Vector3 origin, Vector3 target, int numberOfShots)
    {
        for (int i = 0; i < numberOfShots; i++)
        {
            MakeIcicle(origin, target, new Vector3(Mathf.Sin((float)i / numberOfShots * 2 * Mathf.PI) * radius, Mathf.Cos((float)i / numberOfShots * 2 * Mathf.PI) * radius, 0), i, numberOfShots);
            yield return new WaitForSeconds(delayBetweenSpawn);
        }
    }

    private IEnumerator Shoot(Vector3 target, GameObject projectile, int order, int numOfProjectiles)
    {
        //yield return new WaitForSeconds(delayBetweenShots + delayBeforeShooting);
        if (wait){yield return new WaitForSeconds(delayBetweenSpawn*((float)numOfProjectiles-(float)order) + delayBeforeShooting);}

        else { yield return new WaitForSeconds(delayBetweenShots + delayBeforeShooting); }
        ShootIcicle(target, projectile);
    }


    private void MakeIcicle(Vector3 origin, Vector3 target, Vector3 offset, int order, int numOfProjectiles)
    {
        GameObject projectile = Instantiate(spell, origin, Quaternion.identity, anchor.transform);
        projectile.GetComponent<Transform>().localPosition += offset;
        projectile.transform.LookAt(target);
        projectile.transform.parent = null;
        if (projectile.GetComponent<Damager>() != null)
        {
            projectile.GetComponent<Damager>().SetDamager(damage, dmgLayer, burnTicks, burnDamagePerTick);
        }
        StartCoroutine(Shoot(target, projectile, order, numOfProjectiles));
    }

    private void ShootIcicle(Vector3 target, GameObject projectile)
    {
        //icicle.transform.rotation = Quaternion.LookRotation(target);
        projectile.transform.LookAt(target);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward*500*speed);

    }
}
