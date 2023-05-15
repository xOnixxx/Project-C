using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentalHazards : MonoBehaviour
{

    public Texture2D levelTexture;
    public Texture2D groundTexture;
    public TreeInstance[] trees;
    
    public ISpell[] allowedPlayerEl;
    public ISpell[] allowedEnemyEl;
    public Dictionary<ISpell.Element, float> elementalDmgUp;
    public int numOfObstacles;
    public int numOfEnemies;

    private Texture2D levelTextureOG;
    private Texture2D groundTextureOG;
    private TreeInstance[] treesOG;

    private ISpell[] allowedPlayerElOG;
    private ISpell[] allowedEnemyElOG;
    private Dictionary<ISpell.Element, float> elementalDmgUpOG;
    private int numOfObstaclesOG;
    private int numOfEnemiesOG;

    public void Modify(GameHandler game)
    {
        groundTextureOG = game.groundTexture;
        levelTextureOG = game.levelTexture;
        treesOG = game.trees;
        allowedPlayerElOG = game.allowedPlayerEl;
        allowedEnemyElOG = game.allowedEnemyEl;
        elementalDmgUpOG = game.elementalDmgUp;
        numOfObstaclesOG = game.numOfObstacles;
        numOfEnemiesOG = game.numOfEnemies;

        game.groundTexture = groundTexture;
        game.levelTexture = levelTexture;
        game.trees = trees;
        game.allowedPlayerEl = allowedPlayerEl;
        game.allowedEnemyEl = allowedEnemyEl;
        game.elementalDmgUp = elementalDmgUp;
        game.numOfObstacles = numOfObstacles;
        game.numOfEnemies = numOfEnemies;

    }

    public void Revert(GameHandler game)
    {
        game.groundTexture = groundTextureOG;
        game.levelTexture = levelTextureOG;
        game.trees = treesOG;
        game.allowedPlayerEl = allowedPlayerElOG;
        game.allowedEnemyEl = allowedEnemyElOG;
        game.elementalDmgUp = elementalDmgUpOG;
        game.numOfObstacles = numOfObstaclesOG;
        game.numOfEnemies = numOfEnemiesOG;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
