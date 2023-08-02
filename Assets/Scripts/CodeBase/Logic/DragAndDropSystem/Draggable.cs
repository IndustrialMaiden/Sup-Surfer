using System;
using UnityEngine;

namespace CodeBase.Logic.DragAndDropSystem
{
    public class Draggable : MonoBehaviour
    {
        private Camera _cam;

        private float _width;
        private float _height;
        
        private void Awake()
        {
            _cam = Camera.main;
            gameObject.tag = "Draggable";
        }

        private void LateUpdate()
        {
            FindBoundries();
            SetPosition();
        }

        private void FindBoundries()
        {
            _width = 1 / (_cam.WorldToViewportPoint(new Vector3(1, 1, 0)).x - 0.5f);
            _height = 1 / (_cam.WorldToViewportPoint(new Vector3(1, 1, 0)).y - 0.5f);
        }

        private void SetPosition()
        {
            var xConstraint = Mathf.Clamp(transform.position.x, -_width/2 + 0.2f, _width/2 - 0.2f);
            var yConstraint = Mathf.Clamp(transform.position.y, -_height/2 + 0.5f, _height/2 - 0.5f);
            transform.position = new Vector3(xConstraint, yConstraint, 0);
        }
    }
}