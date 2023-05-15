using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : Spawner
{


    public override IEnumerator ShootRandom(Vector3 origin, Vector3 target, float playerMultiplier)
    {
        while (true)
        {
            yield return new WaitForSeconds(burstInterval);
            {
                for (int i = 0; i < burstAmount; i++)
                {
                    yield return new WaitForSeconds(Random.value);
                    Vector3 randomSpawn = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                    shooter.Cast(gameObject.transform.position + randomSpawn, gameObject.transform.position +  randomSpawn +new Vector3(0,-1,0) + gameObject.transform.forward.normalized , playerMultiplier);
                }
            }
        }
    }
}
