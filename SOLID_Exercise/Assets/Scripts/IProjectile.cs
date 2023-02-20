using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IProjectile
{
    int _Damage { get; }
    IShapeBehaviour _Creator { get; } //Shape that spawned the projectile

    public void Die();


}

public static class IProjectileHelper
{
    public static void DefaultTriggerBehaviour(this IProjectile thisProjectile, Collider2D col)
    {
        ShapeMotor shapeMotor = col.GetComponent<ShapeMotor>();
        IShapeBehaviour shapeBehaviour = col.GetComponent<IShapeBehaviour>();
        IProjectile projectile = col.GetComponent<IProjectile>();

        if (projectile != null && projectile.GetType() != thisProjectile.GetType())
        {
            thisProjectile.Die();
        }
        //If ShapeMotor is on Object and Shapebehavior is something else than the Type that fired. i.e. triangles can not hit other triangles
        else if (shapeBehaviour != null && shapeBehaviour.GetType() != thisProjectile._Creator.GetType())
        {
            shapeMotor._Health -= thisProjectile._Damage;
            thisProjectile.Die();
        }
        else if (col.CompareTag("Wall"))
            thisProjectile.Die();
    }
}