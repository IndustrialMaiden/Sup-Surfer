using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Audio
{
    [Serializable]
    public struct AudioData
    {
        [SerializeField] private string _id;
        [SerializeField] private AudioClip _clip;

        public string Id => _id;
        public AudioClip Clip => _clip;
    }
}