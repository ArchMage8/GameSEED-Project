using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    private WeaponHandler Handler;
    
    [Header("Weapon To Switch to (Pick One: )")]
    public bool setActiveSickle;
    public bool setActiveLasso;
    public bool setActiveKnife;
    private bool Toggled = false;

    private void Start()
    {
        ValidateSetup();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Toggled && collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.L))
            {
                Handler = collision.GetComponent<WeaponHandler>();
                ChangeWeapon();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Handler = null;
    }

    private void ValidateSetup()
    {
        int trueCount = 0;
        if (setActiveSickle) trueCount++;
        if (setActiveLasso) trueCount++;
        if (setActiveKnife) trueCount++;

        if(trueCount == 0)
        {
            Debug.LogError("Pick 1 weapon to switch to");
        }
        
        if (trueCount > 1)
        {
            Debug.LogError("Error: More than one bool is set to true.");
        }
    }

    private void ChangeWeapon()
    {
        if (Handler == null)
        {
            Debug.Log("Handler Not Found");
        }
        else
        {
            
            Toggled = true;
            Handler.SetActiveBool(setActiveSickle, setActiveLasso, setActiveKnife);
        }
    }
}
