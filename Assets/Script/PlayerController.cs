using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groumdCheck;
    public LayerMask groundLayer;

    [SerializeField]
    private bool isGiant = false;
    private bool mj = false;

    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;

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

        if (isGrounded && Input.GetKeyDown(KeyCode.E))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            pAni.SetTrigger("JumpAction");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            FireProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌됨");
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

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("충돌됨");
            if (isGiant)
            {
                Destroy(collision.gameObject);
            }

            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage();
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                EnemyController enemy = collision.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.TakeDamage();
                }
                Destroy(gameObject); // 적과 충돌 시 공 파괴
            }
        }

        // switch (collision.tag)
        {
        //    case "item":
         //       isGiant = true;
         //       mj = true;
         //       break;
        }
        

    }
    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if (projectileRb != null)
        {
            projectileRb.velocity = firePoint.right * projectileSpeed;
        }
        Destroy(projectile, 2f); // 2초 후 자동으로 파괴
    }
}



