using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject player;
    public float levelRange = 1000;
    public TerrainGenerator tergen;
    public WallGenerator walls;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        CharacterController character = player.GetComponent<CharacterController>();
        character.enabled = false;
        transform.position = new Vector3(Random.Range(-levelRange, levelRange), 0, Random.Range(-levelRange, levelRange));
        tergen.roughness = Random.Range(1, 20);
        tergen.fineness = Random.Range(4, 20);
        tergen.noiseFrequency = Random.Range(1, 10);
        tergen.GenerateNewTerrain();
        walls.GenerateWalls();
        player.transform.position = transform.position + new Vector3(0, tergen.roughness + 1, 0);
        character.enabled = true;
    }
}
