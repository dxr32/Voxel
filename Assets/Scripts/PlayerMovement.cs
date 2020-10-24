using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed;
    float pSpeed;
    public float jumpHeight;
    public float gravity = -20f;
    public float groundDistance = 0.4f;
    public float ceelingDistance = 1f;

    public Transform ground;
    public Transform ceeling;
    public Transform ceelingMax;
    
    public LayerMask groundMask;
    public LayerMask enviroment;

    Vector3 velocity;

    bool isGrounded;
    bool canStand;
    bool crouching;
    bool running;

    void Start()
    {
        pSpeed = speed;
    }

    void Update()
    {
        Movement();
        Gravity();
        Jump();
        Run();
        Crouch();
    }

    void Gravity()
    {
        isGrounded = Physics.CheckSphere(ground.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void Movement()
    {
        float xAxis = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float zAxis = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 move = transform.right * xAxis + transform.forward * zAxis;

        controller.Move(move);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && !canStand)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void Run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !crouching)
        {
            speed *= 1.5f;
            running = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !crouching)
        {
            speed = pSpeed;
            running = false;
        }
    }

    void Crouch()
    {
        canStand = Physics.CheckCapsule(ceeling.position, ceelingMax.position, 0.1f, enviroment);

        if (Input.GetKeyDown(KeyCode.LeftControl) && !canStand && !running)
        {
            controller.height = 0.5f;
            controller.radius = 0f;
            speed /= 3f;
            crouching = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) && !canStand && !running)
        {
            controller.height = 2f;
            controller.radius = 0.5f;
            speed /= 3f;
            crouching = false;
        }else if(!Input.GetKey(KeyCode.LeftControl) && !canStand && !running)
        {
            controller.height = 2f;
            controller.radius = 0.5f;
            speed = pSpeed;
            crouching = false;
        }
    }
}
