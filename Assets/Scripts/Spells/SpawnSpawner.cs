using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpawner : Spell
{
    public GameObject spell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public override void Cast(Vector3 origin, Vector3 target, float dmg)
    {
        GameObject spawner = Instantiate(spell, cameraHolder.transform.position, cameraHolder.transform.rotation);
        spawner.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 50);
    }
}
