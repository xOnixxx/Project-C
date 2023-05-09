using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public GameObject toCast;
    private GameObject spellShot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Vector3 origin, Vector3 direction)
    {
        spellShot = Instantiate(toCast, origin, Quaternion.identity);
        spellShot.transform.LookAt(direction);
        spellShot.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit ground");
        Destroy(spellShot);
    }
}
