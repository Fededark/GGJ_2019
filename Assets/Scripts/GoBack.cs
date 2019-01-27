using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBack : MonoBehaviour
{
    public string scene; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("PlayerAction"))
        {
            SceneManager.LoadScene(scene);
        }
    }
}
