using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public PlayerController player;
    public CastingMode caster;
    //Spell stats
    public Transform cameraHolder;
    List<bool> spellStatus = new List<bool>();
<<<<<<< HEAD
    public List<ISpell> spells; //CHANGE TO SPELL
=======
    public List<GameObject> spells; //CHANGE TO SPELL
    public bool isCasting = false;
>>>>>>> 719a61410bf61bb55a524e3adfdaf0763d3d4be7

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
        for (int i = 0; i < spells.Count; i++)
        {
            if (Input.GetKeyDown("" + (i + 1)))
            {
                Cursor.visible = true;
                player.enabled = false;
                isCasting = true;
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
                caster.StartCasting(1, 1, 0.9f, 8);
            }
        }
    }

    public void CheckForEnd()
    {
        if(caster.finishedCasting)
        {
            Debug.Log(caster.successfulPops);
            isCasting = false;
            player.enabled = true;
            Cursor.visible = false;
        }
    }
}
