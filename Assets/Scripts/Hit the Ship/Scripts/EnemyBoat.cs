using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBoat : MonoBehaviour
{
    public float xSpeed { get; private set; }
    public int scale { get; private set; }
    [SerializeField] private float speed = 25f;
    private AudioSource aSource;
    private GameController gameController;
    private GameControllerEndless gameControllerEndless;
    private int health = 0;

    private void Start() {
        xSpeed = Random.Range(1, 3) * speed;
        aSource = GetComponent<AudioSource>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gameControllerEndless = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerEndless>();
        scale = Random.Range(1, 3);
        health = scale;
        transform.localScale = transform.localScale * scale;
    }

    private void Update()
    {        
        transform.Translate(new Vector3(0f, 0f, -xSpeed) * Time.deltaTime, Space.World);
        if(transform.position.x < -800f) {
            if(gameController != null) {
                gameController.enemies.Remove(gameObject);
            }
            if(gameControllerEndless != null) {
                gameControllerEndless.enemies.Remove(gameObject);
            }
            aSource.Play();
            Destroy(gameObject);
        }
    }

    public void Damage(int amount) {
        health = health - amount;
        if(health <= 0) {
            if(gameController != null) {
                gameController.UpdateScore((50 * scale) + xSpeed);
                gameController.enemies.Remove(gameObject);
            }
            if(gameControllerEndless != null) {
                gameControllerEndless.UpdateScore((50 * scale) + xSpeed);
                gameControllerEndless.enemies.Remove(gameObject);
            }
            aSource.Play();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            SceneManager.LoadScene(0);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
