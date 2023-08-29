using System;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] public Button CloseButton;
        
        protected IPlayerProgressService PlayerProgress;
        protected PlayerProgress Progress => PlayerProgress.Progress;

        private Animator _animator;
        private static readonly int Show = Animator.StringToHash("Show");

        protected Action _closeAction;

        public void Construct(IPlayerProgressService playerProgress) => PlayerProgress = playerProgress;

        protected virtual void OnAwake()
        {
            CloseButton.onClick.AddListener(HideWindow);
            _animator = GetComponent<Animator>();
            ShowWindow();
        }

        public virtual void OnShowAnimationComplete(){}

        public virtual void OnCloseAnimationComplete() => _closeAction?.Invoke();

        protected virtual void Initialize(){}

        protected virtual void SubscribeUpdates(){}

        private void Awake() => OnAwake();

        protected virtual void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void ShowWindow() => _animator.SetBool(Show, true);

        private void HideWindow() => _animator.SetBool(Show, false);

        protected virtual void OnDestroy() => CloseButton.onClick.RemoveListener(HideWindow);
    }
}