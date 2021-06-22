using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControllerEndless : MonoBehaviour
{
    public int playerScore { get; private set; }
    public int timeLeft { get; private set; } = 90;

    [Header("Component & objects references")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform nearShipLane;
    [SerializeField] private Transform midShipLane;
    [SerializeField] private Transform farShipLane;

    private Transform lastSpawn;
    public List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        playerScore = 0;
        scoreText.text = "0";
        timeText.text = "∞";
        StartCoroutine("Timer");
    }

    private IEnumerator Timer() {
        yield return new WaitForSecondsRealtime(1f);                
        if(enemies.Count < 3) {
            float rng = Random.value;
            if(rng > 0.7f && lastSpawn != nearShipLane) {
                enemies.Add(Instantiate(enemyPrefab, nearShipLane.position, nearShipLane.rotation));
                lastSpawn = nearShipLane;
            } else if(rng > 0.3f && lastSpawn != midShipLane) {
                enemies.Add(Instantiate(enemyPrefab, midShipLane.position, midShipLane.rotation));
                lastSpawn = midShipLane;
            } else if(lastSpawn != farShipLane) {
                enemies.Add(Instantiate(enemyPrefab, farShipLane.position, farShipLane.rotation));
                lastSpawn = farShipLane;
            }
        }        
        StartCoroutine("Timer");
    }

    public void UpdateScore(float amount) {
        playerScore = playerScore + Mathf.FloorToInt(amount);
        scoreText.text = playerScore.ToString();        
    }
}
