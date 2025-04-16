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

    private bool isGiant = false;
    private bool mj = false;

    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);


        isGrounded = Physics2D.OverlapCircle(groumdCheck.position, 0.2f, groundLayer);

        if (isGiant)
        {
            if (moveInput < 0)
                transform.localScale = new Vector3(2f, 2f, 1f);

            if (moveInput > 0)
                transform.localScale = new Vector3(-2f, 2f, 1f);
        }
        else
        {
            if (moveInput < 0)
                transform.localScale = new Vector3(1f, 1f, 1f);
            if (moveInput > 0)
                transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            pAni.SetTrigger("JumpAction");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            if(isGiant)
                Destroy(collision.gameObject);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveTonextLevel();
        }
        if (collision.CompareTag("Item"))
        {
            isGiant = true;
            Destroy(collision.gameObject);
        }

       // switch (collision.tag)
        {
        //    case "item":
         //       isGiant = true;
         //       mj = true;
         //       break;
        }
    }
}
