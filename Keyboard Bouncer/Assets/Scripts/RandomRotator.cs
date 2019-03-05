using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    Vector3 rdEulerAngle;

    // Start is called before the first frame update
    void Start()
    {
        RandomRotation();    
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnGUI()
    {

    }
    void RandomRotation()
    {
        rdEulerAngle = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        StartCoroutine(EulerRotation(transform.eulerAngles, rdEulerAngle, 0.2f));
    }

    IEnumerator EulerRotation(Vector3 _start, Vector3 _end, float _fadeTime = 0.5f)
    {
        float _timeStartFade = Time.time;
        float _timeSinceStart = Time.time - _timeStartFade;
        float _lerpPercentage = _timeSinceStart / _fadeTime;

        while (true)
        {
            _timeSinceStart = Time.time - _timeStartFade;
            _lerpPercentage = _timeSinceStart / _fadeTime;

            Vector3 currentValue = Vector3.Lerp(_start, _end, _lerpPercentage);
            transform.eulerAngles = currentValue;
            if (_lerpPercentage >= 1) break;


            yield return new WaitForEndOfFrame();
        }

    }
}
