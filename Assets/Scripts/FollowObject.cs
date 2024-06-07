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
    void Start()
    {
        otherObjectTransform = objectToFollow.GetComponent<Transform>();
        ownTransform = gameObject.GetComponent<Transform>();
        SetOffset();
    }

    public void SetOffset()
    {
        offsetOfFollowObjectAndOwnObject = ownTransform.position - otherObjectTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = otherObjectTransform.position + offsetOfFollowObjectAndOwnObject;
    }
}
