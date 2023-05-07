using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Sounds")]
    public class ScriptableObjectSounds : ScriptableObject
    {
        [SerializeField] private List<AudioClip> audioClips;
        public IEnumerable<AudioClip> AudioClips => audioClips;
    }
}
