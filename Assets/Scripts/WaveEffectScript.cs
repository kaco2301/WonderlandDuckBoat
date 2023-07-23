using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEffectScript : MonoBehaviour
{
    [SerializeField] float shakeSpeed = 2f;                   // ��鸮�� �ӵ�
    [SerializeField] float shakeAmount = 0.1f;                  // ��鸮�� ����

    [SerializeField] float rotateAmount = 1.0f;                 // ��鸰 ���¿����� ȸ�� ����

    private Vector3 initialPosition;                            // �ʱ� ȸ������ �����ϴ� ����



    private void Start()
    {
        initialPosition = transform.position;
    }
    private void Update()
    {
        // ��Ʈ�� ��鸲 ȿ�� ����
        //Wave();
    }
    private void LateUpdate()
    {
       // Wave();
    }
    private void Wave()
    {
        // sin �Լ��� ����Ͽ� ��Ʈ�� ��鵵�� ��
        float shake = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

        // ��Ʈ ������Ʈ�� ���� ȸ������ �����Ͽ� ��鸮�� ȿ���� ��
        Vector3 position = initialPosition;
        position.y += shake;
        transform.position = position;
    }
}
