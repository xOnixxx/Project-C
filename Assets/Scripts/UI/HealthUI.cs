using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public HealthManager health;
    private RectTransform healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<RectTransform>();
        healthBar.sizeDelta = new Vector2(health.currentHealth / health.maxHealth * 300, 30);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.sizeDelta = new Vector2((float)health.currentHealth / health.maxHealth * 300, 30);
    }
}
