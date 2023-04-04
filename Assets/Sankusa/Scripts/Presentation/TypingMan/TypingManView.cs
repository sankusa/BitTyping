using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.BitTyping.Domain;
using Zenject;
using UniRx;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Sankusa.BitTyping.Presentation
{
    public class TypingManView : MonoBehaviour
    {
        private readonly static int hashIdle = Animator.StringToHash("Idle");
        private readonly static int hashWalk = Animator.StringToHash("Walk");
        private readonly static int hashRightArmIdle = Animator.StringToHash("RightArmIdle");
        private readonly static int hashRightArmUp = Animator.StringToHash("RightArmUp");
        private readonly static int hashRightArmDown = Animator.StringToHash("RightArmDown");
        private readonly static int hashLeftArmUp = Animator.StringToHash("LeftArmUp");
        private readonly static int hashLeftArmIdle = Animator.StringToHash("LeftArmIdle");
        private readonly static int hashLeftArmDown = Animator.StringToHash("LeftArmDown");
        private readonly static int hashEyeRight = Animator.StringToHash("EyeRight");
        private readonly static int hashEyeDown = Animator.StringToHash("EyeDown");
        [SerializeField] private Animator animator;
        [SerializeField] private Vector2 fadeInPosition;
        [SerializeField] private Vector2 fadeOutPosition;
        [Inject] private TypingCore typingCore;
        private Tweener moveTweener;
        private readonly CompositeDisposable linkDisposabe = new CompositeDisposable();

        void Start()
        {
            typingCore
                .OnInput
                .Subscribe(x =>
                {

                })
                .AddTo(this);

            PlayFadeIn();
        }

        public void PlayFadeIn()
        {
            transform.position = fadeOutPosition;
            moveTweener = transform
                .DOMove(fadeInPosition, 100)
                .SetSpeedBased()
                .OnComplete(() =>
                {
                    animator.SetTrigger(hashIdle);
                })
                .SetLink(gameObject);

            animator.SetTrigger(hashWalk);
            animator.SetTrigger(hashRightArmIdle);
            animator.SetTrigger(hashLeftArmIdle);
            animator.SetTrigger(hashEyeRight);
        }

        public void PlayFadeOut()
        {
            moveTweener = transform
                .DOMove(fadeOutPosition, 50)
                .SetSpeedBased()
                .OnComplete(() =>
                {
                    animator.SetTrigger(hashIdle);
                })
                .SetLink(gameObject);

            animator.SetTrigger(hashWalk);
            animator.SetTrigger(hashRightArmIdle);
            animator.SetTrigger(hashLeftArmIdle);
            animator.SetTrigger(hashEyeDown);
        }

        public void SkipFadeIn()
        {
            transform.position = fadeInPosition;
            animator.SetTrigger(hashIdle);
            animator.SetTrigger(hashWalk);
            animator.SetTrigger(hashRightArmIdle);
            animator.SetTrigger(hashLeftArmIdle);
            animator.SetTrigger(hashEyeRight);
        }

        public async UniTask MoveEyeAsync(CancellationToken token)
        {
            for(int i = 0; i < 10; i++)
            {
                animator.SetTrigger(hashEyeRight);
                await UniTask.Delay((int)(1000 * Random.value), cancellationToken: token);

                animator.SetTrigger(hashEyeDown);
                await UniTask.Delay((int)(1000 * Random.value), cancellationToken: token);
            }
        }

        public void StartLinkTypingInput()
        {
            animator.SetTrigger(hashEyeDown);
            typingCore
                .OnInput
                .Subscribe(input =>
                {
                    if(input)
                    {
                        animator.SetTrigger(hashLeftArmUp);
                        animator.SetTrigger(hashLeftArmDown);
                        animator.SetTrigger(hashRightArmUp);
                    }
                    else
                    {
                        animator.SetTrigger(hashLeftArmUp);
                        animator.SetTrigger(hashRightArmUp);
                        animator.SetTrigger(hashRightArmDown);
                    }
                })
                .AddTo(linkDisposabe);
        }

        public void StopLinkTypingInput()
        {
            linkDisposabe.Clear();
        }
    }
}