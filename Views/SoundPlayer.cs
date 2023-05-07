using System;
using System.Collections.Generic;
using Enums;
using ScriptableObjects;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private ScriptableObjectSounds scriptableObjectSounds;

        private readonly Dictionary<SoundName, AudioClip> _dictionarySounds = new();
        private void Reset()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Awake()
        {
            foreach (var audioClip in scriptableObjectSounds.AudioClips)
            {
                if (Enum.TryParse(audioClip.name, out SoundName soundName))
                {
                    if (_dictionarySounds.ContainsKey(soundName))
                    {
                        Debug.LogWarning("すでに登録されているサウンド名があります" + audioClip.name);
                    }
                    else
                    {
                        _dictionarySounds.Add(soundName, audioClip);
                    }
                }
                else
                {
                    Debug.LogWarning("Enum化できないAudioClipが含まれています。Sound名とEnum名が一致しているか確認してください" + audioClip.name);
                }
            }
        }

        public void PlaySound(params SoundName[] soundNames)
        {
            var soundName = soundNames[UnityEngine.Random.Range(0, soundNames.Length)];
            if (_dictionarySounds.TryGetValue(soundName, out var audioClip))
            {
                audioSource.PlayOneShot(audioClip);
            }
            else
            {
                Debug.LogWarning("サウンド名が登録されていません" + soundName);
            }
        }
    }
}
