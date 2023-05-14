using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpawner : ISpell
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void Cast(Vector3 origin, Vector3 target, float dmg)
    {
        GameObject spawner = Instantiate(spell, origin, Quaternion.identity);
        spawner.transform.LookAt(target);
        spawner.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 50);
    }
}
