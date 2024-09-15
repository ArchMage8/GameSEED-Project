using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBar : MonoBehaviour
{

    public GameObject Boss;
    public Transform HPBar;

    public float bosshp = 40;

    private float bosscurrhp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyCombatHandler BossA = Boss.GetComponent<EnemyCombatHandler>();
        float healthPercentage = BossA.health / bosshp;
        healthPercentage = Mathf.Clamp01(healthPercentage);
        Vector3 newScale = HPBar.localScale;
        newScale.x = healthPercentage;
        HPBar.localScale = newScale;

        if(BossA.health <= 0){
            Destroy(this.gameObject);
        }
    }
}
