using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public GameObject Sickle;
    public GameObject Lasso;
    public GameObject Knife;
    public GameObject Default;

    [HideInInspector] public bool Active_Sickle;
    [HideInInspector] public bool Active_Lasso;
    [HideInInspector] public bool Active_Knife;
    [HideInInspector] public bool Active_Default;

    private void Start()
    {
        UpdateActiveObject();
    }

    public void SetActiveBool(bool sickle, bool lasso, bool knife)
    {
        Active_Sickle = sickle;
        Active_Lasso = lasso;
        Active_Knife = knife;
        UpdateActiveObject();
    }

    private void UpdateActiveObject()
    {
        if (Active_Sickle)
        {
            SetActiveObject(Sickle);
        }
        else if (Active_Lasso)
        {
            SetActiveObject(Lasso);
        }
        else if (Active_Knife)
        {
            SetActiveObject(Knife);
        }
        else
        {
            SetActiveObject(Default);
        }
    }

    private void SetActiveObject(GameObject activeObject)
    {
        Sickle.SetActive(Sickle == activeObject);
        Lasso.SetActive(Lasso == activeObject);
        Knife.SetActive(Knife == activeObject);
        Default.SetActive(Default == activeObject);
    }
}

