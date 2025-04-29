using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] float spawningInterval = 2f;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifetime;

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawningInterval);
        while(true)
        {
            GameObject gameobject = WaterBallsPool.sharedInstance.GetPooledObject();
            if (gameobject != null)
            {
                gameobject.transform.position = spawnPoint.position;
                gameobject.transform.rotation = spawnPoint.rotation;
                gameobject.SetActive(true);
                Projectile projectile = gameobject.GetComponent<Projectile>();
                if(projectile)
                {
                    projectile.SetValues(projectileSpeed, projectileLifetime);
                    StartCoroutine(projectile.Disolve());
                }
            }
            yield return new WaitForSeconds(spawningInterval);
        }
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }

    private void OnDrawGizmos()
    {
        Vector3 start = spawnPoint.position;
        Vector3 end = start + spawnPoint.forward * projectileSpeed * projectileLifetime;

        Gizmos.DrawLine(start, end);
    }
}
