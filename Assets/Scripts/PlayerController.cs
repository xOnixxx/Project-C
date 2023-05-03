using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum State
    {
        Idle,
        Casting
    }
    //Character stats
    public bool inControl = true;
    private CharacterController character;
    private Animator animator;
    public float gravity = 9.87f;
    public float speed = 6;
    public float sprintHorizontalSpeed = 10;
    private float verticalSpeed = 0;
    private float curSpeed = 6;
    public float castModeCooldown = 1.5f;
    private bool canChangeMode = true;
    private State state = State.Idle;

    //Camera stats
    public Transform cameraHolder;
    public float mouseSensitivity = 3f;
    public float upLimit = -50;
    public float downLimit = 50;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        character = GetComponent<CharacterController>();
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
        cameraHolder.Rotate(-verticalRotation * mouseSensitivity, 0, 0);

        Vector3 currentRotation = cameraHolder.localEulerAngles;
        if(currentRotation.x > 180) { currentRotation.x -= 360; }
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        cameraHolder.localRotation = Quaternion.Euler(currentRotation);
    }
    private void CheckActions()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            curSpeed = sprintHorizontalSpeed; 
        }
        else 
        {
            curSpeed = speed;
        }
        if(Input.GetButtonDown("Fire2"))
        {
            if(state == State.Idle && canChangeMode)
            {
                canChangeMode = false;
                StartCoroutine(Cooldown());
                state = State.Casting;
                inControl = false;
                StartCoroutine(CurveUp(0));
                animator.SetBool("inCastMode", true);
            }
            else
            {
                state = State.Idle;
                inControl = true;
                animator.SetBool("inCastMode", false);
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
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(castModeCooldown);
        canChangeMode = true;
    }
}
