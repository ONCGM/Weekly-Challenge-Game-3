using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraAim : MonoBehaviour
{
    [SerializeField] private Transform turretTransform;
    [SerializeField] private CinemachineVirtualCamera vCamera;
    private CinemachinePOV povCamera;
    private Turret turret;

    void Start()
    {
        turret = turretTransform.GetComponent<Turret>();
        povCamera = vCamera.GetCinemachineComponent<CinemachinePOV>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, 3000f);        
        float angle = (Mathf.Asin((Physics.gravity.y * (hit.distance - (transform.position - turretTransform.position).magnitude)) / (532f * 532f))) / 2f;        
        angle = Mathf.Rad2Deg * angle;
        if(angle != angle) {
            angle = 2.5f;
        }
        angle = Mathf.Clamp(angle, -15f, 15f);
        turret.targetRot = new Vector3(angle, povCamera.m_HorizontalAxis.Value, turret.targetRot.z);        
    }
}
