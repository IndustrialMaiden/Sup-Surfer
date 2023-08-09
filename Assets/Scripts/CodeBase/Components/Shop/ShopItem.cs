/*using System;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Utils.Disposables;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.Components.Shop
{
    public class ShopItem : MonoBehaviour
    {
        
        [SerializeField] private string _id;
        [SerializeField] private Text _itemName;
        [SerializeField] private Button _buyButton;
        [SerializeField] private int _price;
        [SerializeField] private Text _priceText;
        [SerializeField] private int _valueToAdd;

        private PlayerProgress _progress;

        private CompositeDisposables _trash = new CompositeDisposables();

        private UnityAction<string, int> _onBuyAction;

        private void Awake()
        {
            _trash.Retain(_buyButton.onClick.Subscribe(OnBuy));
            _trash.Retain(Game.Player.Credits.Subscribe(OnCreditsChanged));
            _progress = AllServices.Container.Single<IPlayerProgressService>().Progress;
        }

        private void Start()
        {
            _price = GetPrice();
        }

        public IDisposable Subscribe(UnityAction<string, int> call)
        {
            _onBuyAction += call;
            return new ActionDisposables(() => _onBuyAction -= call);
        }

        public void SetCost(int price)
        {
            _price = price;
            _priceText.text = new string(_price + " кредитов");
        }

        private int GetPrice()
        {
            foreach (BoolPurchase purchase in _progress.BoolPurchases)
            {
                if (purchase.Id == _id)
                {
                    return purchase.Price;
                }
            }

            foreach (IntPurchase purchase in _progress.IntPurchases)
            {
                if (purchase.Id == _id)
                {
                    return purchase.Price;
                }
            }

            Debug.LogWarning("Can't find Purchase with ID: " + _id);
            return 9999;
            
        }

        private void OnBuy()
        {
            Game.Player.Credits.Value -= _price;
            AllServices.Container.Single<ISaveLoadService>().SaveProgress();
            _onBuyAction.Invoke(_id, _valueToAdd);
        }

        private void OnCreditsChanged(int newvalue, int oldvalue)
        {
            _buyButton.interactable = newvalue > _price;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}*/