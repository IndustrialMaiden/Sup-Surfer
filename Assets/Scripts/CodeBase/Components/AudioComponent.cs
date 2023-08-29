using CodeBase.Infrastructure.Services.Audio;
using UnityEngine;

namespace CodeBase.Components
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioComponent : MonoBehaviour
    {
        [SerializeField] public AudioSource Source;
        [SerializeField] public AudioData[] Sounds;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}