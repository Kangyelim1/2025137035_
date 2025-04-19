using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTraceController : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float raycastDistance = 5f;
    public float traceDistance = 10000f;

    public int maxHealth = 3; // 적의 최대 체력 (Inspector에서 조절 가능)
    private int currentHealth; // 적의 현재 체력

    private Transform player;

    private Rigidbody2D rb;
    private bool isTracking = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; // 시작 시 체력을 최대로 설정
    }

    private void Update()
    {
        Vector2 directionToPlayer = (Vector2)player.position - (Vector2)transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        Vector2 directionNormalized = directionToPlayer.normalized;

        if (distanceToPlayer <= traceDistance)
        {
            isTracking = true;
        }

        if (isTracking)
        {
            // 플레이어를 향해 이동
            transform.Translate(directionNormalized * moveSpeed * Time.deltaTime);
            return; // 이동 후에는 다른 로직을 실행할 필요가 없으므로 return
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionNormalized, raycastDistance);
        Debug.DrawRay(transform.position, directionNormalized * raycastDistance, Color.red);

        bool blocked = false;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
            {
                blocked = true;
                break;
            }
        }

        if (!blocked && !isTracking && distanceToPlayer <= traceDistance)
        {
            transform.Translate(directionNormalized * moveSpeed * Time.deltaTime);
        }


    }

    public void TakeDamage(int damageAmount) // 데미지 양을 인자로 받도록 수정
    {
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + "이(가) " + damageAmount + " 데미지를 받음. 현재 체력: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " 사망!");
        Destroy(gameObject); // 적 오브젝트 파괴 (원하는 사망 처리 추가 가능)
    }

   
}

    // Update is called once per frame
    //void Update()
    //{
    //   Vector2 direction = player.position - transform.position;

    //  if (direction.magnitude > traceDistance)
    // return;

    //   Vector2 directionNormaLized = direction.normalized;
    //
    //   RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionNormaLized, raycastDistance);
    //   Debug.DrawRay(transform.position, directionNormaLized * raycastDistance, Color.red);

    //  foreach (RaycastHit2D rHit in hits)
    //  {
    //      if (rHit.collider != null && rHit.collider.CompareTag("Obstacle"))
    //      {
    //          Vector3 alternativeDirection = Quaternion.Euler(0f, 0f, -90f) * direction;
    //         transform.Translate(alternativeDirection * moveSpeed * Time.deltaTime);
    //    }
    //    else
    //    {
    //  transform.Translate(direction * moveSpeed * Time.deltaTime);
    //    }
    // }


