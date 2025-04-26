using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if(playerController)
            {
                playerController.GoToCheckpoint();
            }
        }

        if (other.tag == "Egg")
        {
            Egg egg = other.GetComponent<Egg>();
            if (egg)
            {
                egg.Respawn();
            }
        }
    }
}