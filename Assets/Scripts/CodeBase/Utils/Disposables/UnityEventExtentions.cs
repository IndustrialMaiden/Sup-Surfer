using System;
using UnityEngine.Events;

namespace CodeBase.Utils.Disposables
{
    public static class UnityEventExtentions
    {
        public static IDisposable Subscribe(this UnityEvent unityEvent, UnityAction call)
        {
            unityEvent.AddListener(call);
            return new ActionDisposables(() => unityEvent.RemoveListener(call));
        }

        public static IDisposable Subscribe<TType>(this UnityEvent<TType> unityEvent, UnityAction<TType> call)
        {
            unityEvent.AddListener(call);
            return new ActionDisposables(() => unityEvent.RemoveListener(call));
        }
    }
}
