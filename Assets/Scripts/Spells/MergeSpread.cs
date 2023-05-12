using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSpread : Spell
{
    private ShootProjectile shooter;
    //public List<GameObject> spellParts;
    public GameObject spell;
    public float radius;

    private GameObject[] icicles;

    //TEMP
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
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
        Debug.Log("HERE");

        Func<float, float, float, float> move = (x, origin, target) => ((1 - (float)Math.Pow(1 - x, 5))*target)+origin;
        foreach (GameObject icicle in icicles)
        {
            icicle.GetComponent<ProjectileMove>().SmoothMove(move, new Vector3(0,0,8), 100);
            icicle.GetComponent<ProjectileMove>().SmoothSize(move, new Vector3(5, 5, 5), 100);
            //icicle.transform.localPosition = //new Vector3(0, 0, 3);
        }
        GameObject BIGicicle = Instantiate(spell, cameraHolder.transform.position, cameraHolder.transform.rotation, cameraHolder.transform);
        BIGicicle.GetComponent<Transform>().localPosition = BIGicicle.GetComponent<Transform>().localPosition + new Vector3(0, 0, 8);
        BIGicicle.GetComponent<Transform>().localScale = new Vector3(5,5,5);
        yield return new WaitForSeconds(1f);
        foreach (GameObject icicle in icicles)
        {
            Destroy(icicle);
        }
        ShootIcicle(target, BIGicicle);

    }

    private void MakeIcicle(Vector3 origin, Vector3 target, Vector3 offset, int order)
    {
        GameObject icicle = Instantiate(spell, cameraHolder.transform.position, cameraHolder.transform.rotation, cameraHolder.transform);
        icicle.GetComponent<Transform>().localPosition = icicle.GetComponent<Transform>().localPosition + new Vector3(0, 0, 3) + offset;
        icicles[order] = icicle;
    }

    private void ShootIcicle(Vector3 target, GameObject icicle)
    {
        //icicle.transform.rotation = Quaternion.LookRotation(target);
        icicle.transform.LookAt(target);
        icicle.transform.parent = null;
        icicle.GetComponent<Rigidbody>().AddForce(icicle.transform.forward * 500);
    }
}
