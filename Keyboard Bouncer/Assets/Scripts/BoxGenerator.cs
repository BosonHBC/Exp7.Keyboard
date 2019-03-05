using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxGenerator : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private Vector2 sizeOfBox;
    private Dictionary<int, KeyCode> keyboardMapping = new Dictionary<int, KeyCode>();
    private List<Transform> cubes = new List<Transform>();
    private bool[] isKeyDown;
    private int[] areaKeyDown;
    [SerializeField] private float fLeanSpeed;

    // Text releated
    [SerializeField] private Text scoreTime;
    private float fLiveTime;
    // Start is called before the first frame update
    void Start()
    {
        #region Mapping Keyboard
        for (int i = 0; i < 9; i++)
        {
            keyboardMapping.Add(i, (KeyCode)(48 + i + 1));
        }
        keyboardMapping.Add(9, KeyCode.Alpha0);
        keyboardMapping.Add(10, KeyCode.Q);
        keyboardMapping.Add(11, KeyCode.W);
        keyboardMapping.Add(12, KeyCode.E);
        keyboardMapping.Add(13, KeyCode.R);
        keyboardMapping.Add(14, KeyCode.T);
        keyboardMapping.Add(15, KeyCode.Y);
        keyboardMapping.Add(16, KeyCode.U);
        keyboardMapping.Add(17, KeyCode.I);
        keyboardMapping.Add(18, KeyCode.O);
        keyboardMapping.Add(19, KeyCode.P);
        keyboardMapping.Add(20, KeyCode.A);
        keyboardMapping.Add(21, KeyCode.S);
        keyboardMapping.Add(22, KeyCode.D);
        keyboardMapping.Add(23, KeyCode.F);
        keyboardMapping.Add(24, KeyCode.G);
        keyboardMapping.Add(25, KeyCode.H);
        keyboardMapping.Add(26, KeyCode.J);
        keyboardMapping.Add(27, KeyCode.K);
        keyboardMapping.Add(28, KeyCode.L);
        keyboardMapping.Add(29, KeyCode.Semicolon);
        keyboardMapping.Add(30, KeyCode.Z);
        keyboardMapping.Add(31, KeyCode.X);
        keyboardMapping.Add(32, KeyCode.C);
        keyboardMapping.Add(33, KeyCode.V);
        keyboardMapping.Add(34, KeyCode.B);
        keyboardMapping.Add(35, KeyCode.N);
        keyboardMapping.Add(36, KeyCode.M);
        keyboardMapping.Add(37, KeyCode.Comma);
        keyboardMapping.Add(38, KeyCode.Period);
        keyboardMapping.Add(39, KeyCode.Slash);
        #endregion
        isKeyDown = new bool[keyboardMapping.Count];
        areaKeyDown = new int[4];
        // Generate box
        for (int i = 0; i < keyboardMapping.Count; i++)
        {
            GameObject go = Instantiate(boxPrefab);
            Transform _goTr = go.transform;
            _goTr.SetParent(transform);
            cubes.Add(_goTr);
            _goTr.localPosition = new Vector3(i % sizeOfBox.x - 4.5f, 0, (int)(i / sizeOfBox.x) - 1.5f);
            go.name = "Cube_" + i.ToString();
            isKeyDown[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        fLiveTime += Time.deltaTime;
        scoreTime.text = (int)fLiveTime + "s";
        KeyboardEvent();
    }

    void KeyboardEvent()
    {

        for (int i = 0; i < keyboardMapping.Count; i++)
        {
            // Keydown
            if (Input.GetKeyDown(keyboardMapping[i]))
            {
                if (i < 20 && (i / 5) % 2 == 0)
                    areaKeyDown[0]++;
                else if (i < 20 && (i / 5) % 2 == 1)
                    areaKeyDown[1]++;
                else if (i >= 20 && (i / 5) % 2 == 0)
                    areaKeyDown[2]++;
                else if (i >= 20 && (i / 5) % 2 == 1)
                    areaKeyDown[3]++;
                //Debug.Log("No." + i + " cube goes down");
                isKeyDown[i] = true;
                StartCoroutine(ObectMoveTo(i, cubes[i].transform.localPosition, new Vector3(cubes[i].transform.localPosition.x, 1, cubes[i].transform.localPosition.z), 0.5f));

            }
            if (Input.GetKeyUp(keyboardMapping[i]))
            {
                if (i < 20 && (i / 5) % 2 == 0)
                    areaKeyDown[0]--;
                else if (i < 20 && (i / 5) % 2 == 1)
                    areaKeyDown[1]--;
                else if (i >= 20 && (i / 5) % 2 == 0)
                    areaKeyDown[2]--;
                else if (i >= 20 && (i / 5) % 2 == 1)
                    areaKeyDown[3]--;
                isKeyDown[i] = false;
                cubes[i].GetComponent<BoxBody>().bIsGoingUp = true;
                //Debug.Log("No." + i + " cube goes up");
                StartCoroutine(ObectMoveTo(i, cubes[i].transform.localPosition, new Vector3(cubes[i].transform.localPosition.x, 0, cubes[i].transform.localPosition.z), 0.1f, false));

            }
            if (Input.GetKey(keyboardMapping[i]))
                Leaner(i);

        }

    }

    IEnumerator ObectMoveTo(int _id, Vector3 _start, Vector3 _end, float _fadeTime = 0.5f, bool _isGoingDown = true)
    {
        float _timeStartFade = Time.time;
        float _timeSinceStart = Time.time - _timeStartFade;
        float _lerpPercentage = _timeSinceStart / _fadeTime;

        while (true)
        {
            _timeSinceStart = Time.time - _timeStartFade;
            _lerpPercentage = _timeSinceStart / _fadeTime;

            Vector3 currentValue = Vector3.Lerp(_start, _end, _lerpPercentage);
            cubes[_id].localPosition = currentValue;

            if (_isGoingDown)
            {
                if (!isKeyDown[_id]) break;
            }

            if (_lerpPercentage >= 1) break;


            yield return new WaitForEndOfFrame();
        }
        if (!_isGoingDown)
            cubes[_id].GetComponent<BoxBody>().bIsGoingUp = false;

    }

    IEnumerator nullIE()
    {
        yield return null;
    }

    void Leaner(int _id)
    {
        Transform rotator = transform.parent;

        if (_id < 20)
        {
            float _amount = areaKeyDown[0] + areaKeyDown[1];
            rotator.Rotate(rotator.right, -fLeanSpeed * _amount * Time.deltaTime);

        }
        else
        {
            float _amount = areaKeyDown[2] + areaKeyDown[3];
            rotator.Rotate(rotator.right, fLeanSpeed * _amount * Time.deltaTime);
        }




        if ((_id / 5) % 2 == 0)
        {
            float _amount = areaKeyDown[0] + areaKeyDown[2];
            rotator.Rotate(rotator.forward, -fLeanSpeed * _amount * Time.deltaTime);
        }
        else
        {
            float _amount = areaKeyDown[1] + areaKeyDown[3];
            rotator.Rotate(rotator.forward, fLeanSpeed * _amount * Time.deltaTime);

        }
    }

}
