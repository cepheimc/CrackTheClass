﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float speed = 10;
    private Animator animator;
    private Vector3 change;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimatorAndMove();

    }

    void UpdateAnimatorAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            var tmp = transform.position;
            animator.SetFloat("moveX", tmp.x);
            animator.SetFloat("moveY", tmp.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        rb.MovePosition(transform.position + speed * change * Time.deltaTime);
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Exit"))
        {
            // We know that this is not complied with SOLID, sorry...
            SceneManager.LoadScene("InitialScene", LoadSceneMode.Single);
        }
    }


}