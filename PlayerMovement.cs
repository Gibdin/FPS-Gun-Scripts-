using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController Controller;

    public float walkspeed = 10f;
    public float currentspeed = 0f;
    public float gravity = -10f;
    public float jump = 5f;
    public float CrouchSpeed = 6f;
    public Vector3 CrouchScale;
    public Vector3 RegularScale;
    public bool Crouched = false;

    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;
    bool Grounded;


    public Vector3 velocity;

    private void Start()
    {
        currentspeed = walkspeed;
    }
    // Update is called once per frame
    void Update()
    {   

        Grounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (Grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        Controller.Move(move * currentspeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && Grounded)
            {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);

            }

        velocity.y += gravity * Time.deltaTime;

        Controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
        
            Crouched = !Crouched;
            transform.localScale = Crouched ? CrouchScale : RegularScale;
            currentspeed = Crouched ? CrouchSpeed : walkspeed;
        
        }


    }
}
