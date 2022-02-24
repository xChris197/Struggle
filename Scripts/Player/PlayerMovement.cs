using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator anim;

    [SerializeField] private float speed;
    [SerializeField] private float gravity = -19.82f;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundRadius = 0.4f;
    [SerializeField] private LayerMask groundLayer;

    private bool bIsGrounded;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        bIsGrounded = Physics.CheckSphere(transform.position, groundRadius, groundLayer);
        if(bIsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * xMovement + transform.forward * zMovement;
        if(xMovement != 0 || zMovement != 0)
        {
            anim.SetBool("bIsMoving", true);
        }
        else
        {
            anim.SetBool("bIsMoving", false);
        }
        controller.Move(move * speed * Time.deltaTime);


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
