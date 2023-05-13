using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject player;
    public float levelRange = 1000;
    public TerrainGenerator tergen;
    public WallGenerator walls;
    public int meleeEnemyNumber = 20;
    public int rangedEnemyNumber = 10;
    public int levelsPassed = 0;
    public List<GameObject> meleeEnemyTypes = new List<GameObject>();
    public List<GameObject> rangedEnemyTypes = new List<GameObject>();
    public List<Spell> spellsForEnemy = new List<Spell>();
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        levelsPassed++;
        CharacterController character = player.GetComponent<CharacterController>();
        character.enabled = false;
        transform.position = new Vector3(Random.Range(-levelRange, levelRange), 0, Random.Range(-levelRange, levelRange));
        tergen.roughness = Random.Range(1, 20);
        tergen.fineness = Random.Range(4, 20);
        tergen.noiseFrequency = Random.Range(1, 10);
        tergen.GenerateNewTerrain();
        walls.GenerateWalls();
        player.transform.position = transform.position + new Vector3(0, tergen.roughness + 1, 0);
        SpawnEnemies();
        character.enabled = true;
    }

    public void SpawnEnemies()
    {
        //RANDOM BASED, FOR ENVIRONMENT SPECIAL REMAKE SCRIPT
        for (int i = 0; i < meleeEnemyNumber; i++)
        {
            Vector2 randomXZ = Random.onUnitSphere * tergen.Size;
            GameObject enemy = Instantiate(meleeEnemyTypes[Random.Range(0, meleeEnemyTypes.Count - 1)],
                new Vector3(randomXZ.x, tergen.roughness, randomXZ.y), Quaternion.identity,transform);
            enemy.GetComponent<MeleeEnemyBehaviour>().target = player.transform;
        }
        for (int i = 0; i < rangedEnemyNumber; i++)
        {
            Vector2 randomXZ = Random.onUnitSphere * tergen.Size;
            GameObject enemy = Instantiate(rangedEnemyTypes[Random.Range(0, rangedEnemyTypes.Count - 1)],
                new Vector3(randomXZ.x, tergen.roughness, randomXZ.y), Quaternion.identity);
            RangedEnemyBehaviour enemyBeh = enemy.GetComponent<RangedEnemyBehaviour>();
            enemyBeh.target = player.transform;
            enemyBeh.spell = Instantiate(spellsForEnemy[Random.Range(0, spellsForEnemy.Count - 1)], Vector3.zero, Quaternion.identity,transform);
        }
    }
}
