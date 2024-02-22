using UnityEngine;

public class FishController : MonoBehaviour
{
	public float m_MaxSpeed = 2.0f;  //�ְ� ���ǵ�
	public float m_MaxTurnSpeed = 0.5f;  //ȸ����
	public float m_Speed;   //�ʱ�ӵ�
	public float m_NeighborDistance = 3.0f;  //�ֺ� �������� �Ÿ�
	private bool m_IsTurning = false;

	private Flock flock;

	void Start()
	{
		m_Speed = Random.Range(0.5f, m_MaxSpeed);  //����� �ʱ� �ӵ� ����
		flock = GetComponentInParent<Flock>(); //�θ� ��ũ��Ʈ
	}

	void Update()
	{
		GetIsTurning();

		if (m_IsTurning) //����Ⱑ �����ȿ� �� ������
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

	//����Ⱑ ��迡 �ٴٸ��� ȸ�� ���¸� ����
	void GetIsTurning()
	{
		//����Ⱑ �����ȿ� �����������
		if (Vector3.Distance(transform.position, Vector3.zero) >= flock.m_Boundary)
		{
			m_IsTurning = true;
		}
		else
		{
			m_IsTurning = false;
		}
	}

	//������� ȸ���� ����
	//�ֺ� �������� ��ġ�� �ӵ��� ����Ͽ� �׷��� �߽����� �̵�
	void SetRotation()
	{
		GameObject[] fishes;
		fishes = flock.m_Fishes; 

		Vector3 center = Vector3.zero;  //�׷��� �߽�
		Vector3 avoid = Vector3.zero;   //ȸ�Ǹ� ���� ����
		float speed = 0.1f;   //�׷��� ��� �ӵ�

		Vector3 targetPosition = flock.m_TargetPosition; 

		float distance;
		int groupSize = 0;

		for (int i = 0; i < fishes.Length; i++)
		{
			if (fishes[i] != gameObject) //�ڽſ� ������ �˻�
			{
				// �ֺ� ����� ���� �Ÿ� ���
				distance = Vector3.Distance(fishes[i].transform.position, transform.position);

				//�ֺ� ����Ⱑ ���� ������ �������
				if (distance <= m_NeighborDistance)
				{
					center += fishes[i].transform.position; //�׷��� �߽��� ������Ʈ
					groupSize++;

					//�ֺ� ����Ⱑ �ʹ� ����� ���
					if (distance < 0.75f)
					{
						//ȸ�� ���͸� ������Ʈ
						avoid += (transform.position - fishes[i].transform.position);
					}

					//�ֺ� ������� ��� �ӵ��� ������Ʈ
					FishController anotherFish = fishes[i].GetComponent<FishController>();
					speed += anotherFish.m_Speed;
				}
			}
		}

		//�ֺ��� ����Ⱑ ���� ���:
		if (groupSize > 0)
		{
			//�׷��� �߽��� ����ϰ� �̵� ��ǥ �������� ���ϵ���
			center = center / groupSize + (targetPosition - transform.position);
			m_Speed = speed / groupSize; //������� ��� �ӵ��� ������Ʈ

			Vector3 direction = (center + avoid) - transform.position;  //�̵������� ����
			if (direction != Vector3.zero) //�̵� ������ �����Ѵٸ� �Ʒ��� �ڵ带 ����
			{
				//Quaternion.Slerp�� ���� ȸ������ ��ǥ ȸ�������� ȸ���� �ε巴�� ����
				transform.rotation = Quaternion.Slerp(transform.rotation,
					Quaternion.LookRotation(direction),
					TurnSpeed() * Time.deltaTime);
			}
		}
	}

	//ȸ���ӵ� �������� ��ȯ
	float TurnSpeed()
	{
		return Random.Range(0.2f, m_MaxTurnSpeed);
	}
}