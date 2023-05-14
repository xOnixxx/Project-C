using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public Text levelNumber;
    public Text levelInfo;
    public GameHandler infoSource;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        levelNumber.text = "Level : " + (1 + infoSource.levelsPassed);
        levelInfo.text = "Debug"; //ADD HAZARD INFO
    }
}
