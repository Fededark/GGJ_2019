using UnityEngine;

public class Carpenter : MonoBehaviour
{
    public HomeBuilder home;
    public GameObject playerPrefab;

    void Start()
    {
        home.Build(transform);
        if (playerPrefab != null)
        {
            GameObject player = Instantiate(playerPrefab);
            player.GetComponent<PlayerDoorMovement>().AssignTo(home.info.hall);
            player.transform.position = home.info.SpawnPoint;
        }
    }

}
