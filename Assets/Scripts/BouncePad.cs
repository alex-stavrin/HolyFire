using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] float bounceForce;
    [SerializeField] AudioSource bounceSound;

    [SerializeField] Material activeMaterial;
    [SerializeField] Material inactiveMaterial;

    public bool isActive = true;

    MeshRenderer filter;

    private void Start()
    {
        filter = GetComponent<MeshRenderer>();
        if(isActive)
        {
            filter.material = activeMaterial;
        }
        else
        {
            filter.material = inactiveMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isActive)
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

    public void Activate()
    {
        if(!isActive)
        {
            isActive = true;
            filter.material = activeMaterial;
        }
    }
}
