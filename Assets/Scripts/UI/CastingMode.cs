using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastingMode : MonoBehaviour
{
    public ParticleSystem particles;
    public ParticleSystem.MainModule particleMain;
    public RectTransform particlePos;
    public Button node;
    public int successfulPops = 0;
    private List<Vector2> pointsOnScreen;
    private List<float> lifetimes;
    private List<float> spawnDelays;
    private Color color;
    public bool finishedCasting = false;
    public float timeBeforeMiniGame = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        particlePos = particles.GetComponent<RectTransform>();
        particleMain = particles.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCasting(List<Vector2> pos, List<float> lifetime, List<float> spawns, ISpell.Element element)
    {
        switch (element)
        {
            case ISpell.Element.None:
                color = Color.white;
                break;
            case ISpell.Element.Ice:
                color = Color.blue;
                break;
            case ISpell.Element.Fire:
                color = new Color(1,0.5f,0.16f);
                break;
            case ISpell.Element.Light:
                color = new Color(0.98f,0.95f,0.64f);
                break;
            case ISpell.Element.Earth:
                color = new Color(0.5f,0.28f,0.1f);
                break;
            default:
                break;
        }
        pointsOnScreen = pos; lifetimes = lifetime; spawnDelays = spawns;
        successfulPops = 0;
        finishedCasting = false;
        StartCoroutine(StartMinigame());
    }

    private IEnumerator CreateButton(int depth)
    {

        Button button = Instantiate(node, Vector3.zero, Quaternion.identity, transform);
        RectTransform rect = button.GetComponent<RectTransform>();
        rect.anchoredPosition = pointsOnScreen[depth];
        Text text = button.GetComponentInChildren<Text>();
        text.text = (depth + 1).ToString();
        text.color = color;
        button.onClick.AddListener(delegate { PopNode(button.gameObject); });
        button.image.color = color;
        StartCoroutine(HandleButton(button.gameObject, lifetimes[depth]));
        yield return new WaitForSeconds(spawnDelays[depth]);
        if(depth < pointsOnScreen.Count - 1)
        {
            StartCoroutine(CreateButton(depth + 1));
        }
        else
        {
            StartCoroutine(FinishCasting(lifetimes[depth] / 2));
        }
    }

    private void PopNode(GameObject button)
    {
        particlePos.anchoredPosition = button.GetComponent<RectTransform>().anchoredPosition;
        particleMain.startColor = button.GetComponent<Button>().image.color;
        particles.Play();
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

    private IEnumerator StartMinigame()
    {
        //Time.timeScale = 0.5f;
        yield return new WaitForSeconds(timeBeforeMiniGame);
        StartCoroutine(CreateButton(0));
    }
    private IEnumerator FinishCasting(float lastWait)
    {
        yield return new WaitForSeconds(lastWait);
        Time.timeScale = 1f;
        finishedCasting = true;
    }
}
