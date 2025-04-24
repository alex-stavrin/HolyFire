using UnityEngine;

public class Campfire : MonoBehaviour
{
    [SerializeField] Transform checkpoint;
    [SerializeField] Light pointLight;
    [SerializeField] ParticleSystem fireParticleSystem;
    [SerializeField] AudioSource fireLitSound;


    bool isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!isActivated)
        {
            if(other.tag == "Player")
            {
                PlayerController playerController = other.GetComponent<PlayerController>();
                if(playerController)
                {
                    isActivated = true;
                    fireParticleSystem.Play();
                    pointLight.intensity = 5f;
                    playerController.lastCheckpoint = checkpoint.position;
                    fireLitSound.Play();
                }
            }
        }
    }
}
