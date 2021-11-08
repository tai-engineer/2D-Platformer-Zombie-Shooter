using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PZS.BTAI;

namespace PZS
{
    [RequireComponent(typeof(CharacterController))]
    public class BossBehaviour : MonoBehaviour
    {
        CharacterController _controller;
        Animator _animator;

        int _bulletHitCount = 0;
        [SerializeField] int _startAmmo;
        int _currentAmmo;

        Transform _bossTransform;
        [SerializeField] LayerMask _PlayerLayer;
        bool _inMeleeRange = false;
        bool _meleeAttack = false;
        Vector2 _startPosition;
        Vector2 _faceDirection = Vector2.left;
        void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _currentAmmo = _startAmmo;
            _bossTransform = transform;
            _startPosition = _bossTransform.position;
        }

        Root _ai;
        void OnEnable()
        {
            _ai = new Root();
            _ai.OpenBranch
                (
                    BT.While(IsAlive).OpenBranch
                    (
                        BT.RandomSequence(new int[] { 6, 2, 1 }).OpenBranch
                        (
                            BT.Repeat(Random.Range(1, 5)).OpenBranch
                            (
                                BT.Trigger(_animator, "Shoot"),
                                BT.Call(Shoot),
                                BT.Wait(0.2f),
                                BT.WaitForAnimatorState(_animator, "Boss_Walk")
                            ),
                            BT.Root().OpenBranch
                            (
                                BT.While(TargetNotInMeleeRange).OpenBranch
                                (
                                    BT.Call(MoveToTarget)
                                ),
                                BT.Call(StopMovement),
                                BT.Trigger(_animator, "Melee"),
                                BT.Call(MeleeAttack),
                                BT.Wait(0.2f),
                                BT.WaitForAnimatorState(_animator, "Boss_Walk"),
                                BT.While(NotInBase).OpenBranch
                                (
                                    BT.Call(ReturnToBase)
                                ),
                                BT.Call(StopMovement)
                            ),
                            BT.Root().OpenBranch
                            (
                                BT.Trigger(_animator, "Throw"),
                                BT.Call(Throw),
                                BT.Wait(0.2f),
                                BT.WaitForAnimatorState(_animator, "Boss_Walk")
                            )
                        )
                    ),
                    BT.Terminate()
                );
        }

        void Update()
        {
            _ai.Tick();
            float distance = PlayerCharacter.Instance.transform.position.x - transform.position.x;
            if (distance > 0)
            {
                _faceDirection = Vector2.right;
                _bossTransform.localScale = new Vector3(-1, 1, 1);
            }
            else if(distance < 0)
            {
                _faceDirection = Vector2.left;
                _bossTransform.localScale = new Vector3(1, 1, 1);
            }
        }
        void FixedUpdate()
        {
            RaycastHit2D hit = Physics2D.Raycast(_bossTransform.position, _faceDirection, 2f, _PlayerLayer);
            _inMeleeRange = hit.collider != null;
        }
        void Shoot()
        {
            _controller.Shoot();
            if(_currentAmmo > 0)
                _currentAmmo--;
        }

        void OnBulletHit()
        {
            _bulletHitCount++;
        }
        void MeleeAttack()
        {
            _meleeAttack = true;
        }
        void MoveToTarget()
        {
            _controller.MovePosition(PlayerCharacter.Instance.transform.position.x);
        }
        void StopMovement()
        {
            _controller.ResetMoveVector();
        }
        bool TargetNotInMeleeRange() => !_inMeleeRange;
        void ReturnToBase()
        {
            _controller.MovePosition(_startPosition.x);
        }
        void Throw()
        {
            Debug.Log("_controller.MoveVector = " + _controller.MoveVector);
        }
        bool NotInBase() => !Mathf.Approximately(transform.position.x, _startPosition.x);
        bool IsAlive() => true;
    }
}
