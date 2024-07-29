using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] MeeleWeaponDamageCollider meeleDamageCollider;

    private void Awake()
    {
        meeleDamageCollider = GetComponentInChildren<MeeleWeaponDamageCollider>();
    }

    public void SetWeaponDamage(CharacterManager CharacterWieldingWeapon,WeaponItem weapon)
    {
        meeleDamageCollider.characterCausingDamage = CharacterWieldingWeapon;
        meeleDamageCollider.physicalDamage = weapon.physicalDamage;
        meeleDamageCollider.magicDamage = weapon.magicDamage;
        meeleDamageCollider.fireDamage = weapon.fireDamage;
        meeleDamageCollider.lightingDamage = weapon.lightingDamage;
        meeleDamageCollider.holyDamage = weapon.holyDamage;

    }
}
