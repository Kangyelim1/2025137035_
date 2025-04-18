using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private bool isMovingRight = true;

    public int maxHitPoints = 10; // 최대 피격 횟수 (원하는 값으로 변경 가능)
    private int currentHitPoints = 0; // 현재 피격 횟수


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isMovingRight)
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        else
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            isMovingRight = !isMovingRight;
        }
    }
    public void TakeDamage()
    {
        currentHitPoints++;
        Debug.Log(gameObject.name + "이(가) 공격받음. 현재 피격 횟수: " + currentHitPoints);

        if (currentHitPoints >= maxHitPoints)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " 사망!");
        // 여기에 적 사망 시 처리할 로직을 추가합니다.
        // 예: 적 파괴, 아이템 드랍, 점수 증가 등
        Destroy(gameObject); // 적 파괴 예시
    }

}
