using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioService : MonoBehaviour
    {
        [Serializable]
        public struct AudioFile
        {
            public string Name;
            public AudioClip Clip;
        }

        public static bool Mute = false;
        public List<AudioFile> AudioTracks = new List<AudioFile>();

        private AudioSource _audioSource;
        private Dictionary<string, AudioClip> _audioRegister;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            _audioRegister = new Dictionary<string, AudioClip>();
            foreach(var track in AudioTracks)
            {
                _audioRegister.Add(track.Name, track.Clip);
            }
        }

        public void Play(AudioTrack track)
        {
            if (Mute) return;
            
            if (_audioRegister.TryGetValue(track.ToString(), out var clip))
            {
                _audioSource.PlayOneShot(clip);
            }
        }
    }
}