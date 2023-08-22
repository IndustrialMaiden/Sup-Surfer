using System.Collections;
using UnityEngine;

namespace CodeBase.Logic.Background
{
    public class BackgroundDestroy : MonoBehaviour
    {
        
        
        public void Destroy()
        {
            StartCoroutine(DestroyBackground());
        }

        private IEnumerator DestroyBackground()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();

            while (spriteRenderer.color.a > 0)
            {
                var color = spriteRenderer.color;
                color.a -= 0.01f;
                spriteRenderer.color = color;
                yield return new WaitForSeconds(0.01f);
            }
            Destroy(gameObject);
        }
    }
}