using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab; // ����Ƽ���� ����
    public Transform launchPoint;       // ����ü ��� ��ġ

    public Transform groumdCheck;
    public LayerMask groundLayer;

    [SerializeField]
    private bool isGiant = false;


    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;

    private float baseMoveSpeed = 2.5f;
    public float moveSpeed = 2.5f;
    public float speedBoostDuration = 3f; // �ӵ� ���� ���� �ð�
    public bool isSpeedBoosted = false; // �ӵ� ���� ����
    public string itemTag = "itemTag";

    private float baseJumpForce = 5f;
    public float jumpForce = 5f;
    public float jumpBoostDuration = 5f; // �ӵ� ���� ���� �ð�
    public bool isJumpBoosted = false; // �ӵ� ���� ����

    public KeyCode attackKey = KeyCode.E; // ���� Ű (Inspector���� ���� ����)

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
    }
    void Start()
    {
      
    }

    private float attackCooldown = 0.2f; // �߻� ����
    private float lastAttackTime = 0f;

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

        // ���� Ű�� ������ Attack �Լ� ȣ��
        if (Input.GetKeyDown(attackKey))
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("����!");
      
        if (projectilePrefab != null && launchPoint != null)
        {
            Debug.Log("����ü ���� ��û");
            GameObject thrownProjectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);
            Debug.Log("����ü ���� �Ϸ�");
            Projectile projectile = thrownProjectile.GetComponent<Projectile>();
            if (projectile != null)
            {
                Debug.Log("����ü launch ����");
                Vector3 launchDirection = transform.localScale.x > 0 ? Vector3.left : Vector3.right;
                projectile.Launch(launchDirection * projectile.launchForce);
            }
            else
            {
                Debug.LogError("Projectile ��ũ��Ʈ�� ����ü�� �����ϴ�!");
            }
        }
        else
        {
            Debug.LogWarning("projectilePrefab �Ǵ� launchPoint�� �������� �ʾҽ��ϴ�.");
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�浹��");
        if (collision.CompareTag("Respawn"))
        {
            if (isGiant)
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
        if (collision.CompareTag("itemTag"))
        {
            StartCoroutine(ActivateSpeedBoost());
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Jump"))
        {
            StartCoroutine(ActivateJumpBoost());
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("�浹��");
            if (isGiant)
            {
                Destroy(collision.gameObject);
            }

            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        // �ӵ� ���� ���
        IEnumerator ActivateSpeedBoost()
        {
            float SpeedStartTime = 0f;

            while (SpeedStartTime < speedBoostDuration)
            {
                moveSpeed = baseMoveSpeed * 2f; // �ӵ� ����
                isSpeedBoosted = true; // �ӵ� ���� ���� ǥ��
                SpeedStartTime += Time.deltaTime;
                yield return null;
            }

            moveSpeed = baseMoveSpeed; // ���� �ӵ��� ����
            isSpeedBoosted = false; // �ӵ� ���� ����
        }

        // ���� ���� ���
        IEnumerator ActivateJumpBoost()
        {
            float JumpBoostStartTime = 0f;

            while (JumpBoostStartTime < jumpBoostDuration)
            {
                jumpForce = 7f; // ��������
                isJumpBoosted = true; // ���� ���� ���� ǥ��
                JumpBoostStartTime += Time.deltaTime;
                yield return null;
            }

            jumpForce = baseJumpForce; // ���� ������ ����
            isJumpBoosted = false; // ���� ���� ����
        }



        // switch (collision.tag)

        //    case "item":
        //       isGiant = true;
        //       mj = true;
        //       break;




    }

    
}
 




