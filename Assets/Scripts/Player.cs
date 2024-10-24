using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject aer;
    [Header("Reference")]
    //[SerializeField] GameManager manager;
    [SerializeField] private Camera cam;//Camera object
    private Rigidbody rb;//This object's rigidbody component
    private Animator animator;
    [SerializeField] LayerMask ObjectLayer;//Object search layer
    [SerializeField] Text TextBox;

    [Header("Navigation")]
    NavMeshAgent Agent;
    [SerializeField] Transform Aim;
    private Vector3 LookPoint;
    private Object Selected = null;
    private Vector3 MoveTo;
    private bool Moving = false;

    [Header("Grab")]
    [SerializeField] private float GrabRayRadius = 0.3f;//Radius of grab SphereCast
    [SerializeField] private float Reach = 300.0f;
    private string LookText = "";

    void Start()
    {
        InputManager.Init(this);//Setup controls
        rb = GetComponent<Rigidbody>();//Setup component reference
        animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {

        if (Moving && Agent.remainingDistance <= Agent.stoppingDistance) {
            Moving = false;
            Selected.Select();
            Selected = null;
            Agent.enabled = false;
        }
        
        if (!Moving) {
            //float LookY = Mathf.MoveTowardsAngle(transform.eulerAngles.y, Aim.eulerAngles.y, 10);

            //Aim.LookAt(aer.transform.position);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Aim.rotation, 30);
            //if (transform.rotation.y < Aim.transform.rotation.y) transform.Rotate(0, 10, 0);
            //else if (transform.rotation.y > Aim.transform.rotation.y) transform.Rotate(0, -10, 0);

            //transform.LookAt(aer.transform.position);
            transform.LookAt(new Vector3(LookPoint.x, transform.position.y, LookPoint.z));
        }

    }

    void Update() 
    {
        if (!Moving) Selected = GetLook();
        if (Selected != null) LookText = Selected.DisplayText;
        else LookText = "";
        TextBox.text = LookText;



    }

    public void Grab()//Attempt object interaction
    {
        if (Selected != null && !Moving) {
            MoveTo = Selected.transform.position;//LookObject.Select();//If pointing at an object, select the object
            Agent.enabled = true;
            Agent.SetDestination(MoveTo);
            Moving = true;
        }
    }

    Object GetLook() {
        //if (Physics.SphereCast(cam.transform.position, GrabRayRadius, cam.transform.forward, out RaycastHit hit, Reach, ObjectLayer))
        if (Physics.Raycast(cam.ScreenPointToRay(Mouse.current.position.value), out RaycastHit hit, Reach))
        {
            LookPoint = hit.point;
            //Aim.LookAt(new Vector3(hit.point.x, Aim.transform.position.y, hit.point.z));
            if (hit.collider.gameObject.GetComponent<Object>() != null) return hit.collider.gameObject.GetComponent<Object>();
            else return null;
        }
        else return null;
    }

}
