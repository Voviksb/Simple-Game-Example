using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBase : MonoBehaviour
{
    [SerializeField] protected Image _healthBar;
    [SerializeField] protected UnitBehaviour healthBarOwner;
    void OnEnable()
    {
      //  Debug.Log(healthBarOwner.name + "Initialized hpbar in base class");
        healthBarOwner.OnDamageReceivedEvent += OnDamageReceived;
    }
    void OnDestroy()
    {
        healthBarOwner.OnDamageReceivedEvent -= OnDamageReceived;
    }
    protected virtual void OnDamageReceived(float valueAsPercentage)
    {
       // Debug.Log("Calling parent class" + healthBarOwner.name);
        _healthBar.fillAmount = valueAsPercentage;
    }
}
