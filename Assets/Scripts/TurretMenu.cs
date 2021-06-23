using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMenu : MonoBehaviour
{
    [Header("Component & Object References")]
    [SerializeField] private Transform cannonPivot;     

    [Header("Variables")]    
    [SerializeField] private float cannonRotationLimit;    

    private AudioSource aSource;    
    private Vector3 targetRot;
    private float step = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xRotation = Mathf.InverseLerp(0f, Screen.height, Input.mousePosition.y);
        float yRotation = Mathf.InverseLerp(0f, Screen.width, Input.mousePosition.x);
        float newYRot = Mathf.Lerp(-10, 65f, yRotation);
        targetRot = new Vector3(0f, newYRot, 0f);
        transform.rotation = Quaternion.Euler(targetRot);
        cannonPivot.rotation = Quaternion.Euler(xRotation * -cannonRotationLimit, newYRot, cannonPivot.rotation.z);
    }
}
