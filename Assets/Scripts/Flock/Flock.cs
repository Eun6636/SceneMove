using UnityEngine;

public class Flock : MonoBehaviour
{
    public GameObject c_prefebFish;  //하위 프리팹
    public GameObject p_prefebFish;  //상위 프리팹
    public int m_Fish = 30;   // 생성할 물고기

    public int Boundary = 7;  //물고기가 자유롭게 움직일수 있는 경계
    public GameObject[] Fishes;
    public Vector3 TargetPosition = Vector3.zero;

    public Vector3 centerPosition; //기준이 되는 좌표

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

            fish.transform.parent = p_prefebFish.transform; //자식 오브젝트로 엮음
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