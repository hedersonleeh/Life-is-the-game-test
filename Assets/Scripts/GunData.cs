using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="",menuName ="LIG/Scriptables")]
public class GunData : ScriptableObject
{
    public Sprite icon;
    public float bulletPower;
    public float bulletSpeedYMultiplier;
    public float firingRate;
    public Bullet bulletPrefab;
}
