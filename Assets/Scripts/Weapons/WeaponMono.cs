using System.Collections;
using System.Collections.Generic;
using Players.Animation;
using UnityEngine;

public class WeaponMono : WeaponInformation
{




    private CharacterMono _characterMono;
    private ThirdPersonAnimation _thirdPersonAnimation;

    private void Awake()
    {
        _characterMono = GetComponent<CharacterMono>();
        _thirdPersonAnimation = GetComponent<ThirdPersonAnimation>();

    }

//    public Weapon.WeaponEnum CurrentWeapon()
//    {
//        //return MyWeapon.
//    }

    public bool RestrictAnimationRotation()
    {
        return MyWeapon.RestrictAnimationRotation();
    }

    public void TransferWeapons(Weapon weapon, GameObject left, GameObject right)
    {
        MyWeapon = weapon;
        Damage = MyWeapon.Damage;
        Range = MyWeapon.Range;
        Speed = MyWeapon.Speed;
        Left = left;
        Right = right;
        CurrentWeapon = MyWeapon.MyCurrentWeapon;
        _thirdPersonAnimation.WeaponChanged(CurrentWeapon);

        
        //[0] = leftHand, [1] = rightHand
        var transforms = _characterMono.GetRightAndLeftHand();
        
        if (Left != null)
        {
            Instantiate(Left, transforms[0]);
        }

        if (Right != null)
        {
            Instantiate(Right, transforms[1]);
        }
    }

}
