using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

internal enum MovementType
{
    TransformBased,
    PhysicsBased
}

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _velocity = 1;
    [SerializeField]
    private float _jumpForce = 2;
    [SerializeField]
    private ForceMode selectedForceMode;
    [SerializeField]
    private MovementType movementType;
    
    
    private Vector3 movementDirection3d;

    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    { 
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       PerformMovement();
    }

    void PerformMovement()
    {

        if (movementDirection3d == Vector3.zero)
        {
            return;
        }
        
        transform.forward = Camera.main.transform.forward;
        Quaternion cameraAlignedRotation = transform.rotation;
        transform.forward = movementDirection3d;
        transform.rotation *= cameraAlignedRotation;
        
        if (movementType == MovementType.TransformBased)
        {
            //gameObject.transform.position += movementDirection3d * _velocity;
            gameObject.transform.Translate(new Vector3(0,0,-0.1f) * _velocity);
        }
        else if (movementType == MovementType.PhysicsBased)
        {
            _rigidbody.AddForce(movementDirection3d, selectedForceMode);
        }
    }
    
    void OnMovement(InputValue inputValue)
    {
        Vector2 movementDirection = inputValue.Get<Vector2>();
        movementDirection3d = new Vector3(movementDirection.x, 0, movementDirection.y);
    }

    void OnJump(InputValue inputValue)
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce, selectedForceMode);
        //Vector3.up is shorthand for new Vector3(0,0,1)
    }

}
