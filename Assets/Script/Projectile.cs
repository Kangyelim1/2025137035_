using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damageAmount = 1; // ���� �ִ� ������ ��
    public float lifeTime = 2f; // ���� ���� (��)

    private Transform target;
    void Start()
    {
        Destroy(gameObject, lifeTime);
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (target == null)
        {
            Debug.LogError("Player ");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�浹 �߻�! �浹�� ������Ʈ: " + collision.gameObject.name);
        if (target != null)
            if (collision.CompareTag("Enemy"))
            {
                EnemyTraceController enemy = collision.GetComponent<EnemyTraceController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageAmount); // ������ ����
                    Debug.Log("�浹 ������. Ÿ��: " + target.name);
                }
                Destroy(gameObject); // �浹 �� ����
            }
            else
            {
                Debug.LogWarning("Player ");
            }
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            // target�� ����� ��� ��ü ���� (��: ���� ����, ��� ���� ��)
        }
    }
    public float speed = 10f;
    public Vector2 direction;

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // ȭ�� �� ������ ����
    }
}
