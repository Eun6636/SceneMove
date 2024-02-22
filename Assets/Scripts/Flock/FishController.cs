using UnityEngine;

public class FishController : MonoBehaviour
{
	public float MaxSpeed = 2.0f;  //�ְ� ���ǵ�
	public float MaxTurnSpeed = 0.5f;  //ȸ����
	public float Speed;   //�ʱ�ӵ�
	public float NeighborDistance = 3.0f;  //�ֺ� �������� �Ÿ�
	private bool IsTurning = false;

	private Flock flock;

	void Start()
	{
		Speed = Random.Range(0.5f, MaxSpeed);  //����� �ʱ� �ӵ� ����
		flock = GetComponentInParent<Flock>(); //�θ� ��ũ��Ʈ
	}

	void Update()
	{
		GetIsTurning();

		if (IsTurning) //����Ⱑ �����ȿ� �� ������
		{
			Vector3 direction = Vector3.zero - transform.position; 
			transform.rotation = Quaternion.Slerp(transform.rotation,
				Quaternion.LookRotation(direction),
				TurnSpeed() * Time.deltaTime);
			Speed = Random.Range(0.5f, MaxSpeed);
		}
		else
		{
			if (Random.Range(0, 5) < 1)
				SetRotation();
		}

		transform.Translate(0, 0, Time.deltaTime * Speed);
	}

	//����Ⱑ ��迡 �ٴٸ��� ȸ�� ���¸� ����
	void GetIsTurning()
	{
		//����Ⱑ �����ȿ� �����������
		if (Vector3.Distance(transform.position, Vector3.zero) >= flock.Boundary)
		{
			IsTurning = true;
		}
		else
		{
			IsTurning = false;
		}
	}

	//������� ȸ���� ����
	//�ֺ� �������� ��ġ�� �ӵ��� ����Ͽ� �׷��� �߽����� �̵�
	void SetRotation()
	{
		GameObject[] fishes;
		fishes = flock.Fishes; 

		Vector3 center = Vector3.zero;  //�׷��� �߽�
		Vector3 avoid = Vector3.zero;   //ȸ�Ǹ� ���� ����
		float speed = 0.1f;   //�׷��� ��� �ӵ�

		Vector3 targetPosition = flock.TargetPosition; 

		float distance;
		int groupSize = 0;

		for (int i = 0; i < fishes.Length; i++)
		{
			if (fishes[i] != gameObject) //�ڽſ� ������ �˻�
			{
				// �ֺ� ����� ���� �Ÿ� ���
				distance = Vector3.Distance(fishes[i].transform.position, transform.position);

				//�ֺ� ����Ⱑ ���� ������ �������
				if (distance <= NeighborDistance)
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
					speed += anotherFish.Speed;
				}
			}
		}

		//�ֺ��� ����Ⱑ ���� ���:
		if (groupSize > 0)
		{
			//�׷��� �߽��� ����ϰ� �̵� ��ǥ �������� ���ϵ���
			center = center / groupSize + (targetPosition - transform.position);
			Speed = speed / groupSize; //������� ��� �ӵ��� ������Ʈ

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
		return Random.Range(0.2f, MaxTurnSpeed);
	}
}