using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnviromentalHazards;

public class GameHandler : MonoBehaviour
{
    private bool currentlyPlaying = false;
    public GameObject player;
    private SpellManager spellManager;
    public float levelRange = 1000;
    public TerrainGenerator tergen;
    public WallGenerator walls;
    private ParticleSystem particles;
    public int meleeEnemyNumber = 20;
    public int currentEnemyNumber;
    public int rangedEnemyNumber = 10;
    public int levelsPassed = -1;
    //656565 BASE HEX FOR SKYBOX
    public Light generalLight;
    public Color baseColorLight;
    public Color baseColorSkybox;
    //6F6F6F BASE HEX FOR FOG
    public Color fogColor;
    public List<GameObject> meleeEnemyTypes = new List<GameObject>();
    public List<GameObject> rangedEnemyTypes = new List<GameObject>();
    public List<GameObject> bossTypes = new List<GameObject>();
    private GameObject enemyParent;

    public List<ISpell> spellsForEnemy = new List<ISpell>();
    public int spawnPointsNumber = 4;
    public float spawnDistance = 8;

    public Texture2D levelTexture;
    public Texture2D groundTexture;
    public GameObject[] trees;

    public ISpell[] allowedPlayerEl;
    private List<ISpell> pickedSpells = new List<ISpell>();
    public List<dmgMod> elementalDmgUp;
    public int numOfObstacles;

    public int firstSpell;
    public int secondSpell;
    public List<int> pickedSpellIndexes;

    //UI ELEMENTS
    public PauseMenuUI pauser;
    public Image choice1Border;
    public Button choice1;
    public Text choice1Text;
    public Image choice2Border;
    public Button choice2;
    public Text choice2Text;
    public Image overlay;
    public Text tooltip;
    public Text count;

    public Button advancer;
    public Button starter;

    public List<Image> spellImages = new List<Image>();
    private EnviromentalHazards pickedHazard;
    public List<EnviromentalHazards> possibleHazards = new List<EnviromentalHazards>();

    // Start is called before the first frame update
    void Start()
    {
        currentlyPlaying = false;
        for (int i = 0; i < spellImages.Count; i++)
        {
            spellImages[i].enabled = false;
        }
        spellManager = player.GetComponent<SpellManager>();
        particles = GetComponent<ParticleSystem>();
        var particleMain = particles.main;
        particleMain.startColor = new ParticleSystem.MinMaxGradient(fogColor);
        particles.Play();
        ChangeShowStatus(true);
        advancer.gameObject.SetActive(false);
        starter.gameObject.SetActive(false);
        pickedHazard = possibleHazards[Random.Range(0, possibleHazards.Count)];
        pickedHazard.Modify(this);
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckForEnemyCount();
    }

    public void ChangeShowStatus(bool changer)
    {
        tooltip.enabled = changer;
        count.enabled = changer;
        overlay.enabled = changer;
        choice1.gameObject.SetActive(changer);
        choice1Border.enabled = changer;
        choice1Text.enabled = changer;
        choice2.gameObject.SetActive(changer);
        choice2Border.enabled = changer;
        choice2Text.enabled = changer;
    }
    public void StartLevel()
    {
        spellManager.enabled = false;
        advancer.gameObject.SetActive(false);
        if (levelsPassed + 1 % 3 == 0)
        {
            player.GetComponent<PlayerController>().enabled = false;
            Cursor.visible = true;
            for (int i = pickedSpells.Count - 1; i >= 0; i--)
            {
                Destroy(pickedSpells[i].gameObject);
            }
            pickedSpells.Clear();
            pickedSpellIndexes.Clear();
            pickedHazard.Revert(this);
            SetDefaultSettings();
            pickedHazard = possibleHazards[Random.Range(0, possibleHazards.Count)];
            pickedHazard.Modify(this);
        }
        Restart();
        //UI - picking spells
        if (levelsPassed % 3 == 0)
        {
            ChangeShowStatus(true);
            GenerateSpellChoice();
        }
        else
        {
            starter.gameObject.SetActive(true);
        }
        //UI - Ready to play

    }

    public void GenerateSpellChoice()
    {
        count.text = (pickedSpells.Count + 1) + "/" + spellImages.Count;
        firstSpell = Random.Range(0, allowedPlayerEl.Length);
        while (pickedSpellIndexes.Contains(firstSpell))
        {
            firstSpell = Random.Range(0, allowedPlayerEl.Length);
        }
        secondSpell = Random.Range(0, allowedPlayerEl.Length);
        while (secondSpell == firstSpell || pickedSpellIndexes.Contains(secondSpell))
        {
            secondSpell = Random.Range(0, allowedPlayerEl.Length);
        }
        choice1.image.sprite = allowedPlayerEl[firstSpell].spellSprite;
        choice1Text.text = allowedPlayerEl[firstSpell].spellName;
        choice2.image.sprite = allowedPlayerEl[secondSpell].spellSprite;
        choice2Text.text = allowedPlayerEl[secondSpell].spellName;
    }

    public void AdvancePicking()
    {
        spellImages[pickedSpells.Count - 1].enabled = true;
        if (pickedSpells.Count == 3)
        {
            ChangeShowStatus(false);
            starter.gameObject.SetActive(true);
        }
        else
        {
            GenerateSpellChoice();
        }
    }


    public void ChooseSpell1()
    {
        spellImages[pickedSpells.Count].sprite = allowedPlayerEl[firstSpell].spellSprite;
        pickedSpellIndexes.Add(firstSpell);
        GameObject spell = Instantiate(allowedPlayerEl[firstSpell].gameObject, Vector3.zero, Quaternion.identity);
        pickedSpells.Add(spell.GetComponent<ISpell>());
        AdvancePicking();
    }

    public void ChooseSpell2()
    {
        spellImages[pickedSpells.Count].sprite = allowedPlayerEl[secondSpell].spellSprite;
        pickedSpellIndexes.Add(secondSpell);
        GameObject spell = Instantiate(allowedPlayerEl[secondSpell].gameObject, Vector3.zero, Quaternion.identity);
        pickedSpells.Add(spell.GetComponent<ISpell>());
        AdvancePicking();
    }

    public void StartGameplay()
    {
        if (levelsPassed % 3 == 2 && levelsPassed > 0)
        {
            SpawnBoss();
        }
        else
        {
            SpawnEnemies();
        }
        spellManager.spells = pickedSpells;
        Cursor.visible = false;
        player.GetComponent<PlayerController>().enabled = true;
        CharacterController character = player.GetComponent<CharacterController>();
        character.enabled = true;
        starter.gameObject.SetActive(false);
        currentlyPlaying = true;
    }

    public void SetDefaultSettings()
    {
        RenderSettings.skybox.SetColor("_Tint", baseColorSkybox);
        RenderSettings.fogColor = fogColor;
        generalLight.color = baseColorLight;
        tergen.roughness = Random.Range(1, 20);
        tergen.fineness = Random.Range(4, 20);
        tergen.noiseFrequency = Random.Range(1, 8);
        walls.radius = tergen.Size;
        walls.height = 60;
    }
    public void Restart()
    {
        levelsPassed++;
        Destroy(enemyParent);
        CharacterController character = player.GetComponent<CharacterController>();
        character.enabled = false;
        transform.position = new Vector3(Random.Range(-levelRange, levelRange), 0, Random.Range(-levelRange, levelRange));
        tergen.GenerateNewTerrain();
        walls.GenerateWalls();
        player.transform.position = transform.position + new Vector3(0, tergen.roughness + 1, 0);
        ParticleSystem.ShapeModule particleShape = particles.shape;
        particleShape.radius = tergen.Size;
        var particleMain = particles.main;
        particleMain.startColor = new ParticleSystem.MinMaxGradient(fogColor);
        particles.Play();
    }

    public void SpawnEnemies()
    {
        List<Vector2> spawnPoints = new List<Vector2>();
        for (int i = 0; i < spawnPointsNumber; i++)
        {
            spawnPoints.Add((Random.Range(0f,1f) < 0.5f ? -1 : 1) * tergen.Size / 4 * 3 * new Vector2(Random.Range(0.4f,1f),Random.Range(0.4f,1f)) / 2);
        }
        enemyParent = new GameObject("EnemyParent");
        enemyParent.transform.parent = transform;
        //RANDOM BASED, FOR ENVIRONMENT SPECIAL REMAKE SCRIPT
        for (int i = 0; i < meleeEnemyNumber; i++)
        {
            Vector2 randomXZ = Random.insideUnitCircle * spawnDistance + spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject enemy = Instantiate(meleeEnemyTypes[Random.Range(0, meleeEnemyTypes.Count)],
                new Vector3(randomXZ.x, tergen.roughness, randomXZ.y) + transform.position, Quaternion.identity, enemyParent.transform);
            MeleeEnemyBehaviour melee = enemy.GetComponent<MeleeEnemyBehaviour>();
            melee.handler = this;
            melee.target = player.transform;
            enemy.GetComponent<HealthManager>().charController = melee;

        }
        for (int i = 0; i < rangedEnemyNumber; i++)
        {
            Vector2 randomXZ = Random.insideUnitCircle * spawnDistance + spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject enemy = Instantiate(rangedEnemyTypes[Random.Range(0, rangedEnemyTypes.Count)],
                new Vector3(randomXZ.x, tergen.roughness, randomXZ.y) + transform.position, Quaternion.identity, enemyParent.transform);
            RangedEnemyBehaviour enemyBeh = enemy.GetComponent<RangedEnemyBehaviour>();
            enemyBeh.handler = this;
            enemyBeh.target = player.transform;
            enemy.GetComponent<HealthManager>().charController = enemyBeh;
        }
        currentEnemyNumber = meleeEnemyNumber + rangedEnemyNumber;
    }
    public void SpawnBoss()
    {
        Vector2 spawn = (Random.Range(0f, 1f) < 0.5f ? -1 : 1) * tergen.Size * new Vector2(Random.Range(0.4f, 1f), Random.Range(0.4f, 1f)) / 2;
        Vector2 randomXZ = Random.insideUnitCircle * spawnDistance + spawn;
        GameObject enemy = Instantiate(bossTypes[Random.Range(0, bossTypes.Count)],
            new Vector3(randomXZ.x, tergen.roughness + 5, randomXZ.y) + transform.position, Quaternion.identity, enemyParent.transform);
        RangedEnemyBehaviour enemyBeh = enemy.GetComponent<RangedEnemyBehaviour>();
        enemyBeh.handler = this;
        enemyBeh.target = player.transform;
        enemy.GetComponent<HealthManager>().charController = enemyBeh;
        currentEnemyNumber = 1;
    }
    public void CheckForEnemyCount()
    {
        if(currentlyPlaying && currentEnemyNumber == 0)
        {
            Cursor.visible = true;
            currentlyPlaying = false;
            player.GetComponent<PlayerController>().enabled = false;
            advancer.gameObject.SetActive(true);
        }
    }
}
