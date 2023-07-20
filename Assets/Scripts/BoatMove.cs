using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;                     // �̵� �ӵ�
    [SerializeField] float boatRotSpeed = 40f;                  // ȸ�� �ӵ�

    [SerializeField] float shakeSpeed = 2f;                   // ��鸮�� �ӵ�
    [SerializeField] float shakeAmount = 0.1f;                  // ��鸮�� ����

    [SerializeField] float rotateAmount = 1.0f;                 // ��鸰 ���¿����� ȸ�� ����

    GameObject[] Items;

    private Vector3 initialPosition;                            // �ʱ� ȸ������ �����ϴ� ����
    private Quaternion initialRotation;                         // �ʱ� ȸ����

    private Rigidbody rigidBody;
    private bool isMovementEnabled = true;

    bool isSpeedBoosted = false; // �̵��ӵ� ���� ȿ�� ����
    float boostDuration = 1f; // �̵��ӵ� ���� ���� �ð�
    float boostAmount = 10f; // �̵��ӵ� ������
    Coroutine boostCoroutine = null; // ���� �̵��ӵ� ���� �ڷ�ƾ ����



    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        rigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isMovementEnabled)
        {
            BoatMovement();
        }

        // ��Ʈ�� ��鸲 ȿ�� ����
        wave();
    }

    private void BoatMovement()
    {
        float Horz = Input.GetAxis("Horizontal");
        float Vert = Input.GetAxis("Vertical");

        //�̵�
        float amount = moveSpeed * Time.deltaTime * Vert; // ���� �����ӿ��� �̵��� �Ÿ�
        rigidBody.MovePosition(rigidBody.position + transform.forward * amount);

        //ȸ��
        float amountRot = boatRotSpeed * Time.deltaTime * Horz; // ���� �����ӿ��� ȸ���� ����
        rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(Vector3.up * amountRot));
    }

    private void wave()
    {
        // sin �Լ��� ����Ͽ� ��Ʈ�� ��鵵�� ��
        float shake = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

        // ��Ʈ ������Ʈ�� ���� ȸ������ �����Ͽ� ��鸮�� ȿ���� ��
        Vector3 position = initialPosition;
        position.y += shake;
        transform.position = position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpeedItem")               // �̵��ӵ� ������
        {
            other.gameObject.SetActive(false);      // ���� ������Ʈ ��Ȱ��ȭ
                                                    // �̵��ӵ� ���� ȿ���� �ߵ��ϰ�, boostCoroutine�� �ڷ�ƾ ���� ����
            if (boostCoroutine != null)
                StopCoroutine(boostCoroutine);      // ���� ȿ���� �ߴܽ�Ŵ
            boostCoroutine = StartCoroutine(ActivateSpeedBoost());
        }

        if(other.tag == "ScoreItem") // ���� ������
        {
            other.gameObject.SetActive(false);

        }

        if(other.tag == "TrapItem") // �������� ������
        {
            other.gameObject.SetActive(false);
        }

    }
    IEnumerator ActivateSpeedBoost()
    {
        isSpeedBoosted = true; // �̵��ӵ� ���� ȿ�� Ȱ��ȭ
        moveSpeed += boostAmount; // �̵��ӵ� ����

        yield return new WaitForSeconds(boostDuration);

        moveSpeed = 10f; // �̵��ӵ� ������� �ǵ�����
        isSpeedBoosted = false; // �̵��ӵ� ���� ȿ�� ��Ȱ��ȭ

        // �ڷ�ƾ ���� �ʱ�ȭ
        boostCoroutine = null;
    }

    public void EnableMovement()
    {
        isMovementEnabled = true;
    }

    public void DisableMovement()
    {
        isMovementEnabled = false;
    }
}
