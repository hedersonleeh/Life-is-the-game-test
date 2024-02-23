using System;
using System.Collections;
using UnityEngine;

public class GrapplingGun : Gun
{
    FpsController _controller;
    bool _usingGrappling;
    public override bool CanShoot => base.CanShoot && !_usingGrappling;
    protected override void Initialize()
    {
        base.Initialize();
        _controller = FindObjectOfType<FpsController>();
    }
    protected override void ShootGun()
    {
        var bullet = _pool.Get() as Hook;
        bullet.gameObject.SetActive(true);
        bullet.transform.position = shootPoint;
        Vector3 initialSpeed = aimDirection.normalized * Data.bulletPower;
        bullet.Rb.useGravity = Data.useGravity;
        bullet.Rb.velocity = initialSpeed;
        bullet.transform.forward = initialSpeed.normalized;
        bullet.InitializeHook(_shootPoint,OnHookLand);
        bullet.AssignPool(_pool);

    }

    private void OnHookLand(Hook hook)
    {
        
        StopAllCoroutines();
        StartCoroutine(HookRoutine(hook));
    }
    IEnumerator HookRoutine(Hook hook)
    {
        var duration = Data.hookMoveDuration;
        var stepTime = 0f;
        var animCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        var initialPosition = _controller.transform.position;
        var targetPosition = hook.transform.position ;
        targetPosition -= (targetPosition -initialPosition ) * .3f;//threshold for min distance with hook and target
        _controller.GetComponent<CharacterController>().enabled = false;
        _usingGrappling = true;
        while (stepTime < duration) 
        {
            stepTime += Time.deltaTime;
            var fixedValue = stepTime / duration;
            //lerp
          _controller.transform.position=  Vector3.Lerp(initialPosition, targetPosition, animCurve.Evaluate(fixedValue));
            yield return new WaitForFixedUpdate();
        }
        //to stick a bit;
        stepTime = 0f;
        while (stepTime < .3)
        {
            stepTime += Time.deltaTime;
            //lerp
            _controller.transform.position = targetPosition;
            yield return new WaitForFixedUpdate();
        }
        _controller.GetComponent<CharacterController>().enabled = true;
        _usingGrappling = false;

        _pool.BackToPool(hook);
    }
}
