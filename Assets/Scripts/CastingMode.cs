using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastingMode : MonoBehaviour
{
    public Button node;
    public int successfulPops = 0;
    private int maxDepth;
    public bool finishedCasting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCasting(float spawnRate, float lifetime,float changeCoef, int maxd)
    {
        successfulPops = 0;
        maxDepth = maxd;
        finishedCasting = false;
        StartCoroutine(CreateButton(spawnRate, lifetime,changeCoef, 0));
    }

    private IEnumerator CreateButton(float spawnRate, float lifetime,float changeCoef, int depth)
    {
        Button button = Instantiate(node, Vector3.zero, Quaternion.identity, transform);
        RectTransform rect = button.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(Random.Range(100f, 1820f), Random.Range(100f, 920f));
        button.GetComponentInChildren<Text>().text = (depth + 1).ToString();
        button.onClick.AddListener(delegate { PopNode(button.gameObject); });
        StartCoroutine(HandleButton(button.gameObject, lifetime));
        yield return new WaitForSeconds(spawnRate);
        if(depth < maxDepth - 1)
        {
            StartCoroutine(CreateButton(spawnRate * changeCoef, lifetime * changeCoef,changeCoef, depth + 1));
        }
        else
        {
            finishedCasting = true;
        }
    }

    private void PopNode(GameObject button)
    {
        successfulPops += 1;
        Destroy(button);
    }

    private IEnumerator HandleButton(GameObject button, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        if(button !=null)
        {
            Destroy(button);
        }
    }
}
