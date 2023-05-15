using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpread : ISpell
{
    //public List<GameObject> spellParts;
    public float radius;
    public float delay;

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
        int numberOfIcicles = Random.Range(3, 8);
        icicles= new GameObject[numberOfIcicles];
        for (int i = 0; i < numberOfIcicles; i++)
        {
            MakeIcicle(origin, target, new Vector3(Mathf.Sin((float)i / numberOfIcicles * 2 * Mathf.PI) * radius, Mathf.Cos((float)i / numberOfIcicles * 2 * Mathf.PI) * radius, 0), i);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(delay);
        foreach (GameObject icicle in icicles)
        {
            if (icicle != null)
            {
                ShootIcicle(target, icicle);
            }
        }


    }

    private void MakeIcicle(Vector3 origin, Vector3 target, Vector3 offset, int order)
    {
        GameObject icicle = Instantiate(spell, origin, Quaternion.identity);
        icicle.GetComponent<Transform>().position += offset;
        icicle.transform.LookAt(target);
        icicles[order] = icicle;
        icicle.transform.parent = null;
    }

    private void ShootIcicle(Vector3 target, GameObject icicle)
    {
        //icicle.transform.rotation = Quaternion.LookRotation(target);
        icicle.transform.LookAt(target);
        icicle.GetComponent<Rigidbody>().AddForce(icicle.transform.forward*500*speed);
    }
}
