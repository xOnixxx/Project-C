using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    public PlayerController player;
    private RectTransform staminaBar;
    // Start is called before the first frame update
    void Start()
    {
        staminaBar = GetComponent<RectTransform>();
        staminaBar.sizeDelta = new Vector2(player.currentStamina / player.maxStamina * 300, 30);
    }

    // Update is called once per frame
    void Update()
    {
        staminaBar.sizeDelta = new Vector2(player.currentStamina / player.maxStamina * 300, 30);
    }
}
