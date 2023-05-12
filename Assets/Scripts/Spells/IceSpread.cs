using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpread : Spell
{
    private shootIcicle shooter;
    //public List<GameObject> spellParts;
    public GameObject spell;

    private GameObject[] icicles;

    //TEMP
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
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
        int numberOfIcicles = Random.Range(3, 8);
        icicles= new GameObject[numberOfIcicles];
        for (int i = 0; i < numberOfIcicles; i++)
        {
            MakeIcicle(origin, target, new Vector3(Mathf.Sin((float)i / numberOfIcicles * 2 * Mathf.PI) * 0.5f, Mathf.Cos((float)i / numberOfIcicles * 2 * Mathf.PI) * 0.5f, 0), i);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);
        foreach (GameObject icicle in icicles)
        {
            ShootIcicle(target, icicle);
        }
    }

    private void MakeIcicle(Vector3 origin, Vector3 target, Vector3 offset, int order)
    {
        GameObject icicle = Instantiate(spell, cameraHolder.transform.position, cameraHolder.transform.rotation, cameraHolder.transform);
        icicle.GetComponent<Transform>().localPosition = icicle.GetComponent<Transform>().localPosition + new Vector3(0,0,3) + offset;
        icicles[order] = icicle;
    }

    private void ShootIcicle(Vector3 target, GameObject icicle)
    {
        //icicle.transform.rotation = Quaternion.LookRotation(target);
        icicle.transform.LookAt(target);
        icicle.transform.parent = null;
        icicle.GetComponent<Rigidbody>().AddForce(icicle.transform.forward*500);
    }
}
