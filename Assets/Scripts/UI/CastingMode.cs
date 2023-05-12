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
    public float timeBeforeMiniGame = 0.5f;
    private Vector2 lastButton = new Vector2(960, 540);
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
        StartCoroutine(StartMinigame(spawnRate, lifetime, changeCoef));
    }

    private IEnumerator CreateButton(float spawnRate, float lifetime,float changeCoef, int depth)
    {

        Button button = Instantiate(node, Vector3.zero, Quaternion.identity, transform);
        RectTransform rect = button.GetComponent<RectTransform>();
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        lastButton += Random.insideUnitCircle * 400;
        lastButton = new Vector2(Mathf.Clamp(lastButton.x, 150f, 1770f), Mathf.Clamp(lastButton.y, 150f, 930f));
        rect.anchoredPosition = lastButton;
        Text text = button.GetComponentInChildren<Text>();
        text.text = (depth + 1).ToString();
        text.color = color;
        button.onClick.AddListener(delegate { PopNode(button.gameObject); });
        button.image.color = color;
        StartCoroutine(HandleButton(button.gameObject, lifetime));
        yield return new WaitForSeconds(spawnRate);
        if(depth < maxDepth - 1)
        {
            StartCoroutine(CreateButton(spawnRate * changeCoef, lifetime * changeCoef,changeCoef, depth + 1));
        }
        else
        {
            Debug.Log(successfulPops);
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

    private IEnumerator StartMinigame(float spawnRate, float lifetime, float changeCoef)
    {
        yield return new WaitForSeconds(timeBeforeMiniGame);
        StartCoroutine(CreateButton(spawnRate, lifetime, changeCoef, 0));
    }
}
