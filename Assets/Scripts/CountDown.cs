using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countDownText;
    BoatMove boatMove;
    float limitTime = 60; // ���� �ð�
    int sec; // �� ����
    int min; // �� ����
    public TMP_Text text_Time; // Ÿ�̸� UI
    private void Start()
    {
        boatMove = GameObject.Find("Boat").GetComponent<BoatMove>();

        StartCoroutine(StartCownDown());
    }
    void Update()
    {
        Invoke("TimeRule", 3.5f);

    }

    public void TimeRule()
    {
        limitTime -= Time.deltaTime; // ���� �ð� ����
        // ��ü �ð��� 60���� Ŭ ��
        if (limitTime >= 60f)
        {
            min = (int)limitTime / 60; // �� ���� ����
            sec = (int)limitTime % 60; // �� ���� ����
            text_Time.text = string.Format("{0}:{1}", min, sec); // UI
        }

        // ��ü �ð��� 60���� ���� ��
        if (limitTime < 60f)
        {
            text_Time.text = "00:" + (int)limitTime; // UI
            if (limitTime < 10f)
            {
                text_Time.text = "00:0" + (int)limitTime; // UI
            }
        }

        // ��ü �ð��� 0���� ���� ��
        if (limitTime < 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("duckBoat"); //
        }
    }

    
    private IEnumerator StartCownDown()
    {
        boatMove.DisableMovement();

        countDownText.text = "3";
        yield return new WaitForSeconds(1f);

        countDownText.text = "2";
        yield return new WaitForSeconds(1f);

        countDownText.text = "1";
        yield return new WaitForSeconds(1f);

        countDownText.text = "Go";

        yield return new WaitForSeconds(0.5f);
        countDownText.text = "";
        boatMove.EnableMovement();
        
    }
}
