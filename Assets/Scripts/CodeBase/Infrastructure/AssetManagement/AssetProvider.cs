using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssets
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector2 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
        
        public GameObject Instantiate(GameObject prefab, Vector2 at)
        {
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
        
        public GameObject Instantiate(GameObject prefab, Transform parent)
        {
            return Object.Instantiate(prefab, parent);
        }
    }
}