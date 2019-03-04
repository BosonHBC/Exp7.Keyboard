using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBody : MonoBehaviour
{
    public bool bIsGoingUp;
    [SerializeField] float fBounceForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb;
        if (bIsGoingUp)
        {
            Debug.Log("Adding Force");
            rb = collision.collider.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(0, fBounceForce, 0), ForceMode.Impulse);
        }
    }
}
