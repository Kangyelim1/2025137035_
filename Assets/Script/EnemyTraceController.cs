using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTraceController : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float raycastDistance = 5f;
    public float traceDistance = 10000f;


    private Transform player;

    private bool isTracking = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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


