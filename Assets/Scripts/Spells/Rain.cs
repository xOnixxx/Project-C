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
                    shooter.Cast(gameObject.transform.position, gameObject.transform.position + new Vector3(0,-1,0) , playerMultiplier);
                }
            }
        }
    }
}
