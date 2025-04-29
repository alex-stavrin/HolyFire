using UnityEngine;
using System.Collections;


public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifetime = 5f;

    public IEnumerator Disolve()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(Disolve());
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void SetValues(float speed, float lifetime)
    {
        this.speed = speed;
        this.lifetime = lifetime;
    }

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
    }
}
