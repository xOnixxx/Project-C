using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public float strength = 1;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        Rigidbody hitBody = collision.rigidbody;
        if(hitBody == null)
        {
            CharacterController character = hit.GetComponent<CharacterController>();
            if(character!= null)
            {
                character.Move(direction * strength);
            }
        }
        else
        {
            hitBody.AddForce(direction * strength);
        }
        Destroy(gameObject);
    }
}
