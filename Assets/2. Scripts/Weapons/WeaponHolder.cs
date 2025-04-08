using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public List<WeaponData> equippedWeapons = new List<WeaponData>();
    public GameObject weaponPrefab;

    private List<GameObject> weaponInstances = new List<GameObject>();

    private void Start()
    {
        SpawnWeapons();
    }

    private void SpawnWeapons()
    {
        float radius = 0.75f; // �÷��̾� �߽ɿ����� �Ÿ�
        float angleStep = 360f / equippedWeapons.Count;
        float startAngle = -90f; // ���ʺ��� ����

        for (int i = 0; i < equippedWeapons.Count; i++)
        {
            float angle = startAngle + (angleStep * i);
            float rad = angle * Mathf.Deg2Rad;

            Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
            GameObject weaponGO = Instantiate(weaponPrefab, transform.position + offset, Quaternion.identity, transform);

            // ���� �̹��� ������ ���� 45�� �߰� ȸ��
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offset.normalized);
            rotation *= Quaternion.Euler(0f, 0f, 45f); // �̹��� ������ ȸ��

            weaponGO.transform.rotation = rotation;

            // ������ ����
            weaponGO.GetComponent<WeaponBehavior>().Initialize(equippedWeapons[i]);

            // ������ ����Ʈ�� �߰�
            weaponInstances.Add(weaponGO);
        }
    }
}
