using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerN : MonoBehaviour
{

    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public PlayerInput Input { get; private set; }
    public CharacterController Controller { get; private set; }

    public ForceReceiver ForceReceiver { get; private set; }

    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();

        stateMachine = new PlayerStateMachine(this);
    }


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //커서 사라지게
        stateMachine.ChangeState(stateMachine.IdleState);

    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}
