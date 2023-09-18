using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform Player { get; private set; }
    [SerializeField] private string playerTag = "Player";
    private HealthSystem playerHealthSystem;

    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private Slider hpGaugeSlider;
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private int currentWaveIndex = 0;
    private int currentSpawnCount = 0;
    private int waveSpawnCount = 0;
    private int waveSpawnPosCount = 0;

    public float spawnInterval = 0.5f;
    public List<GameObject> enemyPrefabs = new List<GameObject>();

    [SerializeField] private Transform spawnPositionsRoot;
    private List<Transform> spawnPositions = new List<Transform>();
           
    private void Awake()
    {
        instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag).transform; // UPdate에서는 사용하지말자.

        playerHealthSystem = Player.GetComponent<HealthSystem>();
        playerHealthSystem.OnDamage += UpdateHealthUI;
        playerHealthSystem.OnHeal += UpdateHealthUI;
        playerHealthSystem.OnDeath += GameOver;

        gameOverUI.SetActive(false);

        for (int i = 0; i <  spawnPositionsRoot.childCount; i++)
        {
            spawnPositions.Add(spawnPositionsRoot.GetChild(i));
        }
    }

    private void Start()
    {
        StartCoroutine("StartNextWave"); // 코루틴 실행
    }

    IEnumerator StartNextWave() // 코루틴 비 동기적으로 실행. 코드를 일시적으로 정지하거나 재생하는 기능 가능. 
    {
        // 안전하게 비동기 코드 제작 가능 
        while (true) // 무한루프, = Update
        {
            if(currentSpawnCount == 0) // 첫 시작 or 다 잡았거나
            {
                UpdateWaveUI();
                yield return new WaitForSeconds(2f); // 2초 뒤 다음 코드 동작

                if(currentWaveIndex % 10 == 0) // 스테이지가 10의 배수라면, 스폰 포지션 늘림
                {
                    waveSpawnPosCount = waveSpawnPosCount + 1 > spawnPositions.Count ? waveSpawnPosCount : waveSpawnPosCount + 1;
                    waveSpawnCount = 0;
                }

                if(currentWaveIndex % 5 == 0)
                {

                }

                if(currentWaveIndex % 3 == 0) // 3의 배수라면, 스폰을 늘림
                {
                    waveSpawnCount += 1;
                }

                for(int i = 0; i < waveSpawnPosCount; i++)
                {
                    int posIdx = Random.Range(0, spawnPositions.Count);
                    for(int j = 0; j < waveSpawnCount; j++)
                    {
                        int prefabIdx = Random.Range(0, enemyPrefabs.Count);
                        GameObject enemy = Instantiate(enemyPrefabs[prefabIdx], spawnPositions[posIdx].position, Quaternion.identity);
                        enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;

                        currentSpawnCount++;
                        yield return new WaitForSeconds(spawnInterval);
                    }
                }

                currentWaveIndex++;
            }
            yield return null;
        }

    }

    private void OnEnemyDeath()
    {
        currentSpawnCount--;
    }

    private void UpdateHealthUI()
    {
        hpGaugeSlider.value = playerHealthSystem.CurrentHealth / playerHealthSystem.MaxHealth;
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true);
        StopAllCoroutines(); // 코루틴 멈춰라
    }

    private void UpdateWaveUI()
    {
        waveText.text = (currentWaveIndex + 1).ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
