using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISpell : MonoBehaviour
{
    public enum Element
    {
        None,
        Ice,
        Fire,
        Light,
        Earth
    }

    public float damage;
    public float dmgMultiplier;
    public List<int> dmgLayer = new List<int>();

    public string spellName;
    public Element element = Element.None;
    public Sprite spellSprite;
    public List<Vector2> nodeCastPositions = new List<Vector2>();
    public List<float> lifetimes = new List<float>();
    public List<float> spawnDelays = new List<float>();

    public float size = 1;
    public float speed = 1;
    public int burnTicks = 0;
    public float burnDamagePerTick = 0;
    public Vector3 offset = Vector3.zero;
    public GameObject spell;

    protected GameObject anchor;
    

    public abstract void Cast(Vector3 origin, Vector3 target, float playerMultiplier);

}
