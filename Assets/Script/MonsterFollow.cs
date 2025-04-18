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
            Debug.LogError("NavMeshAgent�� �����ϴ�! ���� ������Ʈ�� �߰��� �ּ���.");
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        // ���� �Ÿ� �ȿ� ������ ����
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