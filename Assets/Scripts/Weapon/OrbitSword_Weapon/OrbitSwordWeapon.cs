using UnityEngine;

public class OrbitSwordWeapon : WeaponBase
{
    [Header("설정")]
    [SerializeField] private Transform swordVisual;
    [SerializeField] private float rotateSpeed = 90f;

    private float angle = 0f;

    public override void Initialize()
    {
        weaponName = "OrbitSword";
        angle = 0f;

        if (swordVisual == null)
        {
            Debug.LogError("SwordVisual이 연결되지 않았습니다!");
            return;
        }

        UpdatePositionAndRotation();
    }

    public override void Attack()
    {
        if (swordVisual == null) return;

        angle += rotateSpeed * Time.deltaTime;
        if (angle >= 360f) angle -= 360f;

        UpdatePositionAndRotation();
    }

    private void UpdatePositionAndRotation()
    {
        float rad = angle * Mathf.Deg2Rad;
        float radius = swordVisual.localPosition.magnitude;
        Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * radius;
        swordVisual.position = transform.position + offset;

        Vector3 direction = (swordVisual.position - transform.position).normalized;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        swordVisual.rotation = Quaternion.Euler(0f, 0f, rotZ - 45f);
    }

    public override void UpgradeDamage()
    {
        damage += 10f;
        level++;
        Debug.Log($"{weaponName} 데미지 업! 현재 데미지: {damage}");
    }

    public override void UpgradeSpeed()
    {
        rotateSpeed += 30f;
        level++;
        Debug.Log($"{weaponName} 회전 속도 업! 현재 회전 속도: {rotateSpeed}");
    }
}
