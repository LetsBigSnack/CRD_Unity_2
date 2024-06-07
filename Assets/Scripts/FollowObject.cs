using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToFollow;
    // Start is called before the first frame update

    private Transform otherObjectTransform;
    private Transform ownTransform;
    private Vector3 offsetOfFollowObjectAndOwnObject;
    private float _fixedYOffset;
    private float _distance;
    void Start()
    {
        otherObjectTransform = objectToFollow.GetComponent<Transform>();
        ownTransform = gameObject.GetComponent<Transform>();
        _fixedYOffset = ownTransform.position.y - otherObjectTransform.position.y;
        _distance = Vector3.Distance(ownTransform.position, otherObjectTransform.position);
        SetOffset();
    }

    public void SetOffset()
    {
        offsetOfFollowObjectAndOwnObject = ownTransform.position - otherObjectTransform.position;
        offsetOfFollowObjectAndOwnObject.Normalize();
        offsetOfFollowObjectAndOwnObject *= _distance;
        offsetOfFollowObjectAndOwnObject.y = _fixedYOffset;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = otherObjectTransform.position + offsetOfFollowObjectAndOwnObject;
    }
}
