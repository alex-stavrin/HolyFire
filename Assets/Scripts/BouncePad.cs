using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] float bounceForce;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if(playerController)
            {
                playerController.Bounce(bounceForce);
            }
        }
    }
}
