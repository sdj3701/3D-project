using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//추상 클래스
public abstract class AppearanceInfo : MonoBehaviour
{
    protected CharacterBase owner;

    public Dictionary<string, Collider> DamageBoxes = new Dictionary<string, Collider>();

    public Collider GetDamageBox(string boxName)
    {
        if(DamageBoxes.ContainsKey(boxName))
        {
            return DamageBoxes[boxName];
        }
        return null;
    }

    public void DamageBoxEnable(string boxName)
    {
        Collider col = GetDamageBox(boxName);
        if(col) col.enabled = true;
    }

    public void DamageBoxDisable(string boxName)
    {
        Collider col = GetDamageBox(boxName);
        if(col) col.enabled = false;
    }

    public abstract AppearanceType GetAppearanceType();

    public abstract void Rotate(Vector3 direction);

    protected virtual void Start()
    {
        owner = GetComponentInParent<CharacterBase>();
    }

    public void ClaimAttack()
    {
        owner?.Attack(owner);
    }
}
