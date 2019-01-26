using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpenter : MonoBehaviour
{
    public HomeBuilder home;

    // Start is called before the first frame update
    void Start()
    {
        home.Build(transform);
    }

}
