using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followBoat : MonoBehaviour
{/*
    public Vector3 position = new Vector3(0, 3.6f, -7.8f);
    public Vector3 rotation = new Vector3(14, 0, 0);
    public float fov = 30f;

    float boatSpeed = 10f;
    float turnSpeed = 10f;

    Transform boat; // ���� ��� = ������
    Transform cam; // ī�޶�
    Transform pivot; // ī�޶� �̵�, ȸ���� */

    // Start is called before the first frame update
    void Start()
    {/*
        boat = GameObject.Find("Boat").transform; // Ÿ�� ����
        inItCamera(); // ī�޶� �ʱ�ȭ */
    }
    /*
    void inItCamera()
    {
        // ī�޶� ����
        cam = Camera.main.transform;
        cam.GetComponent<Camera>().fieldOfView = fov;

        // Pivot �����
        pivot = new GameObject("Pivot").transform;
        pivot.position = boat.position;

        // ī�޶� Pivot�� Child�� ����
        cam.parent = pivot;
        cam.localPosition = position;
        cam.localEulerAngles = rotation;
    }

    void LateUpdate()
    {
        // ���� ����
        // ������
        Vector3 pos = boat.position;
        Quaternion rot = boat.rotation;

        pivot.position = Vector3.Lerp(pivot.position, pos, boatSpeed * Time.deltaTime); // �̵�
        pivot.rotation = Quaternion.Lerp(pivot.rotation, rot, turnSpeed * Time.deltaTime); // ȸ��
    } */
}
