using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Musics")]
    public class ScriptableObjectMusics : ScriptableObject
    {
        [SerializeField] private List<AudioClip> audioClips;
        public IEnumerable<AudioClip> AudioClips => audioClips;
    }
}
