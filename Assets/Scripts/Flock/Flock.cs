using UnityEngine;

public class Flock : MonoBehaviour
{
    public GameObject c_prefebFish;  //���� ������
    public GameObject p_prefebFish;  //���� ������
    public int m_Fish = 30;   // ������ �����

    public int m_Boundary = 7;  //����Ⱑ �����Ӱ� �����ϼ� �ִ� ���
    public GameObject[] m_Fishes;
    public Vector3 m_TargetPosition = Vector3.zero;

    public Vector3 centerPosition; //������ �Ǵ� ��ǥ

    void Start()
    {
        m_Fishes = new GameObject[m_Fish];

        m_TargetPosition = centerPosition;

        for (int i = 0; i < m_Fish; i++)
        {
            Vector3 position = centerPosition + new Vector3(
                Random.Range(-m_Boundary, m_Boundary),
                Random.Range(-m_Boundary, m_Boundary),
                Random.Range(-m_Boundary, m_Boundary)
            );

            GameObject fish = (GameObject)Instantiate(c_prefebFish, position, Quaternion.identity);

            fish.transform.parent = p_prefebFish.transform; //�ڽ� ������Ʈ�� ����
            m_Fishes[i] = fish;
        }
    }

    void Update()
    {
        GetTargetPosition();
    }

    void GetTargetPosition()
    {
        if (Random.Range(1, 10000) < 50)
        {
            m_TargetPosition = new Vector3(
                Random.Range(m_TargetPosition.x -m_Boundary, m_TargetPosition.x + m_Boundary),
                Random.Range(3, 13),
                Random.Range(m_TargetPosition.z - m_Boundary, m_TargetPosition.z + m_Boundary)
            );
        }
    }
}