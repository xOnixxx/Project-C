using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    //Must be power of 2 + 1
    public Texture2D groundTexture;
    public int grassID;
    public GameObject[] tree;
    public int treeAmount = 200;
    public int heightMapResolution = 33;
    public float roughness = 1;
    public float noiseFrequency = 1;
    public int fineness = 4;
    public int Size = 100;
    public int grassDensity = 16;
    private float[,] heightMap;
    private int[,] detailMap;
    private float maximumHeight;
    private GameObject currentTerrain;
    public TerrainData terdata;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetOctave(float x, float z, int fineness)
    {
        float noise;
        float sum = 0;
        for (int i = 0, coef = 1; i < fineness; i++, coef *= 2)
        {
            noise = (Mathf.PerlinNoise((x * (coef * noiseFrequency)) * 0.01f, (z * (coef * noiseFrequency)) * 0.01f) * 2 - 1) / coef;
            noise = noise < 0 ? 0 : noise > 1 ? 1 : noise;
            sum += noise;
        }
        return sum;
    }

    public void GenerateNewTerrain()
    {
        if (currentTerrain != null)
        {
            Destroy(currentTerrain);
        }
        heightMap = new float[heightMapResolution, heightMapResolution];
        detailMap = new int[heightMapResolution, heightMapResolution];
        //TerrainData terdata = new TerrainData();
        int mapLength = heightMap.GetLength(0);
        terdata.heightmapResolution = heightMapResolution;
        terdata.size = new Vector3(2*Size,roughness,2*Size);
        maximumHeight = 0;
        for (int x = 0; x < mapLength; x++)
        {
            for (int z = 0; z < mapLength; z++)
            {
                heightMap[x, z] = GetOctave(-Size + x + transform.position.x, -Size + z + transform.position.z, fineness);
                detailMap[x, z] = 16;
                if(heightMap[x,z] > maximumHeight)
                {
                    maximumHeight = heightMap[x, z];
                }
            }
        }
        for (int x = 0; x < mapLength; x++)
        {
            for (int z = 0; z < mapLength; z++)
            {
                heightMap[x, z] = heightMap[x,z] / maximumHeight;
            }
        }
        terdata.SetHeights(0, 0, heightMap);
        TreePrototype[] trees = new TreePrototype[tree.Length];
        for (int i = 0; i < tree.Length; i++)
        {
            trees[i] = new TreePrototype();
            trees[i].prefab = tree[i];
        }
        terdata.treePrototypes = trees;
        TreeInstance[] treeinstances = new TreeInstance[treeAmount];
        for (int i = 0; i < treeAmount; i++)
        {
            Vector2 random = Random.insideUnitCircle;
            TreeInstance testTree = new TreeInstance()
            {
                position = new Vector3(Size / 2 - random.x * Size / 2, 0, Size / 2 - random.y * Size / 2) / Size,
                prototypeIndex = Random.Range(0,tree.Length),
                widthScale = 1f,
                heightScale = 1f,
                color = Color.white,
                lightmapColor = Color.white,
                rotation = 0
            };
            treeinstances[i] = testTree;
        }
        terdata.SetTreeInstances(treeinstances, true);
        terdata.SetDetailResolution(heightMapResolution, grassDensity);
        terdata.SetDetailLayer(0, 0, grassID, detailMap);

        TerrainLayer[] terrainLayers = new TerrainLayer[1];
        TerrainLayer ground = new TerrainLayer();
        ground.diffuseTexture = groundTexture;
        terrainLayers[0] = ground;
        terdata.terrainLayers = terrainLayers;

        currentTerrain = Terrain.CreateTerrainGameObject(terdata);
        currentTerrain.GetComponent<Terrain>().detailObjectDistance = Size;
        currentTerrain.transform.position = transform.position - new Vector3(Size,0,Size);
    }
    public void UpdateTerrain()
    {

    }
}
