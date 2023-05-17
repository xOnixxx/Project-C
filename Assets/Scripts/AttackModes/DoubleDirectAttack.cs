using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDirectAttack : AttackerAbstract
{
    public override void Attack(ISpell spell, Transform origin, Transform target, float dmgMultiplier)
    {
        Vector3 baseOffset = origin.forward * origin.lossyScale.z + new Vector3(0, 2.5f, 0) * origin.lossyScale.y / 2;
        Vector3 sideOffset = Vector3.Cross(origin.forward, origin.up) * origin.lossyScale.x * 1f;
        spell.Cast(origin.position + baseOffset + sideOffset, target.position, dmgMultiplier);
        spell.Cast(origin.position + baseOffset - sideOffset, target.position, dmgMultiplier);
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
