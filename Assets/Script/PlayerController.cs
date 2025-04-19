using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
  
   
    public Transform groumdCheck;
    public LayerMask groundLayer;

    [SerializeField]
    private bool isGiant = false;


    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;

    private float baseMoveSpeed = 2.5f;
    public float moveSpeed = 2.5f;
    public float speedBoostDuration = 3f; // 속도 증가 지속 시간
    public bool isSpeedBoosted = false; // 속도 증가 여부
    public string itemTag = "itemTag";

    private float baseJumpForce = 5f;
    public float jumpForce = 5f;
    public float jumpBoostDuration = 5f; // 속도 증가 지속 시간
    public bool isJumpBoosted = false; // 속도 증가 여부

    public KeyCode attackKey = KeyCode.E; // 공격 키 (Inspector에서 변경 가능)

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
    }
    void Start()
    {
      
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

        // 공격 키가 눌리면 Attack 함수 호출
        if (Input.GetKeyDown(attackKey))
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("공격!");
        // 여기에 실제 공격 로직을 구현하세요.
        // 예: 애니메이션 재생, 투사체 발사, 데미지 처리 등
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌됨");
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

        // 속도 증가 기능
        IEnumerator ActivateSpeedBoost()
        {
            float SpeedStartTime = 0f;

            while (SpeedStartTime < speedBoostDuration)
            {
                moveSpeed = baseMoveSpeed * 2f; // 속도 증가
                isSpeedBoosted = true; // 속도 증가 상태 표시
                SpeedStartTime += Time.deltaTime;
                yield return null;
            }

            moveSpeed = baseMoveSpeed; // 원래 속도로 복귀
            isSpeedBoosted = false; // 속도 증가 종료
        }

        // 점프 관련 기능
        IEnumerator ActivateJumpBoost()
        {
            float JumpBoostStartTime = 0f;

            while (JumpBoostStartTime < jumpBoostDuration)
            {
                jumpForce = 7f; // 점프증가
                isJumpBoosted = true; // 점프 증가 상태 표시
                JumpBoostStartTime += Time.deltaTime;
                yield return null;
            }

            jumpForce = baseJumpForce; // 원래 점프로 복귀
            isJumpBoosted = false; // 점프 증가 종료
        }



        // switch (collision.tag)

        //    case "item":
        //       isGiant = true;
        //       mj = true;
        //       break;




    }

    
}
 




