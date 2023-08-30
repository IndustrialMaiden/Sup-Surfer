using CodeBase.Infrastructure;
using CodeBase.Logic.DragAndDropSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class InstructionScreen : WindowBase
    {
        private Draggable _draggable;
        
        protected override void OnAwake()
        {
            
        }

        protected override void Start()
        {
            _draggable = GameObject.FindWithTag("Draggable").GetComponent<Draggable>();
            _draggable._isFirstTouch = false;
            _draggable.OnFirstTouch.AddListener(OnPlayerClick);
        }

        private void OnPlayerClick()
        {
            _draggable._isFirstTouch = true;
            Game.Resume();
            Destroy(gameObject);
        }

        protected override void OnDestroy()
        {
            _draggable.OnFirstTouch.RemoveListener(OnPlayerClick);
        }
    }
}