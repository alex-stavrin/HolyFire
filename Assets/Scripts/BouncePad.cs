using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] float bounceForce;
    [SerializeField] AudioSource bounceSound;

    [SerializeField] Material activeMaterial;
    [SerializeField] Material inactiveMaterial;
    [SerializeField] float eggDivider = 4.5f;

    public bool isActive = true;

    MeshRenderer filter;

    private void Start()
    {
        filter = GetComponent<MeshRenderer>();
        if(isActive)
        {
            filter.material = activeMaterial;
            gameObject.layer = LayerMask.NameToLayer("Bounce");
        }
        else
        {
            filter.material = inactiveMaterial;
            gameObject.layer = LayerMask.NameToLayer("Ground");
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
            else if(other.tag == "Egg")
            {
                Rigidbody rigidbody = other.GetComponent<Rigidbody>();
                if(rigidbody)
                {
                    rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, 0, rigidbody.linearVelocity.z);
                    rigidbody.AddForce(Vector3.up * bounceForce / eggDivider, ForceMode.Impulse);
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
            gameObject.layer = LayerMask.NameToLayer("Bounce");
        }
    }
}
