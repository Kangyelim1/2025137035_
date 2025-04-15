using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groumdCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator pAni;

    private bool isGiant = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if(isGiant)
        {
            if (moveInput < 0)
                transform.localScale = new Vector3(2f, 2f, 1f);

            if (moveInput < 0)
                transform.localScale = new Vector3(-2f, 2f, 1f);
        }

        else
        {
            if (moveInput < 0)
                transform.localScale = new Vector3(1f, 1f, 1f);

            if (moveInput > 0)
                transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        isGrounded = Physics2D.OverlapCircle(groumdCheck.position, 0.2f, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

      if (collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveTonextLevel();
        }

      if(collision.CompareTag("Enemy"))
        {
            if (isGiant)
                Destroy(Collision.gameObject);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

      if (collision.CompareTag("Item"))
        {
            isGiant = true;
            Destroy(collision.gameObject);
        }
    }
}
