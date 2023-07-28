using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatFloat : MonoBehaviour
{
    public float floatStrength = 0.1f; // �� ������ ���� ����
    public float floatSpeed = 2.0f; // �� ���� �ӵ�

    private Vector3 originalPosition;

    void Start()
    {
        // ���� ��ġ�� �����մϴ�.
        originalPosition = transform.position;
    }

    void Update()
    {
        // Time.time�� Mathf.Sin �Լ��� ����Ͽ� ������ �������� ����ϴ�.
        float newYPosition = originalPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatStrength;

        // Transform�� ��ġ�� ���ο� ��ġ�� ������Ʈ�մϴ�.
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }
}
