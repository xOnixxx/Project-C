using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectAttack : AttackerAbstract
{
    public override void Attack(ISpell spell, Transform origin, Transform target, float dmgMultiplier)
    {
        spell.Cast(origin.position + origin.forward * origin.localScale.z + new Vector3(0, 2f, 0) * origin.localScale.y / 2, target.position, dmgMultiplier);
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
