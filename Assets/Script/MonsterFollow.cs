using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterFollow : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent가 없습니다! 몬스터 오브젝트에 추가해 주세요.");
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        // 일정 거리 안에 들어오면 따라감
        if (distance < 10f)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
        }
    }
}