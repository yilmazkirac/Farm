using System;
using UnityEngine;
    public class PlayerAnimator : MonoBehaviour
    {
       [SerializeField] private Animator animator;
       
       public void ManageAnimations(Vector3 move)
        {
            if (move.magnitude>0)
            {
                PlayRunAnimation();

                animator.transform.forward = move.normalized;
            }
            else
            {
                PlayIdleAnimation();
            }
        }

        private void PlayRunAnimation()
        {
            animator.Play("Run");
        }
        private void PlayIdleAnimation()
        {
            animator.Play("Idle");
        }
    }
