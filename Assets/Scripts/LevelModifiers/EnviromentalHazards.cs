using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class dmgMod : MonoBehaviour
{
    public ISpell.Element element;
    public float multiplier;
}
public class EnviromentalHazards : MonoBehaviour
{

    public int grassID;
    public Texture2D groundTexture;
    public GameObject[] trees;
    
    public ISpell[] allowedPlayerEl;
    public List<dmgMod> elementalDmgUp;
    public int numOfObstacles;
    public int numOfMeleeEnemies;
    public int numOfRangedEnemies;
    public int numOfTrees;

    public List<GameObject> meleeEnemyTypes;
    public List<GameObject> rangedEnemyTypes;
    public List<GameObject> bossTypes;

    private int grassIDOG;
    private Texture2D groundTextureOG;
    private GameObject[] treesOG;

    private ISpell[] allowedPlayerElOG;
    private List<dmgMod> elementalDmgUpOG;
    private int numOfObstaclesOG;
    private int numOfMeleeEnemiesOG;
    private int numOfRangedEnemiesOG;
    private int numOfTreesOG;

    private List<GameObject> meleeEnemyTypesOG;
    private List<GameObject> rangedEnemyTypesOG;
    private List<GameObject> bossTypesOG;

    public void Modify(GameHandler game)
    {
        groundTextureOG = game.tergen.groundTexture;
        grassIDOG = game.tergen.grassID;
        treesOG = game.tergen.tree;
        allowedPlayerElOG = game.allowedPlayerEl;
        elementalDmgUpOG = game.elementalDmgUp;
        numOfObstaclesOG = game.numOfObstacles;
        numOfMeleeEnemiesOG = game.meleeEnemyNumber;
        numOfRangedEnemiesOG = game.rangedEnemyNumber;
        numOfTreesOG = game.tergen.treeAmount;

        meleeEnemyTypesOG = game.meleeEnemyTypes;
        rangedEnemyTypesOG = game.rangedEnemyTypes;
        bossTypesOG = game.bossTypes;

        game.tergen.groundTexture = groundTexture;
        game.tergen.grassID = grassID;
        game.tergen.tree = trees;
        game.allowedPlayerEl = allowedPlayerEl;
        game.elementalDmgUp = elementalDmgUp;
        game.numOfObstacles = numOfObstacles;
        game.meleeEnemyNumber = numOfMeleeEnemies;
        game.rangedEnemyNumber = numOfRangedEnemies;
        game.tergen.treeAmount = numOfTrees;

        game.meleeEnemyTypes = meleeEnemyTypes;
        game.rangedEnemyTypes = rangedEnemyTypes;
        game.bossTypes = bossTypes;
    } 

    public void Revert(GameHandler game)
    {
        game.tergen.groundTexture = groundTextureOG;
        game.tergen.grassID = grassIDOG;
        game.tergen.tree = treesOG;
        game.allowedPlayerEl = allowedPlayerElOG;
        game.elementalDmgUp = elementalDmgUpOG;
        game.numOfObstacles = numOfObstaclesOG;
        game.meleeEnemyNumber = numOfMeleeEnemiesOG;
        game.rangedEnemyNumber = numOfRangedEnemiesOG;
        game.tergen.treeAmount = numOfTreesOG;
        game.meleeEnemyTypes = meleeEnemyTypesOG;
        game.rangedEnemyTypes = rangedEnemyTypesOG;
        game.bossTypes = bossTypesOG;
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
