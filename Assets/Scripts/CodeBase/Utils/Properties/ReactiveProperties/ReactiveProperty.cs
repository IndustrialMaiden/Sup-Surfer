using System;

namespace CodeBase.Utils.Properties.ReactiveProperties
{
    [Serializable]
    public class ReactiveProperty<TPropertyType>
    {
        public event Action<TPropertyType> OnChanged;

        private TPropertyType _value;
        public TPropertyType Value
        {
            get => _value;
            set
            {
                if(_value?.Equals(value) ?? false) return;
                _value = value;
                OnChanged?.Invoke(_value);
            }
        }
    }
}