using UnityEngine;

public class FishController : MonoBehaviour
{
	public float m_MaxSpeed = 2.0f;  //최고 스피드
	public float m_MaxTurnSpeed = 0.5f;  //회전력
	public float m_Speed;   //초기속도
	public float m_NeighborDistance = 3.0f;  //주변 물고기와의 거리
	private bool m_IsTurning = false;

	private Flock flock;

	void Start()
	{
		m_Speed = Random.Range(0.5f, m_MaxSpeed);  //물고기 초기 속도 설정
		flock = GetComponentInParent<Flock>(); //부모 스크립트
	}

	void Update()
	{
		GetIsTurning();

		if (m_IsTurning) //물고기가 영역안에 들어가 있을때
		{
			Vector3 direction = Vector3.zero - transform.position; 
			transform.rotation = Quaternion.Slerp(transform.rotation,
				Quaternion.LookRotation(direction),
				TurnSpeed() * Time.deltaTime);
			m_Speed = Random.Range(0.5f, m_MaxSpeed);
		}
		else
		{
			if (Random.Range(0, 5) < 1)
				SetRotation();
		}

		transform.Translate(0, 0, Time.deltaTime * m_Speed);
	}

	//물고기가 경계에 다다르면 회전 상태를 설정
	void GetIsTurning()
	{
		//물고기가 영역안에 들어있을때임
		if (Vector3.Distance(transform.position, Vector3.zero) >= flock.m_Boundary)
		{
			m_IsTurning = true;
		}
		else
		{
			m_IsTurning = false;
		}
	}

	//물고기의 회전을 설정
	//주변 물고기들의 위치와 속도를 고려하여 그룹의 중심으로 이동
	void SetRotation()
	{
		GameObject[] fishes;
		fishes = flock.m_Fishes; 

		Vector3 center = Vector3.zero;  //그룹의 중심
		Vector3 avoid = Vector3.zero;   //회피를 위한 백터
		float speed = 0.1f;   //그룹의 평균 속도

		Vector3 targetPosition = flock.m_TargetPosition; 

		float distance;
		int groupSize = 0;

		for (int i = 0; i < fishes.Length; i++)
		{
			if (fishes[i] != gameObject) //자신외 물고기들 검사
			{
				// 주변 물고기 사이 거리 계산
				distance = Vector3.Distance(fishes[i].transform.position, transform.position);

				//주변 물고기가 일정 범위내 있을경우
				if (distance <= m_NeighborDistance)
				{
					center += fishes[i].transform.position; //그룹의 중심을 업데이트
					groupSize++;

					//주변 물고기가 너무 가까운 경우
					if (distance < 0.75f)
					{
						//회피 벡터를 업데이트
						avoid += (transform.position - fishes[i].transform.position);
					}

					//주변 물고기의 평균 속도를 업데이트
					FishController anotherFish = fishes[i].GetComponent<FishController>();
					speed += anotherFish.m_Speed;
				}
			}
		}

		//주변에 물고기가 있을 경우:
		if (groupSize > 0)
		{
			//그룹의 중심을 계산하고 이동 목표 지점으로 향하도록
			center = center / groupSize + (targetPosition - transform.position);
			m_Speed = speed / groupSize; //물고기의 평균 속도를 업데이트

			Vector3 direction = (center + avoid) - transform.position;  //이동방향을 구함
			if (direction != Vector3.zero) //이동 방향이 존재한다면 아래의 코드를 실행
			{
				//Quaternion.Slerp는 현재 회전에서 목표 회전까지의 회전을 부드럽게 보간
				transform.rotation = Quaternion.Slerp(transform.rotation,
					Quaternion.LookRotation(direction),
					TurnSpeed() * Time.deltaTime);
			}
		}
	}

	//회전속도 랜덤으로 반환
	float TurnSpeed()
	{
		return Random.Range(0.2f, m_MaxTurnSpeed);
	}
}