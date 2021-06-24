using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraAim : MonoBehaviour
{
    [SerializeField] private Transform turretTransform;
    [FormerlySerializedAs("vCamera")] [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform target;
    private List<Cannon> cannons = new List<Cannon>();
    private float bulletSpeed;

    private void Start() {
        cannons = FindObjectsOfType<Cannon>().ToList();
        bulletSpeed = cannons[0].bulletSpeed + 25f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out var hit, 3000f);
        
        foreach(var cannon in cannons) {
            var angle = Mathf.Asin(Physics.gravity.y * (hit.distance - (transform.position - cannon.transform.position).magnitude) / (bulletSpeed * bulletSpeed)) / 2f;        
            angle = Mathf.Rad2Deg * angle;
            //angle = Mathf.Clamp(angle, -15f, 15f);
            if(float.IsNaN(angle)) angle = 0f;
            //var rotY = Mathf.Lerp(-105, -68, Mathf.InverseLerp(-15, 22, mainCamera.transform.rotation.eulerAngles.y));
            var targetRot = new Vector3(0f, mainCamera.transform.rotation.eulerAngles.y - 90f + Random.Range(-0.5f, 0.51f), -angle);
            cannon.targetRot = targetRot;
        }
        
        target.position = new Vector3(hit.point.x, target.position.y, hit.point.z);
        target.rotation = Quaternion.Euler(90f, 0f, -mainCamera.transform.rotation.eulerAngles.y);
    }
}
