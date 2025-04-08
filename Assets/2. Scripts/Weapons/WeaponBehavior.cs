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

        // ������ �̹��� ����
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null && data.icon != null)
        {
            sr.sprite = data.icon;
        }

        // �ݶ��̴� �ڵ� ���� �� ����
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<CircleCollider2D>();
        }

        if (sr != null)
        {
            float radius = Mathf.Max(sr.bounds.extents.x, sr.bounds.extents.y);
            collider.radius = radius * 0.75f; // ��¦ �ٿ��� ����
        }

        collider.isTrigger = true;

        // ���� ���� ����
        Vector3 dir = (transform.position - transform.parent.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, dir);
        transform.rotation = rotation * Quaternion.Euler(0f, 0f, 45f);

        // �ʱ� ��ġ ����
        initialLocalPosition = transform.localPosition;

        // ���� ���� �ڵ� ����
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

        if (data.weaponName.Contains("Į") || data.weaponName.Contains("��"))
        {
            styleToUse = (Random.value < 0.5f) ? WeaponData.AttackStyle.Stab : WeaponData.AttackStyle.Slash;
        }
        else if (data.weaponName.Contains("����"))
        {
            styleToUse = WeaponData.AttackStyle.Slash;
        }
        else if (data.weaponName.Contains("â"))
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
