using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    public GameObject wall;
    private Vector3 centre;
    public int numVertices = 4;
    public float radius = 10;
    public float height = 10;
    private List<GameObject> wallInstances = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GenerateWalls()
    {
        for (int i = wallInstances.Count - 1; i >= 0; i--)
        {
            Destroy(wallInstances[i]);
        }
        wallInstances.Clear();
        centre = GetComponentInParent<Transform>().position;
        Vector3[] polygonVertices = new Vector3[numVertices];
        for (int i = 0; i < numVertices; i++)
        {
            polygonVertices[i] = new Vector3(Mathf.Sin((float)i / numVertices * 2 * Mathf.PI) * radius, 0, Mathf.Cos((float)i/numVertices * 2 * Mathf.PI) * radius) + centre;
        }
        GameObject newWall;
        Collider collider;
        for (int i = 0; i < numVertices - 1; i++)
        {
            newWall = Instantiate(wall, (polygonVertices[i] + polygonVertices[i + 1]) / 2, Quaternion.identity, transform);
            newWall.transform.localScale = new Vector3(radius, radius, radius); ; //TODO Get reliable sizes
            newWall.transform.LookAt(centre);
            newWall.transform.rotation = Quaternion.AngleAxis(90, (polygonVertices[i] - polygonVertices[i+1]));
            newWall.transform.eulerAngles = new Vector3(90, newWall.transform.eulerAngles.y, newWall.transform.eulerAngles.z);
            collider = newWall.GetComponent<Collider>();
            if(collider != null)
            {
                collider.transform.localScale = new Vector3(radius * 2, 2, height + 1);
            }
            wallInstances.Add(newWall);
        }
        newWall = Instantiate(wall, (polygonVertices[numVertices-1] + polygonVertices[0]) / 2, Quaternion.identity, transform);
        newWall.transform.localScale = new Vector3(radius, radius, radius); ; //TODO Get reliable sizes
        newWall.transform.LookAt(centre);
        newWall.transform.rotation = Quaternion.AngleAxis(90, (polygonVertices[numVertices - 1] - polygonVertices[0]));
        newWall.transform.eulerAngles = new Vector3(90, newWall.transform.eulerAngles.y, newWall.transform.eulerAngles.z);
        collider = newWall.GetComponent<Collider>();
        if (collider != null)
        {
            collider.transform.localScale = new Vector3(radius * 2, 2, height + 1);
        }
        wallInstances.Add(newWall);
        newWall = Instantiate(wall, centre + new Vector3(0,height,0), Quaternion.identity, transform);
        newWall.transform.localScale = new Vector3(radius, radius, radius);
        newWall.transform.eulerAngles = new Vector3(180,0, 0);
        collider = newWall.GetComponent<Collider>();
        if (collider != null)
        {
            collider.transform.localScale = new Vector3(radius*2, 2, radius * 2);
        }
        wallInstances.Add(newWall);
    }
}
