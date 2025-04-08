using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public float detectRange = 1f;  // 무기별 감지 범위 (자동 계산 or 수동 설정 가능)
    public LayerMask enemyLayer;

    [HideInInspector] public Transform target; // 가장 가까운 적

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

        // 적이 있다면 그쪽을 바라봄
        if (target != null)
        {
            Vector2 dir = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 45); // 이미지 보정 45도
        }
    }

    // 편의용 디버그
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
