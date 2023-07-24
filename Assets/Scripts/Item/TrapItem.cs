using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapItem : MonoBehaviour
{
    public float trapDuration = 3f; // ���� ���� �ð� (��)
    public float reduceAmount = 2f; // �̵� �ӵ� ���� ����
    public GameObject trapEffectPrefab; // ���� ȿ�� ������
    public Canvas canvas;


    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        BoatMove playerMovement = other.GetComponent<BoatMove>();
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;

            // �̵� �ӵ� ���� �ڷ�ƾ ����
            if (playerMovement != null)
            {
                playerMovement.ApplySpeedReduce(trapDuration, reduceAmount);
            }

            Destroy(gameObject);

            StartCoroutine(TrapEffectCoroutine(other.gameObject));
            
        }
    }

    private IEnumerator TrapEffectCoroutine(GameObject player)
    {
        GameObject trapEffectInstance = Instantiate(trapEffectPrefab, canvas.transform);
        Image trapImage = trapEffectInstance.GetComponent<Image>();
        trapImage.color = new Color(trapImage.color.r, trapImage.color.g, trapImage.color.b, 1f);

        CanvasGroup canvasGroup = trapEffectInstance.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = trapEffectInstance.AddComponent<CanvasGroup>();
        }

        yield return new WaitForSeconds(trapDuration);

        // �̹��� ������ �������
        float elapsedTime = 0f;
        float fadeDuration = 1f; // �̹����� ������� �ð� (��)

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            canvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;

            // ���� �����ӱ��� ���
            yield return new WaitForEndOfFrame();
        }

        Destroy(trapEffectInstance);
    }
}
