using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    public GameHandler gameMaster;
    private enum State
    {
        Idle,
        SpellReady,
        Casting
    }
    //Character stats
    public bool inControl = true;
    private CharacterController character;
    public PauseMenuUI pauser;
    public float gravity = 9.87f;
    public float speed = 6;
    public float sprintHorizontalSpeed = 10;
    private float verticalSpeed = 0;
    private float curSpeed = 6;
    public float castModeCooldown = 1.5f;
    private bool canChangeMode = true;
    private State state = State.Idle;
    public float maxStamina = 5;
    public float currentStamina = 5;
    public float staminaRegenDelay = 0.3f;
    public bool canRun = true;
    public bool canRegen = true;

    //Camera stats
    public Transform cameraHolder;
    public float mouseSensitivity = 3f;
    public float upLimit = -50;
    public float downLimit = 50;
    List<GameObject> spikes = new List<GameObject>();

    private SpellManager spellManager;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        character = GetComponent<CharacterController>();
        spellManager = GetComponent<SpellManager>();
        spellManager.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        CheckActions();
        if (inControl)
        {
            Move();
        }
        Rotate();
    }

    private void Move()
    {
        if (character.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                verticalSpeed += speed;
            }
            else
            {
                verticalSpeed = 0;
            }
        }
        else { verticalSpeed -= gravity * Time.deltaTime; }

        Vector3 gravityVec = new Vector3(0, verticalSpeed, 0);
        Vector3 moveVec = Vector3.zero;
        if (inControl)
        {
            float horizontalMove = Input.GetAxis("Horizontal");
            float verticalMove = Input.GetAxis("Vertical");
            moveVec = transform.forward * verticalMove + transform.right * horizontalMove;
        }

        character.Move(curSpeed * Time.deltaTime * moveVec + Time.deltaTime * gravityVec);
    }
    private void Rotate()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");
        transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);

        foreach(var spike in spikes)
        {
            spike.transform.Rotate(0, -horizontalRotation * mouseSensitivity, 0);
        }
        cameraHolder.Rotate(-verticalRotation * mouseSensitivity, 0, 0);

        Vector3 currentRotation = cameraHolder.localEulerAngles;
        if(currentRotation.x > 180) { currentRotation.x -= 360; }
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        cameraHolder.localRotation = Quaternion.Euler(currentRotation);
    }
    private void CheckActions()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameMaster.Restart();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
        }
        if(Input.GetKey(KeyCode.LeftShift) && canRun)
        {
            StopCoroutine(RecoverStamina());
            canRegen = false;
            currentStamina -= Time.deltaTime;
            if(currentStamina < 0)
            {
                currentStamina = 0;
                canRun = false;
            }
            curSpeed = sprintHorizontalSpeed; 
        }
        else 
        {
            curSpeed = speed;
            if(currentStamina < maxStamina)
            {
                if(canRegen)
                {
                    currentStamina += Time.deltaTime;
                    if(currentStamina > maxStamina)
                    {
                        currentStamina = maxStamina;
                        canRun = true;
                    }
                }
                else
                {
                    StartCoroutine(RecoverStamina());
                }
            }
        }
        if(Input.GetButtonDown("Fire2"))
        {
            if(state == State.Idle && canChangeMode)
            {
                canChangeMode = false;
                state = State.SpellReady;
                inControl = false;
                StartCoroutine(CurveUp(0)); //Make it so player can't fly with this
                spellManager.enabled = true;
            }
            else
            {
                StartCoroutine(CastModeCooldown());
                state = State.Idle;
                inControl = true;
                spellManager.enabled = false;
                spellManager.Hide();
            }
        }
    }

    private IEnumerator CurveUp(int depth)
    {
        yield return new WaitForSeconds(0.02f);
        if (depth < 10)
        {
            character.Move(new Vector3(0, 0.3f, 0) + transform.forward / 10);
            StartCoroutine(CurveUp(depth + 1));
        }
    }
    private IEnumerator CastModeCooldown()
    {
        yield return new WaitForSeconds(castModeCooldown);
        canChangeMode = true;
    }
    private IEnumerator RecoverStamina()
    {
        yield return new WaitForSeconds(staminaRegenDelay);
        canRegen = true;
    }
    public override void Die()
    {
        Destroy(spellManager);
        Destroy(this);
    }
}
