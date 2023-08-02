using CodeBase.Components.Shop;
using CodeBase.Infrastructure.Services.Localization;
using CodeBase.StaticData.Localization;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "DefsFacade", menuName = "Defs/DefsFacade")]
    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private LocalizationDef[] _localizations;
        [SerializeField] public Purchases Purchases;


        private static readonly string DefsFacadePath = "StaticData/DefsFacade";
        private static DefsFacade _instance;
        public static DefsFacade Instance => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade LoadDefs()
        {
            return _instance = Resources.Load<DefsFacade>(DefsFacadePath);
        }

        public LocalizationDef GetLocalizationDef(LocalizationType localization)
        {
            return _localizations[(int) localization];
        }
    }
}