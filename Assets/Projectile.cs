using UnityEngine;
using System.Collections;


public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] float dieDelay = 5f;

    IEnumerator Disolve()
    {
        yield return new WaitForSeconds(dieDelay);
        Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(Disolve());
    }

    void Update()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }
}
