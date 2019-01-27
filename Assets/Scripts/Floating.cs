using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public Transform target;
    public float excursion = 1f;
    public float time = 1f;

    // Update is called once per frame
    void Update()
    {
        target.localPosition = new Vector3(0f, Mathf.Sin(Time.time * time) * excursion, 0f);
    }
}
