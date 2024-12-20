using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController cc;
    private CameraLook cameraLook;
    [Space]
    [Space]
    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float jumpForce = 5.5f;

    [Space]
    [SerializeField] private float crouchTransitionSpeed = 5f;

    [SerializeField] private float gravity = -7f;
    
    private CameraLook cam;
    private float gravityAcceleration;
    private float yVelocity;

    

    void Start()
    {
        cc = GetComponent<CharacterController>();
        cam = GetComponentInChildren<CameraLook>();

        gravityAcceleration = gravity * gravity;
        gravityAcceleration *=  Time.deltaTime;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        Vector3 moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W) && ! Input.GetKey(KeyCode.S))
        moveDir.z += 1;
        if (Input.GetKey(KeyCode.S) && ! Input.GetKey(KeyCode.W))
        moveDir.z -= 1;
        if (Input.GetKey(KeyCode.D) && ! Input.GetKey(KeyCode.A))
        moveDir.x += 1;
        if (Input.GetKey(KeyCode.A) && ! Input.GetKey(KeyCode.D))
        moveDir.x -= 1;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDir *= runSpeed;

            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 2, 0), crouchTransitionSpeed * Time.deltaTime);
            cc.height = Mathf.Lerp(cc.height, 2, crouchTransitionSpeed * Time.deltaTime);
            cc.center = Vector3.Lerp(cc.center, new Vector3(0, 1, 0), crouchTransitionSpeed *Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && ! Input.GetKey(KeyCode.LeftShift))
        {
            moveDir *= crouchSpeed;

            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 1, 0), crouchTransitionSpeed * Time.deltaTime);
            cc.height = Mathf.Lerp(cc.height, 1.2f, crouchTransitionSpeed * Time.deltaTime);
            cc.center = Vector3.Lerp(cc.center, new Vector3(0, 0.59f, 0), crouchTransitionSpeed *Time.deltaTime);
        }
        else 
        {
            moveDir *= walkSpeed;

            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 2, 0), crouchTransitionSpeed * Time.deltaTime);
            cc.height = Mathf.Lerp(cc.height, 2, crouchTransitionSpeed * Time.deltaTime);
            cc.center = Vector3.Lerp(cc.center, new Vector3(0, 1, 0), crouchTransitionSpeed *Time.deltaTime);
        }
        if (cc.isGrounded)
        {
            yVelocity = 0;
            
            if(Input.GetKey(KeyCode.Space))
            {
                yVelocity = jumpForce;
            }
        }
        else
            yVelocity -= gravityAcceleration;

        moveDir.y = yVelocity;

        moveDir = transform.TransformDirection(moveDir);
        moveDir *= Time.deltaTime;

        cc.Move(moveDir);
    }
}
