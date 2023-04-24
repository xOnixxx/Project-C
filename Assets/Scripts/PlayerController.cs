using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Character stats
    public CharacterController character;
    public float gravity = 9.87f;
    public float horizontalSpeed = 4;
    private float verticalSpeed = 0;
    public float speed = 6;

    //Camera stats
    public Transform cameraHolder;
    public float mouseSensitivity = 3f;
    public float upLimit = -50;
    public float downLimit = 50;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (character.isGrounded) 
        {
            if (Input.GetButton("Jump"))
            {
                verticalSpeed += horizontalSpeed;
            }
            else
            {
                verticalSpeed = 0;
            }
        }
        else { verticalSpeed -= gravity * Time.deltaTime; }

        Vector3 gravityVec = new Vector3(0, verticalSpeed, 0);
        Vector3 moveVec = transform.forward * verticalMove + transform.right * horizontalMove;

        character.Move(speed * Time.deltaTime * moveVec + Time.deltaTime * gravityVec);
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
}
