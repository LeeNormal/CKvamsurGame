using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // 따라갈 대상 (플레이어)
    public float smoothSpeed = 8f;    // 따라가는 속도 (클수록 빠르게 따라감)
    public Vector3 offset = new Vector3(0, 0, -10); // Z축 고정값

    void LateUpdate()
    {
        if (target == null) return; // 타겟이 없으면 실행 안함

        // 목표 위치 계산 (X, Y는 따라가고 Z는 고정)
        Vector3 desiredPosition = target.position + offset;

        // 부드럽게 따라가기 (선형 보간)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 실제 카메라 위치 갱신
        transform.position = smoothedPosition;
    }
}
