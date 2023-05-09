using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    //Spell stats
    public Transform cameraHolder;
    List<bool> spellStatus = new List<bool>();
    public List<Spell> spells;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spells.Count; i++)
        {
            spellStatus.Add(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForCasting();
    }

    private IEnumerator Cooldown(float delay, int indexChanged)
    {
        yield return new WaitForSeconds(delay);
        spellStatus[indexChanged] = !spellStatus[indexChanged];
    }

    public void CheckForCasting()
    {
        for (int i = 0; i < spells.Count; i++)
        {
            if (Input.GetKeyDown("" + (i + 1)))
            {
                RaycastHit closestHit;
                Vector3 origin = transform.position + new Vector3(0, 4, 0);
                Vector3 target;
                if (Physics.Raycast(origin, cameraHolder.forward, out closestHit, Mathf.Infinity))
                {
                    target = closestHit.point;
                }
                else
                {
                    target = transform.position + cameraHolder.forward * 1000;
                }
                spells[i].Shoot(origin,target);
            }
        }
    }
}
