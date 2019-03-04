using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    [SerializeField] Transform _LookAt;
    [SerializeField] Transform _Ball;
    // Start is called before the first frame update

    Vector3 defaultDifference;
    void Start()
    {
        defaultDifference = _Ball.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _lookAtPos = (_LookAt.position + _Ball.position)/2;
        transform.LookAt(_lookAtPos);

        Debug.DrawLine(transform.position, _lookAtPos, Color.red);

        transform.position = new Vector3(transform.position.x,(_Ball.position - defaultDifference).y,transform.position.z);

    }
}
