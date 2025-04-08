using System.Collections;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    private WeaponData data;
    private EnemyDetector detector;

    private float cooldownTimer = 0f;
    private Vector3 initialLocalPosition;

    public void Initialize(WeaponData weaponData)
    {
        data = weaponData;

        // 아이콘 이미지 적용
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null && data.icon != null)
        {
            sr.sprite = data.icon;
        }

        // 콜라이더 자동 생성 및 설정
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<CircleCollider2D>();
        }

        if (sr != null)
        {
            float radius = Mathf.Max(sr.bounds.extents.x, sr.bounds.extents.y);
            collider.radius = radius * 0.75f; // 살짝 줄여서 설정
        }

        collider.isTrigger = true;

        // 무기 방향 보정
        Vector3 dir = (transform.position - transform.parent.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, dir);
        transform.rotation = rotation * Quaternion.Euler(0f, 0f, 45f);

        // 초기 위치 저장
        initialLocalPosition = transform.localPosition;

        // 감지 범위 자동 설정
        detector = GetComponent<EnemyDetector>();
        if (detector != null)
        {
            float range = data.range;

            if (data.type == WeaponData.WeaponType.Melee)
                range = Mathf.Max(1f, 0.5f + range / 300f);

            detector.detectRange = range;
        }
    }


    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f && detector != null && detector.target != null)
        {
            PerformAttack();
            cooldownTimer = data.cooldown;
        }
    }

    private void PerformAttack()
    {
        Vector3 attackDir = (detector.target.position - transform.position).normalized;

        WeaponData.AttackStyle styleToUse = data.attackStyle;

        if (data.weaponName.Contains("칼") || data.weaponName.Contains("검"))
        {
            styleToUse = (Random.value < 0.5f) ? WeaponData.AttackStyle.Stab : WeaponData.AttackStyle.Slash;
        }
        else if (data.weaponName.Contains("도끼"))
        {
            styleToUse = WeaponData.AttackStyle.Slash;
        }
        else if (data.weaponName.Contains("창"))
        {
            styleToUse = WeaponData.AttackStyle.Stab;
        }

        switch (styleToUse)
        {
            case WeaponData.AttackStyle.Stab:
                StartCoroutine(StabMotion(attackDir));
                break;
            case WeaponData.AttackStyle.Slash:
                StartCoroutine(SlashMotion());
                break;
        }
    }

    private IEnumerator StabMotion(Vector3 dir)
    {
        Vector3 offset = dir * 0.3f;
        transform.localPosition += offset;
        yield return new WaitForSeconds(0.05f);
        transform.localPosition = initialLocalPosition;
    }

    private IEnumerator SlashMotion()
    {
        Quaternion originalRot = transform.rotation;
        float angle = 40f;
        float duration = 0.1f;

        float t = 0f;
        while (t < duration)
        {
            float rotZ = Mathf.Lerp(0f, angle, t / duration);
            transform.rotation = originalRot * Quaternion.Euler(0f, 0f, rotZ);
            t += Time.deltaTime;
            yield return null;
        }

        transform.rotation = originalRot;
    }
}
