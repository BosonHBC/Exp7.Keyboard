using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheBall : MonoBehaviour
{

    Rigidbody rb;

   [SerializeField] float verticalSpeed;
    Vector3 currPos;
    Vector3 prevPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currPos = transform.position;
        prevPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        prevPos = currPos;
        currPos = transform.position;
        verticalSpeed = (currPos - prevPos).y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
            SceneManager.LoadScene(0);
    }
}
