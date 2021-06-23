using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyShip : MonoBehaviour {
    
    [SerializeField] private Vector3 speed = Vector3.zero;
    [SerializeField] private int health = 10;
    private AudioSource aSource;
    private GameController gameController;

    private void Start() {
        speed = Random.Range(1, 3) * speed;
        aSource = GetComponent<AudioSource>();
        gameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {        
        transform.Translate(speed * Time.deltaTime, Space.World);

        if(transform.position.x < 400f) return;
        
        if(gameController != null) {
            gameController.enemies.Remove(gameObject);
        }
        
        if(aSource != null) aSource.Play();
        Destroy(gameObject);
    }

    public void Damage(int amount) {
        health -= amount;

        if(health > 0) return;
        
        if(gameController != null) {
            gameController.UpdateScore((15 * health) + speed.magnitude);
            gameController.enemies.Remove(gameObject);
        }
        
        aSource.Play();
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player")) return;
        SceneManager.LoadScene(0);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
