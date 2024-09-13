using System.Collections;
using UnityEngine;

public class BossAttackSpiral : MonoBehaviour
{
    public GameObject objectToSpawn;
    public int maxParticles;
    public float spawnDelay;

    [HideInInspector] public bool isAttackComplete;

    public void SpiralAttack()
    {
        StartCoroutine(SpiralCoroutine());
    }

    private IEnumerator SpiralCoroutine()
    {
        isAttackComplete = false;
        int particlesSpawned = 0;

        while (particlesSpawned < maxParticles)
        {
            Debug.Log("Spawner Position: " + transform.position);
            // Use the current position of the object for each particle
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            particlesSpawned++;
            yield return new WaitForSeconds(spawnDelay);
        }

        isAttackComplete = true;
    }
}
