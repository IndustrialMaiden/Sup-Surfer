using System;
using System.Collections;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Components.Shop
{
    public class ShopController : MonoBehaviour
    {
        [Space][Header("Credits")]
        [SerializeField] private Text _creditsText;

        [Space] [Header("Prices")] 
        [SerializeField] private int[] _prices;
        
        [Space][Header("Shop Items")]
        [SerializeField] private ShopItem[] _shopItems;
        [Space]
        [SerializeField] private CanvasGroup _canvasGroup;

        private PlayerProgress _progress;
        


        private CompositeDisposables _trash = new CompositeDisposables();
        private void Awake()
        {
            _progress = AllServices.Container.Single<IPlayerProgressService>().Progress;
            
            _trash.Retain(Game.Player.Credits.Subscribe(OnCreditsChanged));
            
            BuyActionSubscription();
            ReloadText();
            
            _canvasGroup.alpha = 0f;
            gameObject.SetActive(false);
        }

        private void OnCreditsChanged(int newValue, int oldValue)
        {
            ReloadText();
        }

        private void BuyActionSubscription()
        {
            foreach (ShopItem shopItem in _shopItems)
            {
                _trash.Retain(shopItem.Subscribe(OnItemBought));
            }
        }

        private void OnItemBought(string id, int valueToAdd)
        {
            if (valueToAdd == 0)
            {
                foreach (BoolPurchase purchase in _progress.BoolPurchases)
                {
                    if (purchase.Id == id)
                    {
                        purchase.Value = true;
                        AllServices.Container.Single<ISaveLoadService>().SaveProgress();
                        break;
                    }
                }
            }

            else
            {
                foreach (IntPurchase purchase in _progress.IntPurchases)
                {
                    if (purchase.Id == id)
                    {
                        purchase.Value = valueToAdd;
                        AllServices.Container.Single<ISaveLoadService>().SaveProgress();
                        break;
                    }
                }
            }
            
        }

        private void ReloadText()
        {
            _creditsText.text = Game.Player.Credits.Value.ToString();
        }
        
        public void ShowShopMenu()
        {
            gameObject.SetActive(true);
            StartCoroutine(FadeIn());
        }

        public void CloseShopMenu()
        {
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeIn()
        {
            while (_canvasGroup.alpha < 1f)
            {
                _canvasGroup.alpha += 0.10f;
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }

        private IEnumerator FadeOut()
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= 0.10f;
                yield return new WaitForSecondsRealtime(0.01f);
            }
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}