using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public float xSize = 10;
    public float zSize = 10;
    public float cellsPerUnit = 1;
    public float roughness = 1;
    public float noiseFrequency = 1;
    public int fineness = 4;
    public List<GameObject> trees = new List<GameObject>();
    public int treeNumber = 20;
    public List<GameObject> props = new List<GameObject>();
    public int propNumber = 10;


    void Start()
    {
        BuildMesh();
        GenerateEnvironment();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public float GetOctave(float x, float z, int fineness)
    {
        float sum = 0;
        for (int i = 0, coef = 1; i < fineness; i++, coef*=2)
        {
            sum += (Mathf.PerlinNoise((x * (coef * noiseFrequency)) * 0.01f, (z * (coef * noiseFrequency)) * 0.01f) * 2 - 1) / coef;
        }
        return sum;
    }

    public void BuildMesh()
    {
        Mesh mesh = new Mesh();
        Vector3 p = transform.localPosition;
        int xArraySize = (int)(xSize * 2 * cellsPerUnit);
        int zArraySize = (int)(zSize * 2 * cellsPerUnit);
        Vector3[] vertices = new Vector3[(xArraySize + 1) * (zArraySize + 1)];
        int[] triangles = new int[xArraySize * zArraySize * 6];
        Vector2[] uvCoords = new Vector2[(xArraySize + 1) * (zArraySize + 1)];

        for (int x = 0, vertex = 0; x < xArraySize + 1; x++)
        {
            for (int z = 0; z < zArraySize + 1; z++, vertex++)
            {
                vertices[vertex] = new Vector3(x / cellsPerUnit - xSize, GetOctave(- xSize + x / cellsPerUnit,-zSize + z / cellsPerUnit, fineness) * roughness,z / cellsPerUnit - zSize) + p;
                uvCoords[vertex] = new Vector2(p.x - xSize + x / cellsPerUnit, p.z - zSize + z / cellsPerUnit);
            }
        }
        for (int x = 0, t = 0, v = 0; x < xArraySize; x++, v++)
        {
            for (int z = 0; z < zArraySize; z++, t+=6, v++)
            {
                triangles[t] = v;
                triangles[t + 1] = v + 1;
                triangles[t + 2] = v + zArraySize + 1;
                triangles[t + 3] = v + 1;
                triangles[t + 4] = v + zArraySize + 2;
                triangles[t + 5] = v + zArraySize + 1;
            }
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvCoords;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void GenerateEnvironment()
    {
        float radius = Mathf.Min(xSize, zSize);
        for (int i = 0; i < treeNumber; i++)
        {
            Vector2 xz = Random.insideUnitCircle * radius;
            GameObject obj = Instantiate(trees[Random.Range(0, trees.Count)],
                new Vector3(xz.x + transform.position.x, GetOctave(xz.x, xz.y, fineness) * roughness - 0.5f, xz.y + transform.position.z), Quaternion.identity, transform);
            obj.transform.eulerAngles = new Vector3(-90, 0, Random.Range(0f,360f));
        }
        for (int i = 0; i < propNumber; i++)
        {
            Vector2 xz = Random.insideUnitCircle * radius;
            GameObject obj = Instantiate(props[Random.Range(0, props.Count)],
                new Vector3(xz.x + transform.position.x, GetOctave(xz.x, xz.y, fineness) * roughness - 0.5f, xz.y + transform.position.z), Quaternion.identity, transform);
            obj.transform.eulerAngles = new Vector3(-90, 0, Random.Range(0f, 360f));
        }
    }
}
