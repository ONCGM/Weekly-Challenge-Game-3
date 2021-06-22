using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private string enemyTag;
    [SerializeField] private string seaTag;

    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rb.velocity), 1f);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag(enemyTag)) {
            other.GetComponent<EnemyBoat>().Damage(1);            
            Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
