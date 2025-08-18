#nullable enable

namespace Game
{
    using UnityEngine;

    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : SingletonMonoBehaviour<SoundManager>
    {
        [SerializeReference]
        [ResolveComponent]
        private AudioSource audioSource = null!;

        [field: Header("Audio clips")]

        [field: SerializeReference]
        private AudioClip clickAudioClip = null!;

        protected override SoundManager LocalInstance => this;

        public void PlayClickSound()
        {
            this.audioSource.PlayOneShot(this.clickAudioClip);
        }
    }
}
