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
    public int dmgLayer;

    public string spellName;
    public Element element = Element.None;
    public Sprite spellSprite;
    public List<Vector2> nodeCastPositions = new List<Vector2>();
    public List<float> lifetimes = new List<float>();
    public List<float> spawnDelays = new List<float>();

    public float speed = 1;
    public GameObject spell;

    public abstract void Cast(Vector3 origin, Vector3 target, float playerMultiplier);

}
