using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] float spawningInterval = 2f;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform spawnPoint;

    IEnumerator Spawn()
    {
        while(true)
        {
            Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(spawningInterval);
        }
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }
}
