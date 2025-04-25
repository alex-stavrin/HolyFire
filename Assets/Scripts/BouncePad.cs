using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] float bounceForce;
    [SerializeField] AudioSource bounceSound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if(playerController)
            {
                playerController.Bounce(bounceForce);
                bounceSound.Play();
            }
        }
    }
}
