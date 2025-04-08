using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public float detectRange = 1f;  // ���⺰ ���� ���� (�ڵ� ��� or ���� ���� ����)
    public LayerMask enemyLayer;

    [HideInInspector] public Transform target; // ���� ����� ��

    private void Update()
    {
        FindClosestEnemy();
    }

    void FindClosestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectRange, enemyLayer);

        float closestDistance = float.MaxValue;
        Transform closest = null;

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closest = hit.transform;
            }
        }

        target = closest;

        // ���� �ִٸ� ������ �ٶ�
        if (target != null)
        {
            Vector2 dir = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 45); // �̹��� ���� 45��
        }
    }

    // ���ǿ� �����
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
