using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {
    [Header("Component & objects references")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform nearShipLane;
    [SerializeField] private Transform midShipLane;
    [SerializeField] private Transform farShipLane;

    private int playerScore;
    private Transform lastSpawn;
    public List<GameObject> enemies = new List<GameObject>();

    private void Start() {
        playerScore = 0;
        scoreText.text = "0";
        StartCoroutine(nameof(Timer));
    }

    private IEnumerator Timer() {
        yield return new WaitForSecondsRealtime(1f);                
        if(enemies.Count < 3) {
            var rng = Random.value;
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
        
        StartCoroutine(nameof(Timer));
    }

    public void UpdateScore(float amount) {
        playerScore += Mathf.FloorToInt(amount);
        scoreText.text = $"Score: {playerScore.ToString()}";        
    }
}
