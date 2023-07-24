using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //������ �������ð��ȿ� �������� ��ƾ��ϴ� ��Ģ.
    //�� ������ ���� �ð��� ���� ������ ������ ����
    //�� ������ ���ϰ� ���ѽð��� ���� ��� ���ӿ��� ��, ���� ���� ��� ������ ���� Ŭ���� ��
    //�������� ������, �̵� �ӵ��� ����/ ��ƾ��ϴ� ������/ �þ߸� ������ ����������

    public static GameManager Instance { get; private set;}

    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    public GameObject joyStick;

    public TextMeshProUGUI leftTime;
    public TextMeshProUGUI remainingItemsText;

    private int totalScore = 0;
    private int CollectedItems = 0;

    CountDown countDown;


    // Start is called before the first frame update
    private void Awake()
    {
        CountDown countDownScript = FindObjectOfType<CountDown>();
        countDown = GetComponent<CountDown>();

        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UpdateRemainingItemsText();
    }

    public void IncreaseScore(int scoreToAdd)
    {
        totalScore += scoreToAdd;
        CollectedItems++;

        UpdateRemainingItemsText();

        if (CollectedItems == 10)
        {
            GameClear();
        }
    }

    public void TimeOver()
    {
        if(CollectedItems!=10)
        { }
    }

    private void GameClear()
    {
        joyStick.SetActive(false);
        gameClearPanel.SetActive(true);
        countDown.DeactiveText();
        remainingItemsText.text = "";
        Time.timeScale = 0f;
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        countDown.DeactiveText();
        Time.timeScale = 0f; // ���� �Ͻ�����
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // ���� �簳
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateRemainingItemsText()
    {
        remainingItemsText.text = CollectedItems.ToString() + "/10";
    }
}
