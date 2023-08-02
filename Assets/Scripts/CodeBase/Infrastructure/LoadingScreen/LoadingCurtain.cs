using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.LoadingScreen
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;


        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1f;
        }

        public void Hide()
        {
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= 0.10f;
                yield return new WaitForSecondsRealtime(0.01f);
            }

            gameObject.SetActive(false);
        }
    }
}