using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Movement : MonoBehaviour
{
     public float moveSpeed = 5f;
    public Animator animator;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        // Get input from either keyboard or controller 1
        float moveHorizontal = Input.GetAxisRaw("Horizontal") + Input.GetAxis("Joy1X");
        float moveVertical = Input.GetAxis("Vertical") + Input.GetAxis("Joy1Y");

        Debug.Log(moveHorizontal);
        
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        animator.SetFloat("Horizontal", moveHorizontal);
        animator.SetFloat("Vertical", moveVertical);
        
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }
}
