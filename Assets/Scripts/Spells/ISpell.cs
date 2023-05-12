using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public float damage;
    public float dmgMultiplier;
    public int dmgLayer;

    public float spawnRate;
    public float lifeTime;
    public float changeCoef;
    public float cooldown;
    public int nodeNum;

    public Transform cameraHolder;
    public abstract void Cast(Vector3 origin, Vector3 target, float playerMultiplier);

}
