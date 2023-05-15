using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileSpread : ISpell
{
    //public List<GameObject> spellParts;
    public float radius;
    public float delayBeforeShooting;
    public float delayBetweenShots;
    public float delayBetweenSpawn;

    public bool wait;

    public int minNumShots;
    public int maxNumShots;

    private GameObject[] icicles;
    private GameObject anchor;

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
        int numberOfIcicles = Random.Range(minNumShots, maxNumShots);
        icicles= new GameObject[numberOfIcicles];

        StartCoroutine(Spawn(origin, target, numberOfIcicles));
        yield return new WaitForSeconds(delayBeforeShooting*numberOfIcicles);

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
        if (wait){yield return new WaitForSeconds(delayBeforeShooting*((float)numOfProjectiles-(float)order) + delayBetweenShots);}

        else { yield return new WaitForSeconds(delayBetweenShots + delayBeforeShooting); }
        ShootIcicle(target, projectile);
    }


    private void MakeIcicle(Vector3 origin, Vector3 target, Vector3 offset, int order, int numOfProjectiles)
    {
        GameObject icicle = Instantiate(spell, origin, Quaternion.identity, anchor.transform);
        icicle.GetComponent<Transform>().localPosition += offset;
        icicle.transform.LookAt(target);
        icicles[order] = icicle;
        icicle.transform.parent = null;
    
        StartCoroutine(Shoot(target, icicle, order, numOfProjectiles));
    }

    private void ShootIcicle(Vector3 target, GameObject icicle)
    {
        //icicle.transform.rotation = Quaternion.LookRotation(target);
        icicle.transform.LookAt(target);
        icicle.GetComponent<Rigidbody>().AddForce(icicle.transform.forward*500*speed);

    }
}
