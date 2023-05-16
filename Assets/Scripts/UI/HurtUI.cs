using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HurtUI : MonoBehaviour
{
    public List<Image> hurtScreens = new List<Image>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in hurtScreens)
        {
            item.enabled = false;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowHurtScreen(int index, float delay)
    {
        hurtScreens[index].enabled = true;
        yield return new WaitForSeconds(delay);
        hurtScreens[index].enabled = false;
    }
}
