using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    
    [SerializeField] private GameObject playerObject;
    [SerializeField] private float closestDistanceToPlayer;

    private FollowObject _followObject;
    
    private float maximumDistanceFromPlayer;
    private Vector3 previousPlayerPostion;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _followObject = GetComponent<FollowObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCameraRotation(InputValue inputValue)
    {
        Vector2 inputVector = inputValue.Get<Vector2>();
        transform.RotateAround(playerObject.transform.position, new Vector3(0,1,0),  inputVector.x);
        _followObject.SetOffset();
    }
}
