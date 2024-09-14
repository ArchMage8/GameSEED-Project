using System.Collections;
using UnityEngine;

public class BossAttackTrack : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject target;
    public float delayBeforeAttack;
    public float moveSpeed;

    private float hanFunnyVariableThatIDontKNowWhatToCall;

    [HideInInspector] public bool isAttackComplete;

    public void TrackAttack()
    {
        StartCoroutine(TrackCoroutine());
    }

    private IEnumerator TrackCoroutine()
    {
        isAttackComplete = false;

        yield return new WaitForSeconds(delayBeforeAttack);

        Vector2 direction = target.transform.position.x > transform.position.x ? Vector2.right : Vector2.left;

        for (int i = 0; i < 11; i++)
        {
            //Hans cursed code starts here
                if(target.transform.position.x >= transform.position.x){
                hanFunnyVariableThatIDontKNowWhatToCall = -18;
            }else{
                hanFunnyVariableThatIDontKNowWhatToCall = 18;
            }
            //Hans cursed code ends here

            GameObject projectile = Instantiate(objectToSpawn, transform.position + new Vector3(hanFunnyVariableThatIDontKNowWhatToCall, i-5, 0), Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * moveSpeed;
        }

        isAttackComplete = true;
    }
}
