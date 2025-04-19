using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damageAmount = 1; // 공이 주는 데미지 양
    public float lifeTime = 2f; // 공의 수명 (초)

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
        Debug.Log("충돌 발생! 충돌한 오브젝트: " + collision.gameObject.name);
        if (target != null)
            if (collision.CompareTag("Enemy"))
            {
                EnemyTraceController enemy = collision.GetComponent<EnemyTraceController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageAmount); // 데미지 전달
                    Debug.Log("충돌 감지됨. 타겟: " + target.name);
                }
                Destroy(gameObject); // 충돌 후 삭제
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
            // target이 사라진 경우 대체 로직 (예: 추적 멈춤, 대기 상태 등)
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
        Destroy(gameObject); // 화면 밖 나가면 제거
    }
}
