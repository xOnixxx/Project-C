using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSpread : Spell
{
    public GameObject BIGspell;

    public float radius;
    public float delay;
    public float bigSize;

    private GameObject[] icicles;



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
        int numberOfIcicles = UnityEngine.Random.Range(3, 8);
        icicles = new GameObject[numberOfIcicles];
        for (int i = 0; i < numberOfIcicles; i++)
        {
            MakeIcicle(origin, target, new Vector3(Mathf.Sin((float)i / numberOfIcicles * 2 * Mathf.PI) * radius, Mathf.Cos((float)i / numberOfIcicles * 2 * Mathf.PI) * radius, 0), i);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);

        Func<float, float, float, float> move = (x, origin, target) => ((1 - (float)Math.Pow(1 - x, 5))*target)+origin;
        foreach (GameObject icicle in icicles)
        {
            icicle.GetComponent<ProjectileMove>().SmoothMove(move, new Vector3(0,0,5), 100);
        }
        GameObject BIGicicle = Instantiate(BIGspell, origin, Quaternion.identity, gameObject.transform);
        BIGicicle.transform.localScale = new Vector3(bigSize,bigSize,bigSize);
        BIGicicle.transform.LookAt(target);
        BIGicicle.transform.parent = null;
        yield return new WaitForSeconds(delay);
        foreach (GameObject icicle in icicles)
        {
            
            Destroy(icicle);
        }
        ShootIcicle(target, BIGicicle);

    }

    private void MakeIcicle(Vector3 origin, Vector3 target, Vector3 offset, int order)
    {
        GameObject icicle = Instantiate(spell, origin, Quaternion.identity, gameObject.transform);
        icicle.transform.LookAt(target);
        icicle.GetComponent<Transform>().localPosition += offset;
        icicles[order] = icicle;
        icicle.transform.parent = null;
    }

    private void ShootIcicle(Vector3 target, GameObject icicle)
    {
        icicle.transform.LookAt(target);
        icicle.GetComponent<Rigidbody>().AddForce(icicle.transform.forward * 500);
    }
}
