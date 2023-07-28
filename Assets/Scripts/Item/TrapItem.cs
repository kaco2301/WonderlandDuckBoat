using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapItem : MonoBehaviour
{
    public float trapDuration = 3f; // ���� ���� �ð� (��)
    public float reduceAmount = 2f; // �̵� �ӵ� ���� ����
    public GameObject trapEffectPrefab;  // ���� ȿ�� ������

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
            
            StartCoroutine(TrapEffectCoroutine(other.gameObject));

            Destroy(gameObject);
        }
    }


    private IEnumerator TrapEffectCoroutine(GameObject player)
    {
        Camera playerCamera = Camera.main;
        GameObject trapEffectInstance = Instantiate(trapEffectPrefab);
        trapEffectInstance.transform.SetParent(playerCamera.transform);
        trapEffectInstance.transform.localPosition = Vector3.forward;

        ParticleSystem[] allParticleSystems = trapEffectInstance.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particleSystem in allParticleSystems)
        {
            var main = particleSystem.main;
            main.loop = false;
            particleSystem.Play();
        }
        Debug.Log("PLAY");

        Debug.Log($"trapDuration: {trapDuration}");

        yield return new WaitForSeconds(trapDuration);

        foreach (ParticleSystem particleSystem in allParticleSystems)
        {
            particleSystem.Stop();
        }
        Debug.Log("STOP");

        float maxEffectDuration = 0.0f;
        foreach (ParticleSystem particleSystem in allParticleSystems)
        {
            if (particleSystem.main.duration > maxEffectDuration)
            {
                maxEffectDuration = particleSystem.main.duration;
            }
        }
        Debug.Log($"maxEffectDuration: {maxEffectDuration}");
        yield return new WaitForSeconds(maxEffectDuration);

        Destroy(trapEffectInstance);
        Debug.Log("DESTROY");
    }
}