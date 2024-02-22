using UnityEngine;

public class Flock : MonoBehaviour
{
    public GameObject c_prefebFish;  //하위 프리팹
    public GameObject p_prefebFish;  //상위 프리팹
    public int m_Fish = 30;   // 생성할 물고기

    public int m_Boundary = 7;  //물고기가 자유롭게 움직일수 있는 경계
    public GameObject[] m_Fishes;
    public Vector3 m_TargetPosition = Vector3.zero;

    public Vector3 centerPosition; //기준이 되는 좌표

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

            fish.transform.parent = p_prefebFish.transform; //자식 오브젝트로 엮음
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