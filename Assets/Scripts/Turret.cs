using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Component & Object References")]
    [SerializeField] private Transform cannonPivot;
    [SerializeField] private Transform leftBulletSpawnPoint;
    [SerializeField] private Transform middleBulletSpawnPoint;
    [SerializeField] private Transform rightBulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;    

    [Header("Variables")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireRate;    

    [Header("References")]
    [SerializeField] private ParticleSystem cannonSmokeLeft;
    [SerializeField] private ParticleSystem cannonSmokeLeft2;
    [SerializeField] private ParticleSystem cannonSmokeMiddle;
    [SerializeField] private ParticleSystem cannonSmokeMiddle2;
    [SerializeField] private ParticleSystem cannonSmokeRight;
    [SerializeField] private ParticleSystem cannonSmokeRight2;

    [Header("References")]
    [SerializeField] private CannonFiringTween leftCannonTween;    
    [SerializeField] private CannonFiringTween middleCannonTween;    
    [SerializeField] private CannonFiringTween rightCannonTween;        

    private AudioSource aSource;
    private float nextTimeToFire = 0f;
    public Vector3 targetRot { get; set; } = Vector3.zero;
    private float step = 0.5f;

    // Start is called before the first frame update
    private void Start() {
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void FixedUpdate() {        
        if(transform.rotation.y != targetRot.y ) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.x, targetRot.y, transform.rotation.z), step * 2f);            
        }

        if(cannonPivot.rotation.x != targetRot.x) {                        
            cannonPivot.rotation = Quaternion.RotateTowards(cannonPivot.rotation, Quaternion.Euler(targetRot.x, targetRot.y, cannonPivot.rotation.z), step);
        }

        if(Input.GetButton("Fire1")) {
            if(Time.time > nextTimeToFire) {
                nextTimeToFire = Time.time + fireRate;                
                Invoke("FireLeftBullet", 0.175f);
                Invoke("FireMiddleBullet", 0.175f);
                Invoke("FireRightBullet", 0.175f);
            }            
        }
    }

    private void FireLeftBullet() {
        GameObject bullet = Instantiate(bulletPrefab, leftBulletSpawnPoint.position, leftBulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);        
        aSource.Play();
        leftCannonTween.StartCoroutine("PlayTween");
        cannonSmokeLeft.Play();
        cannonSmokeLeft2.Play();
        Destroy(bullet, 15f);
    }

    private void FireMiddleBullet() {
        GameObject bullet = Instantiate(bulletPrefab, middleBulletSpawnPoint.position, middleBulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);        
        aSource.Play();
        middleCannonTween.StartCoroutine("PlayTween");
        cannonSmokeMiddle.Play();
        cannonSmokeMiddle2.Play();
        Destroy(bullet, 15f);
    }

    private void FireRightBullet() {
        GameObject bullet = Instantiate(bulletPrefab, rightBulletSpawnPoint.position, rightBulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);        
        aSource.Play();
        rightCannonTween.StartCoroutine("PlayTween");
        cannonSmokeRight.Play();
        cannonSmokeRight2.Play();
        Destroy(bullet, 15f);
    }
}
