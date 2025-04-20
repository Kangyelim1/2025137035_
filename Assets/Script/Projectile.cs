using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject projectilePrefab; // �߻��� ����ü ������
    public Transform launchPoint;
    public float launchForce = 10f;

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

        // ��ũ��Ʈ�� Ȱ��ȭ�� �� ���� ��Ȱ��ȭ�մϴ�.
       // gameObject.SetActive(false);

        // �߻� �� lifeTime�� ������ �ڵ����� �Ҹ��ŵ�ϴ�.
        Destroy(gameObject, lifeTime);

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

        // Ȱ��ȭ�� �Ŀ��� �̵� ������ �����մϴ�.
        if (gameObject.activeSelf)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // ȭ�� �� ������ ����
    }

    // �ܺο��� ���� �߻��� �� ȣ���ϴ� �Լ�
    public void Launch(Vector3 launchDirection)
    {
        direction = launchDirection;
        // �߻� ������ ���� Ȱ��ȭ�մϴ�.
        gameObject.SetActive(true);
    }

    public void ThrowBall()
    {
        if (projectilePrefab != null && launchPoint != null)
        {
            // ����ü �������� �߻� ��ġ�� �ν��Ͻ�ȭ�մϴ�.
            GameObject thrownProjectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);

            // ����ü�� Projectile ��ũ��Ʈ�� �ִ��� Ȯ���մϴ�.
            Projectile projectile = thrownProjectile.GetComponent<Projectile>();
            if (projectile != null)
            {
                Vector3 launchDirection = launchPoint.forward;
                projectile.Launch(launchDirection * launchForce);
            }
            else
            {
                Debug.LogError("������ ����ü�� Projectile ��ũ��Ʈ�� �����ϴ�!");
            }
        }
        else
        {
            Debug.LogError("Projectile Prefab �Ǵ� Launch Point�� �������� �ʾҽ��ϴ�!");
        }

    }

}
