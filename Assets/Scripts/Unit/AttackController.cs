using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject bulletPrefab;
    
    private Unit unit;
    private UnitMovement unitMovement;
    [SerializeField] private UnitAttackData attackData;
    [SerializeField] private Transform rangeTrigger;
    [SerializeField] private Transform targetToAttack;


    [SerializeField] private bool isAttacking = false;

    private Vector3 centerPosition = Vector3.zero;
    private float attackTime = 0f;
    

    private void Start()
    {
        unit = GetComponent<Unit>();
        unitMovement = GetComponent<UnitMovement>();
        attackData = GameManager.Instance.UnitObjectDataSO.unitsData.Find(x => x.Name == unit.UnitName).UnitAttackData;
        rangeTrigger.localScale = new Vector3(attackData.Range, attackData.Range, attackData.Range);
    }

    public void Update()
    {
        if ( targetToAttack == null ||(targetToAttack != null && targetToAttack.gameObject.activeSelf == false))
        {
            if (isAttacking)
            {
                isAttacking = false;
                StopAttack();
            }

            return;
        }
        // check distance between unit and target object 
        if (Vector2.Distance(transform.position, targetToAttack.position) < attackData.Range*4 &&
            unitMovement.IsCommandToMove == false)
        {
            if (isAttacking == false)
            {
                Vector3 dir = (targetToAttack.position + centerPosition) - transform.position;
                float rotationz = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rotationz);
                unit.ChangeState(UnitState.Attacking);
                //unitMovement.StopFollow();
                isAttacking = true;
            }
        }
        else
        {
            isAttacking = false;
        }

        if (isAttacking)
            AttackToTarget();
    }
    /// <summary>
    /// Stop attacking
    /// </summary>
    private void StopAttack()
    {
        unit.unitAnimationController.StopAttacking();
    }

    /// <summary>
    /// attack to target
    /// </summary>
    private void AttackToTarget()
    {
        if (attackTime >= attackData.RateOfFire)
        {
            attackTime = 0f;
            Bullet bullet = PoolManager.Instance.GetObject(PoolObjectType.Bullet).GetComponent<Bullet>();
            bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
            bullet.SetDamage(transform, attackData.Damage);
        }
        else
        {
            attackTime += Time.deltaTime;
        }
    }
    /// <summary>
    /// updating the target object
    /// </summary>
    /// <param name="attackable"></param>
    public void SetTarget(GameObject attackable)
    {
        targetToAttack = attackable.transform;
        if (attackable.TryGetComponent(out SpriteRenderer renderer))
        {
            //for to be at the center point
            centerPosition = renderer.bounds.size;
            centerPosition.x /= 2;
            centerPosition.y /= 2;
        }

        //isAttacking = false;
    }
}