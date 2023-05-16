using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFromAbove : AttackerAbstract
{
    public override void Attack(ISpell spell, Transform origin, Transform target, float dmgMultiplier)
    {
        spell.Cast(target.position + new Vector3(0, 30f, 0), target.position, dmgMultiplier);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
