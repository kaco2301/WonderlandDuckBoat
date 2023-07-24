using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMove : MonoBehaviour
{
    public float moveSpeed = 10f;                     // �̵� �ӵ�
    private float currentSpeed;
    private float currentSpeedMultiplier = 1f; // ���� �̵� �ӵ� ���� (�⺻�� 1: ���� �̵� �ӵ�)

    [SerializeField] float rotationSpeed = 40f;                 // ȸ�� �ӵ�


    private Rigidbody rb;                                       //

    public bool isMovementEnabled = true;                       // �÷��̾� �̵� ���� ����

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        currentSpeed = moveSpeed;
    }

    private void Update()
    {
        if (isMovementEnabled)
        {
            BoatMovement();
            BoatRotation();
        }
    }

    private void BoatMovement()
    {
        float Vert = Input.GetAxis("Vertical");

        // �̵�
        Move(Vert);
    }

    public void Move(float verticalInput)
    {
        // �̵�
        float amount = currentSpeed * Time.deltaTime * verticalInput; // ���� �����ӿ��� �̵��� �Ÿ�
        rb.MovePosition(rb.position + transform.forward * amount);
    }

    private void BoatRotation()
    {
        float Horz = Input.GetAxis("Horizontal");

        // ȸ��
        Rotate(Horz);
    }

    public void Rotate(float horizontalInput)
    {
        // ȸ��
        float amountRot = rotationSpeed * Time.deltaTime * horizontalInput; // ���� �����ӿ��� ȸ���� ����                                                   
        rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * amountRot));
    }

    public void ApplySpeedBoost(float duration, float boostAmount)
    {
        // �̵� �ӵ��� �÷��ִ� �ڷ�ƾ ����
        StartCoroutine(BoostSpeed(duration, boostAmount));
    }

    public void ApplySpeedReduce(float duration,float reduceAmount)
    {
        StartCoroutine(ReduceSpeed(duration, reduceAmount));
    }

    private IEnumerator BoostSpeed(float duration, float boostAmount)
    {
        //SpeedItem���� ���
        currentSpeed *= boostAmount;
        //�ν�Ʈ�縸ŭ �ӵ�����
        yield return new WaitForSeconds(duration);
        //�ν�Ʈ�縸ŭ �ӵ�����
        currentSpeed /= boostAmount;
    }
    private IEnumerator ReduceSpeed(float duration, float reduceAmount)
    {
        currentSpeed /= reduceAmount;
        yield return new WaitForSeconds(duration);
        currentSpeed *= reduceAmount;

    }

    public void SetSpeedMultiplier(float multiplier)
    {
        currentSpeedMultiplier = multiplier;
    }

    // �̵� �ӵ� ���� ������� ����
    public void ResetSpeedMultiplier()
    {
        currentSpeedMultiplier = 1f;
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
