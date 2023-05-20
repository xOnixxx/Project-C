using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    public PlayerController player;
    public CastingMode caster;
    //Spell stats
    public Transform cameraHolder;


    public List<ISpell> spells = new List<ISpell>();
    public Image chargedSpellImage;
    public Text chargedSpellText;
    public Text tooltip;
    private int chargedSpellIndex = -2;
    public bool isCasting = false;


    // Start is called before the first frame update
    void Start()
    {
        Hide();
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

    public void CheckForCasting()
    {
        if (chargedSpellIndex > -1 && Input.GetButtonDown("Fire1"))
        {
            RaycastHit closestHit;
            Vector3 origin = transform.position;
            Vector3 target;
            if (Physics.Raycast(origin, cameraHolder.forward, out closestHit, Mathf.Infinity))
            {
                target = closestHit.point;
            }
            else
            {
                target = transform.position + cameraHolder.forward * 1000;
            }
            spells[chargedSpellIndex].Cast(origin + 2 * transform.forward, target,(float)caster.successfulPops / spells[chargedSpellIndex].nodeCastPositions.Count);
            Hide();
            chargedSpellIndex = -2;
        }
        for (int i = 0; i < spells.Count; i++)
        {
            if (Input.GetKeyDown("" + (i + 1)))
            {
                Cursor.visible = true;
                player.enabled = false;
                isCasting = true;
                caster.StartCasting(spells[i].nodeCastPositions, spells[i].lifetimes, spells[i].spawnDelays, spells[i].element);
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
            else
            {
                chargedSpellImage.sprite = spells[chargedSpellIndex].spellSprite;
                chargedSpellImage.enabled = true;
                chargedSpellText.text = spells[chargedSpellIndex].spellName + " charged (" + ((float)caster.successfulPops / spells[chargedSpellIndex].nodeCastPositions.Count).ToString("##.##") + ")";
                chargedSpellText.enabled = true;
                tooltip.enabled = true;
            }
            isCasting = false;
            player.enabled = true;
            Cursor.visible = false;
        }
    }

    public void Hide()
    {
        chargedSpellImage.enabled = false;
        chargedSpellText.enabled = false;
        tooltip.enabled = false;
    }    
}
