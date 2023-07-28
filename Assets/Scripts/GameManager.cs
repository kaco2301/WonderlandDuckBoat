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

    public Camera mainCamera;
    public Camera uiCamera;

    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    public GameObject joyStick;
    [SerializeField] GameObject welcomeCanvas;
    

    public TextMeshProUGUI leftTime;
    public TextMeshProUGUI remainingItemsText;
    public TextMeshProUGUI CollctedItemsText;
    

    private int totalScore = 0;
    private int CollectedItems = 0;
    private int minute = 60;

    CountDown countDown;


    // Start is called before the first frame update
    private void Awake()
    {
        
        CountDown countDownScript = FindObjectOfType<CountDown>();
        countDown = GetComponent<CountDown>();

        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false);
        joyStick.SetActive(false);
        welcomeCanvas.SetActive(true);
        Time.timeScale = 1;

        mainCamera.enabled = false;
        uiCamera.enabled = true;

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
    private void PastTime()
    {
        int a = minute - countDown.limitTime;
        int minutes = a / 60;
        int seconds = a % 60;

        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);

        leftTime.text = timeText;
    }

    private void Start()
    {
        
    }

    public void GameStartBtn()
    {
        mainCamera.enabled = true;
        uiCamera.enabled = false;

        Time.timeScale = 1;
        welcomeCanvas.SetActive(false);
        joyStick.SetActive(true);
        countDown.StartCounting();
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
        if(CollectedItems!=10 && countDown.limitTime <= 0)
        {
            GameOver();
        }
    }

    private void GameClear()
    {
        
        joyStick.SetActive(false);
        gameClearPanel.SetActive(true);
        countDown.DeactiveText();
        remainingItemsText.text = "";
        PastTime();

        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        countDown.DeactiveText();
        CollctedItemsText.text = CollectedItems.ToString() + "/10";

        Time.timeScale = 0f; // ���� �Ͻ�����
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // ���� �簳
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("OriginScene");
    }

    private void UpdateRemainingItemsText()
    {
        remainingItemsText.text = CollectedItems.ToString() + "/10";
    }
}
