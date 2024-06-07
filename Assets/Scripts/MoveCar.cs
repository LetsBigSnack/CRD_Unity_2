using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCar : MonoBehaviour
{
    [SerializeField] private float originalVelocity = .5f;

    private float currentVelocity;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckForVelocity());
    }

    // Update is called once per frame
    void Update()
    {
        //use transform.Translate to move Car into its forward direction by currentVelocity
        transform.Translate(Vector3.forward * currentVelocity);
    }

    IEnumerator CheckForVelocity()
    {
        //DONT FORGET TO EXECUTE the Coroutine "CheckForVelocity" once
        while (true)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit) && hit.collider.CompareTag("CarRayHit"))
            {
                currentVelocity = originalVelocity / 10;
            }
            else
            {
                currentVelocity = originalVelocity;
            }
            //Raycast?
            //if player is hit currentVelocity is half of original velocity.
            //if not, currentVelocity is original velocity
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision other) /*or make OnTriggerEnter(Collider other) - depending on how you configure your colliders.. I went with physical collisions here, since my car should act as a physical object.. e.g. for moving the player if it hits it.*/
    {
        if (other.gameObject.CompareTag("TurnCar"))
        {
            //use transform.Rotate to rotate car by 180 degrees around its y Axis
            transform.Rotate(Vector3.up, 180);
        }
    }
   
}
