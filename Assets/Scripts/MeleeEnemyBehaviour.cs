using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehaviour : MonoBehaviour
{
    private CharacterController character;
    public Transform target;
    public float movementSpeed = 2;
    public float gravity = 9.87f;
    private float verticalSpeed;
    // Start is called before the first frame update
    void Start()
    {
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

        Vector3 gravityVec = new Vector3(0, verticalSpeed, 0);
        if (Vector3.Distance(transform.position,target.position) > 1)
        {
            transform.LookAt(target);
            character.Move(movementSpeed * Time.deltaTime * transform.forward + gravityVec * Time.deltaTime);
        }
    }
}
