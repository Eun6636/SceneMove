using UnityEngine;

public class AvoidLayer : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public float avoidanceRadius = 5f;
    public float avoidanceForce = 10f;
    public float maxSpeed = 5f;

    private void FixedUpdate()
    {
        Vector3 avoidanceDirection = Vector3.zero;

        // 레이캐스트를 이용하여 피해야 할 레이어의 장애물 감지
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, avoidanceRadius, transform.forward, 0f, obstacleLayer);

        foreach (RaycastHit hit in hits)
        {
            // 피해야 할 레이어의 장애물이 감지된 경우, 피하는 방향 계산
            Vector3 obstacleDirection = transform.position - hit.transform.position;
            avoidanceDirection += obstacleDirection.normalized * (avoidanceRadius - obstacleDirection.magnitude);
        }

        // 피하는 방향을 현재 위치에 반영하여 이동
        transform.position += avoidanceDirection.normalized * maxSpeed * Time.deltaTime;
    }
}
