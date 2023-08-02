using CodeBase.Infrastructure.Services.Audio;
using UnityEngine;

namespace CodeBase.Components
{
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