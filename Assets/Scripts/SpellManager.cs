using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public PlayerController player;
    public CastingMode caster;
    //Spell stats
    public Transform cameraHolder;
    public List<bool> spellStatus = new List<bool>();


    public List<Spell> spells = new List<Spell>();
    private int chargedSpellIndex = -2;
    public bool isCasting = false;


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
        if (isCasting)
        {
            CheckForEnd();
        }
        else
        {
            CheckForCasting();
        }
    }

    private IEnumerator Cooldown(float delay, int indexChanged)
    {
        yield return new WaitForSeconds(delay);
        spellStatus[indexChanged] = !spellStatus[indexChanged];
    }

    public void CheckForCasting()
    {
        if (chargedSpellIndex > -1 && Input.GetButtonDown("Fire1"))
        {
            RaycastHit closestHit;
            Vector3 origin = transform.position + new Vector3(0, 1, 0);
            Vector3 target;
            if (Physics.Raycast(origin, cameraHolder.forward, out closestHit, Mathf.Infinity))
            {
                target = closestHit.point;
            }
            else
            {
                target = transform.position + cameraHolder.forward * 1000;
            }
            spells[chargedSpellIndex].Cast(origin, target,caster.successfulPops);
            chargedSpellIndex = -2;
        }
        for (int i = 0; i < spells.Count; i++)
        {
            if (Input.GetKeyDown("" + (i + 1)))
            {
                Cursor.visible = true;
                player.enabled = false;
                isCasting = true;
                caster.StartCasting(spells[i].spawnRate, spells[i].lifeTime, spells[i].changeCoef, spells[i].nodeNum);
                chargedSpellIndex = i;
            }
        }  
    }

    public void CheckForEnd()
    {
        if(caster.finishedCasting)
        {
            if (caster.successfulPops == 0)
            {
                chargedSpellIndex = -2;
            }
            isCasting = false;
            player.enabled = true;
            Cursor.visible = false;
        }
    }
}
