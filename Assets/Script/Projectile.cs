using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject projectilePrefab; // 발사할 투사체 프리팹
    public Transform launchPoint;
    public float launchForce = 10f;

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

        // 스크립트가 활성화될 때 공을 비활성화합니다.
       // gameObject.SetActive(false);

        // 발사 후 lifeTime이 지나면 자동으로 소멸시킵니다.
        Destroy(gameObject, lifeTime);

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

        // 활성화된 후에만 이동 로직을 실행합니다.
        if (gameObject.activeSelf)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // 화면 밖 나가면 제거
    }

    // 외부에서 공을 발사할 때 호출하는 함수
    public void Launch(Vector3 launchDirection)
    {
        direction = launchDirection;
        // 발사 시점에 공을 활성화합니다.
        gameObject.SetActive(true);
    }

    public void ThrowBall()
    {
        if (projectilePrefab != null && launchPoint != null)
        {
            // 투사체 프리팹을 발사 위치에 인스턴스화합니다.
            GameObject thrownProjectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);

            // 투사체에 Projectile 스크립트가 있는지 확인합니다.
            Projectile projectile = thrownProjectile.GetComponent<Projectile>();
            if (projectile != null)
            {
                Vector3 launchDirection = launchPoint.forward;
                projectile.Launch(launchDirection * launchForce);
            }
            else
            {
                Debug.LogError("생성된 투사체에 Projectile 스크립트가 없습니다!");
            }
        }
        else
        {
            Debug.LogError("Projectile Prefab 또는 Launch Point가 설정되지 않았습니다!");
        }

    }

}
