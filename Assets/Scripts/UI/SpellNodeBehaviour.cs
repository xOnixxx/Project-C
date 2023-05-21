using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellNodeBehaviour : MonoBehaviour
{
    private bool playerHit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        playerHit= true;
    }

    public bool getHit()
    {
        return playerHit;
    }
}
