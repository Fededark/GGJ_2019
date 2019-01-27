using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public Transform target;
    public float excursion = 1f;
    public float time = 1f;

    private Vector3 basePos;

    private void Awake()
    {
        basePos = target.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        target.localPosition = new Vector3(basePos.x, basePos.y + Mathf.Sin(Time.time * time) * excursion, basePos.z);
    }
}
