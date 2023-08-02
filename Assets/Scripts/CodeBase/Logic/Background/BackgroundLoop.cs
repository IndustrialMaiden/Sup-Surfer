using System;
using System.Security.Cryptography;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Logic.Background
{
    public class BackgroundLoop : MonoBehaviour
    {
        [SerializeField] private int _movingSpeed;
        [SerializeField] private float _offset;
        
        [SerializeField] private GameObject[] _levels;

        private Vector2 _screenBounds;

        private ParentChildPositions[] _childPositions;

        private IGameFactory _factory;

        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        private void Start()
        {
            _screenBounds = Camera.main.ScreenToWorldPoint(
                new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            
            _childPositions = new ParentChildPositions[_levels.Length];

            for (int i = 0; i < _levels.Length; i++)
            {
                _childPositions[i] = ConstructBackgroundSize(_levels[i]);
            }
            
        }

        private ParentChildPositions ConstructBackgroundSize(GameObject obj)
        {
            float objectHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y - _offset;
            int objectsNeeded = (int)Mathf.Ceil(_screenBounds.y * 2 / objectHeight);

            //GameObject parent = Instantiate(obj, transform);
            GameObject parent = _factory.CreatePrefabWithParent(obj, transform);
            parent.transform.localScale = new Vector3(1, 1, 1);
            //GameObject clone = Instantiate(obj);
            GameObject clone = _factory.CreatePrefabUnregistered(obj, Vector2.zero);
            for (int i = 0; i <= objectsNeeded; i++)
            {
                //GameObject c = Instantiate(clone, parent.transform);
                GameObject c = _factory.CreatePrefabWithParent(clone, parent.transform);
                
                var position = obj.transform.position;
                c.transform.position = new Vector3(
                    position.x, objectHeight * i, position.z);
                c.name = obj.name + i;
            }
            
            Destroy(clone);
            Destroy(parent.GetComponent<SpriteRenderer>());
            
            Transform[] childrensTransform = parent.GetComponentsInChildren<Transform>();

            return new ParentChildPositions() 
                {Parent = parent, 
                    FirstChildPos = childrensTransform[1].position, 
                    LastChildPos = childrensTransform[^1].position};
        }

        private void LateUpdate()
        {
            foreach (var obj in _childPositions)
            {
                MoveLevel(obj);
            }
            
        }

        private void MoveLevel(ParentChildPositions obj)
        {
            Transform[] childrensTransform = obj.Parent.GetComponentsInChildren<Transform>();

            if (childrensTransform.Length > 1)
            {
                GameObject firstChild = childrensTransform[1].gameObject;
                GameObject lastChild = childrensTransform[^1].gameObject;

                float objectHeight = lastChild.GetComponent<SpriteRenderer>().bounds.extents.y * 2 - _offset;

                firstChild.transform.position = Vector3.MoveTowards(
                    firstChild.transform.position, firstChild.transform.position + new Vector3(
                        0, -_movingSpeed, 0), _movingSpeed * Time.deltaTime);
                
                lastChild.transform.position = Vector3.MoveTowards(
                    lastChild.transform.position, lastChild.transform.position + new Vector3(
                        0, -_movingSpeed, 0), _movingSpeed * Time.deltaTime);

                if (firstChild.transform.position.y < obj.FirstChildPos.y - objectHeight)
                {
                    firstChild.transform.position = new Vector3(
                        obj.LastChildPos.x, obj.LastChildPos.y, obj.LastChildPos.z);
                }
                
                else if (lastChild.transform.position.y < obj.FirstChildPos.y - objectHeight)
                {
                    lastChild.transform.position = new Vector3(
                        obj.LastChildPos.x, obj.LastChildPos.y, obj.LastChildPos.z);
                }
            }
        }
    }
}