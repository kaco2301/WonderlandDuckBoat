using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMove : MonoBehaviour
{
    [Header("Joystick")]
    [SerializeField] UIVirtualJoystick moveJoystick;
    [SerializeField] UIVirtualJoystick rotateJoystick;

    [Header("Speed")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 3f;
    [SerializeField] float maxRotationSpeed = 10f;
    [SerializeField] float moveLerpSpeed = 5f;
    [SerializeField] float rotateLerpSpeed = 5f;



    private float currentSpeed;
    
    


    private Rigidbody rb;

    public bool isMovementEnabled = true;

    private float verticalInput;
    private float horizontalInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = moveSpeed;
    }



    private void Update()
    {
        if (isMovementEnabled)
        {
            Vector2 moveJoystickInput = moveJoystick.outputValue;
            float targetMoveSpeed = moveJoystickInput.y * maxMoveSpeed;
            targetMoveSpeed = Mathf.Clamp(targetMoveSpeed, -maxMoveSpeed, maxMoveSpeed);
            verticalInput = Mathf.Lerp(verticalInput, targetMoveSpeed, moveLerpSpeed * Time.deltaTime);

            Vector2 rotateJoystickInput = rotateJoystick.outputValue;
            float targetRotateSpeed = rotateJoystickInput.x * maxRotationSpeed;
            targetRotateSpeed = Mathf.Clamp(targetRotateSpeed, -maxRotationSpeed, maxRotationSpeed);
            horizontalInput = Mathf.Lerp(horizontalInput, targetRotateSpeed, rotateLerpSpeed * Time.deltaTime);
        }
    }
    

    private void FixedUpdate()
    {
        if (isMovementEnabled)
        {
            BoatMovement();
            BoatRotation();
        }
    }

    private void BoatMovement()
    {
        // �̵�
        Move(verticalInput);
    }

    public void Move(float verticalInput)
    {
        // �̵�
        float amount = currentSpeed * Time.fixedDeltaTime * verticalInput;
        rb.MovePosition(rb.position + transform.forward * amount);
    }

    private void BoatRotation()
    {
        // ȸ��
        Rotate(horizontalInput);
    }

    public void Rotate(float horizontalInput)
    {
        // ȸ��
        float amountRot = rotationSpeed * Time.fixedDeltaTime * horizontalInput;
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

    



    public void EnableMovement()
    {
        isMovementEnabled = true;
    }

    public void DisableMovement()
    {
        isMovementEnabled = false;
    }
}
