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
    public Animator animator;

    [Header("Timing Settings")]
    public float delayBetweenAttacks = 2f;
    public float delayAfterCycle = 5f;

    private bool isCycling = false;
    private EnemyCombatHandler enemyCombatHandler;
    private bool canAttack;

    private void Start()
    {
        enemyCombatHandler = GetComponent<EnemyCombatHandler>();
        canAttack = enemyCombatHandler.CanAttack;
    }

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
        
        yield return new WaitForSeconds(delayBetweenAttacks / 2);
        animator.SetBool("Spiral", true);
        yield return new WaitForSeconds(delayBetweenAttacks / 2);

        // Spiral Attack
        spiralAttack.SpiralAttack();
        yield return new WaitUntil(() => spiralAttack.isAttackComplete);
        animator.SetBool("Spiral", false);
        // Delay between attacks
        yield return new WaitForSeconds(delayBetweenAttacks / 2);
        animator.SetBool("Star", true);
        yield return new WaitForSeconds(delayBetweenAttacks / 2);

        // Star Attack
        starAttack.StarAttack();
        yield return new WaitUntil(() => starAttack.isAttackComplete);
        animator.SetBool("Star", false);
        // Delay between attacks
        yield return new WaitForSeconds(delayBetweenAttacks / 2);
        animator.SetBool("Track", true);
        yield return new WaitForSeconds(delayBetweenAttacks / 2);

        // Track Attack
        trackAttack.TrackAttack();
        yield return new WaitUntil(() => trackAttack.isAttackComplete);
        animator.SetBool("Track", false);
       
        // Delay after full cycle
        yield return new WaitForSeconds(delayAfterCycle);
        
        

        if (canAttack)
        {
            StartCoroutine(AttackCycle());
        }
    }

   

}
