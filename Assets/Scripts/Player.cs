using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Reference")]
    [SerializeField] private Camera cam;//Camera object
    private Rigidbody rb;//This object's rigidbody component
    [SerializeField] LayerMask ObjectLayer;//Object search layer
    [SerializeField] TextMeshProUGUI TextBox;

    [Header("Move")]
    [SerializeField] private float Speed = 5.0f;//Speed increase per secnod
    [SerializeField] private float MaxSpeed = 3.0f;//Highest speed
    [SerializeField] private float SlowSpeed;//Deceleration speed
    private float CurrentSpeed = 0.0f;//Current speed
    private Vector3 Dirs = Vector2.zero;//Movement direction

    [Header("Look")]
    [SerializeField] private Vector2 Sensitivity = Vector2.one;//Look speed multiplier
    [SerializeField] private float MinAngleY = -50.0f;//Highest look angle
    [SerializeField] private float MaxAngleY = 70.0f;//Lowest look agle
    private Vector2 Rotation = Vector2.zero;//Look direction
    private Object LookObject = null;
    private string LookText = "";

    [Header("Grab")]
    [SerializeField] private float GrabRayRadius = 0.3f;//Radius of grab SphereCast
    [SerializeField] private float Reach = 3.0f;

    void Start()
    {
        InputManager.Init(this);//Setup controls
        rb = GetComponent<Rigidbody>();//Setup component reference
    }

    void FixedUpdate()
    {
        if (Dirs.magnitude == 0) CurrentSpeed -= Speed * 1.5f * Time.deltaTime;//Slow down player if no inputs
        else CurrentSpeed = CurrentSpeed += Speed * Time.deltaTime;//Accelerate player if inputs

        CurrentSpeed = Mathf.Clamp(CurrentSpeed, 0, MaxSpeed);//Clamp speed

        rb.velocity = cam.transform.rotation * Dirs * CurrentSpeed;//Set velocity

    }

    void Update() 
    {
        LookObject = GetLook();//Display LookObject text
        if (LookObject != null) LookText = LookObject.DisplayText;
        else LookText = "";
        TextBox.text = LookText;
    }
    public void Move(Vector3 new_dirs)//Set velocity
    {
        Dirs = new_dirs;//Set move direction to inputed direction
    }

    public void Look(Vector2 dir)//Set look direction
    {
        Rotation += dir * Sensitivity * Time.deltaTime;//Adjust look direction
        Rotation.y = Mathf.Clamp(Rotation.y, MinAngleY, MaxAngleY);

        Quaternion LookX = Quaternion.AngleAxis(Rotation.x, Vector3.up);//Get horizontal rotation
        Quaternion LookY = Quaternion.AngleAxis(-Rotation.y, Vector3.right);//Get vertical rotatiom

        cam.transform.localRotation = LookY;//Set new look direction

        transform.localRotation = LookX;
    }

    public void Grab()//Attempt object interaction
    {
        if (LookObject != null) LookObject.Select();//If pointing at an object, select the object
    }

    Object GetLook() {
        if (Physics.SphereCast(cam.transform.position, GrabRayRadius, cam.transform.forward, out RaycastHit hit, Reach, ObjectLayer))
        {
            return hit.collider.gameObject.GetComponent<Object>();
        }
        else return null;
    }

}
