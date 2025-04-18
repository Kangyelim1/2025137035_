using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private bool isMovingRight = true;

    public int maxHitPoints = 10; // �ִ� �ǰ� Ƚ�� (���ϴ� ������ ���� ����)
    private int currentHitPoints = 0; // ���� �ǰ� Ƚ��


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
        Debug.Log(gameObject.name + "��(��) ���ݹ���. ���� �ǰ� Ƚ��: " + currentHitPoints);

        if (currentHitPoints >= maxHitPoints)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " ���!");
        // ���⿡ �� ��� �� ó���� ������ �߰��մϴ�.
        // ��: �� �ı�, ������ ���, ���� ���� ��
        Destroy(gameObject); // �� �ı� ����
    }

}
