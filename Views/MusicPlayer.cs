using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using ScriptableObjects;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private ScriptableObjectMusics scriptableObjectMusics;
        [SerializeField] private float durationMusicStart;
        private readonly Dictionary<MusicName, AudioClip> _musics = new();
        
        private void Reset()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
        }

        private void Awake()
        {
            foreach (var audioClip in scriptableObjectMusics.AudioClips)
            {
                if (Enum.TryParse(audioClip.name, out MusicName musicName))
                {
                    if (_musics.ContainsKey(musicName))
                    {
                        Debug.LogWarning("BGMが重複しています。：" + musicName);
                    }
                    else
                    {
                        _musics.Add(musicName, audioClip);
                    }
                }
                else
                {
                    Debug.LogWarning("Enumパースに失敗し、BGMの登録に失敗しました。：" + audioClip.name);
                }
            }
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(durationMusicStart);
            if (audioSource.isPlaying)
            {
                yield break;
            }

            audioSource.time = 0.0f;
            audioSource.Play();
        }
        
        public void ChangeMusic(MusicName musicName)
        {
            if (_musics.TryGetValue(musicName, out var audioClip))
            {
                if (audioSource.clip == audioClip)
                {
                    return;
                }
                
                audioSource.clip = audioClip;
                audioSource.time = 0.0f;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("BGMが登録されていません。：" + musicName);
            }
        }
    }
}
