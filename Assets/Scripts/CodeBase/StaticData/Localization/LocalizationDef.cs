using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.StaticData.Localization
{
    [CreateAssetMenu(fileName = "Localization", menuName = "StaticData/Localization", order = 0)]
    public class LocalizationDef : ScriptableObject
    {
        [SerializeField] private List<LocaleItem> _localeItems;
        [SerializeField] private string _url;
        
        private UnityWebRequest _request;
        private string _textData;

        public Dictionary<string, string> GetLocale()
        {
            var locale = new Dictionary<string, string>();

            foreach (LocaleItem localeItem in _localeItems)
            {
                locale.Add(localeItem.Key, localeItem.Value);
            }

            return locale;
        }
        
        [ContextMenu("Update Localization")]
        public void LoadData()
        {
            if (_request != null) return;
            
            _request = UnityWebRequest.Get(_url);
            _request.SendWebRequest().completed += OnDataLoaded;
        }
        
        private void OnDataLoaded(AsyncOperation operation)
        {
            if (!operation.isDone) return;
            
            Debug.Log("Localization data loaded");
            _textData = _request.downloadHandler.text;
            
            ParseData();
        }

        private void ParseData()
        {
            _localeItems.Clear();
            
            var rows = _textData.Split('\n');
            foreach (string row in rows)
            {
                try
                {
                    var parts = row.Split('\t');
                    parts[1] = parts[1].TrimEnd('\r');
                    _localeItems.Add(new LocaleItem{Key = parts[0], Value = parts[1]});
                }
                catch (Exception e)
                {
                    Debug.LogError($"Can't parse row: {row}.\n {e}");
                }
            }
        }
    }

    [Serializable]
    public class LocaleItem
    {
        public string Key;
        public string Value;
    }
}