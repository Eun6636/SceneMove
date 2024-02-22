using UnityEngine;

public class Flock : MonoBehaviour
{
    public GameObject c_prefebFish;  //���� ������
    public GameObject p_prefebFish;  //���� ������
    public int m_Fish = 30;   // ������ �����

    public int Boundary = 7;  //����Ⱑ �����Ӱ� �����ϼ� �ִ� ���
    public GameObject[] Fishes;
    public Vector3 TargetPosition = Vector3.zero;

    public Vector3 centerPosition; //������ �Ǵ� ��ǥ

    void Start()
    {
        Fishes = new GameObject[m_Fish];

        TargetPosition = centerPosition;

        for (int i = 0; i < m_Fish; i++)
        {
            Vector3 position = centerPosition + new Vector3(
                Random.Range(-Boundary, Boundary),
                Random.Range(-Boundary, Boundary),
                Random.Range(-Boundary, Boundary)
            );

            GameObject fish = (GameObject)Instantiate(c_prefebFish, position, Quaternion.identity);

            fish.transform.parent = p_prefebFish.transform; //�ڽ� ������Ʈ�� ����
            Fishes[i] = fish;
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
            TargetPosition = new Vector3(
                Random.Range(TargetPosition.x -Boundary, TargetPosition.x + Boundary),
                Random.Range(3, 13),
                Random.Range(TargetPosition.z - Boundary, TargetPosition.z + Boundary)
            );
        }
    }
}