using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag = 0.3f;

    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    void Update()
    {

        //벨루시티가 0보다 작으면서 땅에 붙어있는지 확인
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            //그에 맞는 중력 설정
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            //땅이 아니라면 계속 중력 과부하
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        //저항?
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    public void Reset()
    {
        impact = Vector3.zero;
        verticalVelocity = 0f;
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }
}

