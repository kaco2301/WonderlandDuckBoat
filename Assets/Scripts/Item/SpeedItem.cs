using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : MonoBehaviour
{
    public float boostDuration = 1f; // �̵� �ӵ��� �÷��ִ� �ð�(��)
    public float boostAmount = 1.5f; // �̵� �ӵ��� �󸶳� �÷����� ����

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾� �̵� �ӵ��� �÷���
            BoatMove playerMovement = other.GetComponent<BoatMove>();

            if (playerMovement != null)
            {
                playerMovement.ApplySpeedBoost(boostDuration, boostAmount);
            }

            Destroy(gameObject); // �������� ������ �����
        }
    }
}
