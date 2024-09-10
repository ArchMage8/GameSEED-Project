using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackManager : MonoBehaviour
{
    [Header("Attack System")]
    public PlayerDetector playerDetector;
    public BossAttackSpiral spiralAttack;
    public BossAttackStar starAttack;
    public BossAttackTrack trackAttack;

    [Header("Timing Settings")]
    public float delayBetweenAttacks = 2f;
    public float delayAfterCycle = 5f;

    private bool isCycling = false;

    private void Update()
    {
        if (playerDetector.playerInRange && !isCycling)
        {
            Debug.Log("Test");
            StartCoroutine(AttackCycle());
        }

        else if (playerDetector.playerInRange == false)
        {
            isCycling = false;
            StopCoroutine(AttackCycle());
        }
    }

    private IEnumerator AttackCycle()
    {
        isCycling = true;
        
        yield return new WaitForSeconds(delayBetweenAttacks);

        // Spiral Attack
        spiralAttack.SpiralAttack();
        yield return new WaitUntil(() => spiralAttack.isAttackComplete);

        // Delay between attacks
        yield return new WaitForSeconds(delayBetweenAttacks);

        // Star Attack
        starAttack.StarAttack();
        yield return new WaitUntil(() => starAttack.isAttackComplete);

        // Delay between attacks
        yield return new WaitForSeconds(delayBetweenAttacks);

        // Track Attack
        trackAttack.TrackAttack();
        yield return new WaitUntil(() => trackAttack.isAttackComplete);

        // Delay after full cycle
        yield return new WaitForSeconds(delayAfterCycle);

        // Repeat cycle
        StartCoroutine(AttackCycle());
    }
}
