using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackManager : MonoBehaviour
{

    [Header("Target")]
    public GameObject target;

    
    public PlayerDetector playerDetector;
    public BossAttackSpiral spiralAttack;
    public BossAttackStar starAttack;
    public BossAttackTrack trackAttack;
    public Animator animator;

    [Header("Timing Settings")]
    public float delayBeforeAnimation;
    public float delaySpiralAttack;
    public float delayStarAttack;
    public float delayTrackAttack;
    public float delayAfterCycle = 5f;

    [Header("Audio Files")]
    public AudioClip SpiralAttackSFXClip;
    public AudioClip StarAttackSFXClip;
    public AudioClip TrackAttackSFXClip;
    public AudioClip DualBeamAttackSFXClip;
    public AudioClip SeedLaserAttackSFXClip;

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
        
        yield return new WaitForSeconds(delayBeforeAnimation);
        animator.SetBool("Spiral", true);
        yield return new WaitForSeconds(delaySpiralAttack);

        // Spiral Attack
        ShooterSFXManager.instance.PlaySFX(SpiralAttackSFXClip);
        spiralAttack.SpiralAttack();
        //yield return new WaitUntil(() => spiralAttack.isAttackComplete);
        yield return new WaitForSeconds(6f);
        animator.SetBool("Spiral", false);

        // Delay between attacks
        yield return new WaitForSeconds(delayBeforeAnimation);
        ShooterSFXManager.instance.PlaySFX(StarAttackSFXClip);
        animator.SetBool("Star", true);
        yield return new WaitForSeconds(delayStarAttack);

        // Star Attack
        starAttack.StarAttack();
        yield return new WaitUntil(() => starAttack.isAttackComplete);
        animator.SetBool("Star", false);

        // Delay between attacks
        yield return new WaitForSeconds(delayBeforeAnimation);

        // Hans cursed code starts here
        // Check if Target (player) is on the left or right, then play corresponding animation.
        if(target.transform.position.x >= transform.position.x){
            Debug.Log("Play Animation Shoot TO RIGHT");
            animator.SetBool("TrackLeft", true);
        }else{
            Debug.Log("Play Animation Shoot TO LEFT");
            animator.SetBool("TrackRight", true);
        }
        //Hans cursed code ends here
        
        yield return new WaitForSeconds(delayTrackAttack);

        // Track Attack
        ShooterSFXManager.instance.PlaySFX(TrackAttackSFXClip);
        trackAttack.TrackAttack();
        //yield return new WaitUntil(() => trackAttack.isAttackComplete);
        yield return new WaitForSeconds(4f);
        animator.SetBool("TrackRight", false);
        animator.SetBool("TrackLeft", false);
       
        // Hans cursed code starts here

        //Dual beam attack
        Debug.Log("Dual Beam START");
        yield return new WaitForSeconds(delayBeforeAnimation);
        ShooterSFXManager.instance.PlaySFX(DualBeamAttackSFXClip);
        animator.SetBool("DualBeam", true);
        yield return new WaitForSeconds(15f);
        animator.SetBool("DualBeam", false);
        Debug.Log("Dual Beam FINISH");

        //Seed laser attack
        Debug.Log("Seed Laser START");
        yield return new WaitForSeconds(delayBeforeAnimation);
        ShooterSFXManager.instance.PlaySFX(SeedLaserAttackSFXClip);
        animator.SetBool("SeedLaser", true);
        yield return new WaitForSeconds(7.5f);
        animator.SetBool("SeedLaser", false);
        Debug.Log("Seed Laser FINISH");




        //Hans cursed code ends here

        // Delay after full cycle
        yield return new WaitForSeconds(delayAfterCycle);
        
        

        if (canAttack)
        {
            StartCoroutine(AttackCycle());
        }
    }

   

}
