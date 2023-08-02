using System;
using UnityEngine;

namespace CodeBase.Utils.Properties
{
    [Serializable]
    public class FloatPersistentProperty : PrefsPersistentProperty<float>
    {
        public FloatPersistentProperty(float defaultValue, string key) : base(defaultValue, key)
        {
            Init();
        }

        protected override void Write(float value)
        {
            PlayerPrefs.SetFloat(Key, value);
        }

        protected override float Read(float defaultValue)
        {
            return PlayerPrefs.GetFloat(Key, defaultValue);
        }
    }
}
