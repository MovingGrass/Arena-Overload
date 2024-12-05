using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Movement : MonoBehaviour
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
        // Get input from either keyboard or controller 2
        float moveHorizontal = Input.GetAxis("Joy2X");
        float moveVertical = Input.GetAxis("Joy2Y");
        
        // Also allow arrow keys for player 2
        if (Input.GetKey(KeyCode.LeftArrow)) moveHorizontal -= 1;
        if (Input.GetKey(KeyCode.RightArrow)) moveHorizontal += 1;
        if (Input.GetKey(KeyCode.UpArrow)) moveVertical += 1;
        if (Input.GetKey(KeyCode.DownArrow)) moveVertical -= 1;
        
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
