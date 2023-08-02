using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Localization;
using CodeBase.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Components
{
    [RequireComponent(typeof(Text))]
    public class LocalizationComponent : MonoBehaviour
    {
        [SerializeField] private string _key;

        private CompositeDisposables _trash = new CompositeDisposables();

        private void Awake()
        {
            _trash.Retain(AllServices.Container.Single<ILocalizationService>().Subscribe(OnLocalizationChanged));
            Localize();
        }

        private void OnLocalizationChanged() => Localize();

        private void Localize() => GetComponent<Text>().text = 
            AllServices.Container.Single<ILocalizationService>().CurrentLocalization[_key];

        private void OnDestroy() => _trash.Dispose();
    }
}