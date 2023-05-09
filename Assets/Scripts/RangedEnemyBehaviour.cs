using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBehaviour : MonoBehaviour
{
    private CharacterController character;
    public Transform target;
    private Emitter emitter;
    public float attackRange = 10;
    public float movementSpeed = 2;
    public float gravity = 9.87f;
    private float verticalSpeed;
    // Start is called before the first frame update
    void Start()
    {
        emitter = GetComponent<Emitter>();
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
    }

    public void Chase()
    {
        if (character.isGrounded)
        {
            verticalSpeed = 0;
        }
        else { verticalSpeed -= gravity * Time.deltaTime; }
        transform.LookAt(target);
        Vector3 gravityVec = new Vector3(0, verticalSpeed, 0);
        Vector3 forwardMovement = new Vector3(0,0,0);
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity);
        if (Vector3.Distance(transform.position, target.position) > attackRange || hit.collider.gameObject.layer != 6)
        {
            forwardMovement = transform.forward;
        }
        else
        {
            emitter.Emit(target.position);
        }
        character.Move(movementSpeed * Time.deltaTime * forwardMovement + gravityVec * Time.deltaTime);
    }
}