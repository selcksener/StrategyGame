using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{
    private static readonly int IsIdle = Animator.StringToHash("isIdle");
    private static readonly int IsFollowing = Animator.StringToHash("isFollowing");
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    [SerializeField] private Animator unitAnimator;
    public void IdleAnimation()
    {
        unitAnimator.SetBool(IsIdle,true);
        unitAnimator.SetBool(IsFollowing,false);
        unitAnimator.SetBool(IsAttacking,false);
    }

    public void FollowAnimation()
    {
        unitAnimator.SetBool(IsIdle,false);
        unitAnimator.SetBool(IsFollowing,true);
        unitAnimator.SetBool(IsAttacking,false);
    }

    public void AttackAnimation()
    {
        unitAnimator.SetBool(IsFollowing,false);
        unitAnimator.SetBool(IsIdle,false);
        unitAnimator.SetBool(IsAttacking,true);
    }

    public void StopFollow()
    {
        unitAnimator.SetBool(IsFollowing,false);
    }

    public void StopAttacking()
    {
        unitAnimator.SetBool(IsAttacking,false);
    }
}
