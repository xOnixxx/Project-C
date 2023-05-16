using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackerAbstract : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Attack(ISpell spell, Transform origin, Transform target, float dmgMultiplier);
}
