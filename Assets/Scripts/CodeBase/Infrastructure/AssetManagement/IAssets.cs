using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector2 at);

        GameObject Instantiate(GameObject prefab, Vector2 at);

        GameObject Instantiate(GameObject prefab, Transform parent);
        GameObject Instantiate(GameObject prefab, Transform parent, Vector2 at);
    }
}