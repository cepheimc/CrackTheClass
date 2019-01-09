using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float speed = 10;
    public float maxAttentionMultiplier = 1;

    private Animator animator;
    private Vector3 change;
    private Rigidbody2D rb;
    private float attentionLimit;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        attentionLimit = 1 * maxAttentionMultiplier;
    }

    void FixedUpdate()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimatorAndMove();

        var lecturer = GameObject.FindGameObjectWithTag("Lecturer");
        float attentionChange = AttectionChange(transform.position, lecturer.transform.position);
        attentionLimit -= attentionChange * Time.deltaTime;
        if (attentionLimit > maxAttentionMultiplier)
            attentionLimit = maxAttentionMultiplier;

        if (attentionLimit <= 0)
            SceneManager.LoadScene("InitialScene", LoadSceneMode.Single);

        // Debug.Log(attentionLimit);
    }

    float AttectionChange(Vector3 position1, Vector3 position2)
    {
        float distance = (position1 - position2).magnitude;
        float attectionChange = (float) Math.Log(distance, 0.2) + 1; // How would it change in a second
        if (attectionChange < 0)
            attectionChange /= 2;  // In case of lowering attention - it lowers twice slower compared to gaining

        return attectionChange;
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

    public int GetPatience()
    {
        return (int) Math.Round(attentionLimit * 100 / maxAttentionMultiplier);
    }


}
