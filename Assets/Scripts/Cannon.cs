using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.VFX;

public class Cannon : MonoBehaviour
{
    [Header("Component & Object References")]
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;    

    [Header("Variables")]
    [SerializeField] public float bulletSpeed;
    [SerializeField] private float fireRate;

    [Header("References")]
    [SerializeField] private VisualEffect cannonSmoke;

    private AudioSource aSource;
    private float nextTimeToFire = 0f;
    public Vector3 targetRot = Vector3.zero;
    private readonly float step = 0.5f;

    // Start is called before the first frame update
    private void Start() {
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void FixedUpdate() {        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, targetRot.y, targetRot.z), step * 2f);

        if(!Input.GetButton("Fire1")) return;
        if(Time.time < nextTimeToFire) return;
        nextTimeToFire = Time.time + fireRate;             
        Invoke(nameof(FireBullet), 0.175f);
    }

    private void FireBullet() {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);        
        aSource.Play();
        if(cannonSmoke) cannonSmoke.Play();
    }
}
