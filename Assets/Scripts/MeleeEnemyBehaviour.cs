using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehaviour : Character
{
    public GameHandler handler;
    private CharacterController character;
    public Transform target;
    public float movementSpeed = 2;
    public float gravity = 9.87f;
    public float attackRange = 1.5f;
    public float damage = 10;
    public ISpell.Element element;
    public float burnDamage = 0;
    public int burnTicks = 0;
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
        if (Vector3.Distance(transform.position, target.position) > attackRange)
        {
            transform.LookAt(target);
            character.Move(movementSpeed * Time.deltaTime * transform.forward + gravityVec * Time.deltaTime);
        }
        else
        {
            HealthManager toHit = target.gameObject.GetComponent<HealthManager>();
            if (toHit != null)
            {
                toHit.GetHit(damage, element, burnTicks, burnDamage);
            }
            Die();
        }
    }
    public override void Die()
    {
        handler.currentEnemyNumber--;
        //Destroy(gameObject);
        gameObject.GetComponent<EnemyDestroy>().KillLoud();
    }
}
