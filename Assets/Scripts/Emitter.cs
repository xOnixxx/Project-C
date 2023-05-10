using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    public GameObject objectToEmit;

    public float bulletSpeed = 1000;
    public float currentSpread = 0;
    public int bulletsperShot = 1;
    public bool canShoot = true;
    public float attackDelay = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Emit(Vector3 target)
    {
        if (canShoot)
        {
            canShoot = false;
            for (int i = 0; i < bulletsperShot; i++)
            {
                GameObject emitted = Instantiate(objectToEmit, transform.position + transform.forward, Quaternion.identity);
                Rigidbody body = emitted.GetComponent<Rigidbody>();
                if (body != null)
                {
                    emitted.transform.LookAt(target + Random.insideUnitSphere * currentSpread);
                    body.AddForce(emitted.transform.forward * bulletSpeed);
                }
            }
            StartCoroutine(RefreshDelay());
        }
    }
    private IEnumerator RefreshDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        canShoot = true;
    }
}
