using UnityEngine;

public class Campfire : MonoBehaviour
{
    [SerializeField] Transform checkpoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if(playerController)
            {
                Debug.Log("set checkpoint");
                playerController.lastCheckpoint = checkpoint.position;
            }
        }
    }
}
