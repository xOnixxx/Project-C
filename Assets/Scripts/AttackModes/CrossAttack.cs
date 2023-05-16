using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossAttack : AttackerAbstract
{
    public override void Attack(ISpell spell, Transform origin, Transform target, float dmgMultiplier)
    {
        Vector3 side = Vector3.Cross(origin.forward, origin.up) + new Vector3(0,0.2f,0);
        spell.Cast(target.position + side * 30, target.position, dmgMultiplier);
        spell.Cast(target.position - side * 30, target.position, dmgMultiplier);
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
