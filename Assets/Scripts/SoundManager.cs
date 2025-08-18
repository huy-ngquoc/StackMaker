﻿#nullable enable

namespace Game
{
    using UnityEngine;

    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : SingletonMonoBehaviour<SoundManager>
    {
        [SerializeReference]
        [ResolveComponent]
        private AudioSource audioSource = null!;

        protected override SoundManager LocalInstance => this;

        public void PlaySound(AudioClip audioClip)
        {
            this.audioSource.PlayOneShot(audioClip);
        }
    }
}
