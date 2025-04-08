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
        float radius = 0.75f; // 플레이어 중심에서의 거리
        float angleStep = 360f / equippedWeapons.Count;
        float startAngle = -90f; // 위쪽부터 시작

        for (int i = 0; i < equippedWeapons.Count; i++)
        {
            float angle = startAngle + (angleStep * i);
            float rad = angle * Mathf.Deg2Rad;

            Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
            GameObject weaponGO = Instantiate(weaponPrefab, transform.position + offset, Quaternion.identity, transform);

            // 방향 이미지 보정을 위해 45도 추가 회전
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offset.normalized);
            rotation *= Quaternion.Euler(0f, 0f, 45f); // 이미지 보정용 회전

            weaponGO.transform.rotation = rotation;

            // 데이터 적용
            weaponGO.GetComponent<WeaponBehavior>().Initialize(equippedWeapons[i]);

            // 관리용 리스트에 추가
            weaponInstances.Add(weaponGO);
        }
    }
}
