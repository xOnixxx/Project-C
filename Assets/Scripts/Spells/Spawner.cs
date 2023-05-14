using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class Spawner : ISpell
{
    public ShootProjectile shooterPrefab;
    public int burstAmount;
    public float burstInterval;
    public float speedProjectile;
    public float speedSpawner;

    protected ShootProjectile shooter;
    // Start is called before the first frame update
    void Start()
    {
        shooter = Instantiate<ShootProjectile>(shooterPrefab);
        shooter.spell = spell;
        shooter.damage = damage;
        shooter.dmgMultiplier = dmgMultiplier;
        shooter.dmgLayer = dmgLayer;
        shooter.speed = speedProjectile;
        Vector3 origin = gameObject.transform.position;
        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * speedSpawner);
        Cast(origin, new Vector3(0, 0, 0), dmgMultiplier); 
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void Cast(Vector3 origin, Vector3 target, float playerMultiplier)
    {
        StartCoroutine(ShootRandom(origin, new Vector3(0,0,0), playerMultiplier));
    }

    public virtual IEnumerator ShootRandom(Vector3 origin, Vector3 target, float playerMultiplier)
    {
        while (true)
        {
            yield return new WaitForSeconds(burstInterval);
            {
                for (int i = 0; i < burstAmount; i++)
                {
                    shooter.Cast(gameObject.transform.position, Random.insideUnitSphere*5000, playerMultiplier);
                }
            }
        }
    }

}
