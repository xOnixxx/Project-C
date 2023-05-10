using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{
    public float damage { get; set;}
    public float dmgMultiplier { get; set; }
    public int dmgLayer { get; set; }
    public void Cast(Vector3 origin, Vector3 target);

}
