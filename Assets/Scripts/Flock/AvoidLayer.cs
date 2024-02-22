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

        // ����ĳ��Ʈ�� �̿��Ͽ� ���ؾ� �� ���̾��� ��ֹ� ����
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, avoidanceRadius, transform.forward, 0f, obstacleLayer);

        foreach (RaycastHit hit in hits)
        {
            // ���ؾ� �� ���̾��� ��ֹ��� ������ ���, ���ϴ� ���� ���
            Vector3 obstacleDirection = transform.position - hit.transform.position;
            avoidanceDirection += obstacleDirection.normalized * (avoidanceRadius - obstacleDirection.magnitude);
        }

        // ���ϴ� ������ ���� ��ġ�� �ݿ��Ͽ� �̵�
        transform.position += avoidanceDirection.normalized * maxSpeed * Time.deltaTime;
    }
}
