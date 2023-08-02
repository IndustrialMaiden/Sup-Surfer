using System;
using UnityEngine;

namespace CodeBase.Logic.DragAndDropSystem
{
    public class DracControlTest : MonoBehaviour
    {
        private bool _isDragActive;

        private Vector2 _screenPosition;
        private Vector3 _worldPosition;

        private Draggable _lastDragged;

        private bool _isClicked;

        private void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isDragActive = !_isDragActive;
                _lastDragged = null;
            }
                
            Vector3 mousePos = Input.mousePosition;
            _screenPosition = new Vector2(mousePos.x, mousePos.y);
            _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);
                
            if (_isDragActive && _lastDragged != null)
            {
                _lastDragged.transform.position = new Vector2(_worldPosition.x, _worldPosition.y);
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);
                if (hit.collider != null)
                {
                    Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
                    if (draggable != null)
                    {
                        _lastDragged = draggable;
                    }
                }
            }
        }
    }
}