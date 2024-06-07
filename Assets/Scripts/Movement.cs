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
    private ForceMode selectedForceMode;
    [SerializeField]
    private MovementType movementType;
    
    private Vector3 movementDirection3d;

    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    
    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce = 2;
    [SerializeField] private float jumpingMaxHeight = 5.5f;
    [SerializeField] private float fallFactor = 0.9f;

    private bool _isGrounded = false;
    private bool _isJumping = false;
    
    void Start()
    { 
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        StartCoroutine(CheckForGround());
        
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
        if (_isJumping || !_isGrounded)
        {
            return;
        }
        StartCoroutine(JumpControlFlow());
        
    }

    private IEnumerator JumpControlFlow()
    {
        _isJumping = true;
        float jumpHeight = transform.position.y + jumpingMaxHeight;
        Debug.Log("JUMP button pressed");
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Force);
        while (transform.position.y < jumpHeight)
        {
            Debug.Log("jump ongoing"+ Time.time);
            yield return null;
        }
        _rigidbody.AddForce(Vector3.up * _jumpForce * -1 * fallFactor, ForceMode.Force);
        Debug.Log("jump done");
        _isJumping = false;
    }
    
    private IEnumerator CheckForGround()
    {
        RaycastHit hit;
        float prevY;
        float currentY = transform.position.y;

        while (true)
        {
            bool raycastSuccess = Physics.Raycast(transform.position, transform.up * -1, out hit);
            if (raycastSuccess && hit.collider.gameObject.CompareTag("Ground") && hit.distance <= 0.50001f)
            {
                _isGrounded = true;
                StopCoroutine(JumpControlFlow());
                _isJumping = false;
            }
            else
            {
                _isGrounded = false;
            }
            yield return null;
        }
    }
}
