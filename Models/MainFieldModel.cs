using System;
using System.Collections.Generic;
using Enums;
using UniRx;
using UnityEngine;

namespace Models
{
    public class MainFieldModel
    {
        private readonly ReactiveCollection<MainFieldType> _reactiveCollectionMainField = new();

        public IObservable<CollectionAddEvent<MainFieldType>> OnAddedMainField =>
            _reactiveCollectionMainField.ObserveAdd();

        public MainFieldType GetMainFieldTypeByIndex(int index)
        {
            if (index < 0 || index >= _reactiveCollectionMainField.Count)
            {
                return MainFieldType.None;
            }

            return _reactiveCollectionMainField[index];
        }

        public void BuildMainField()
        {
            var dictionaryNextMainFieldTypes = new Dictionary<MainFieldType, List<MainFieldType>>
            {
                {
                    MainFieldType.Desert, new List<MainFieldType>
                    {
                        MainFieldType.Ice,
                        MainFieldType.Poison
                    }
                },
                {
                    MainFieldType.Ice, new List<MainFieldType>
                    {
                        MainFieldType.Desert,
                        MainFieldType.Poison
                    }
                },
                {
                    MainFieldType.Poison, new List<MainFieldType>
                    {
                        MainFieldType.Desert,
                        MainFieldType.Ice
                    }
                }
            };
            var nextLengthValues = new List<int>
            {
                2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 5, 5, 6
            };

            var currentMainFieldType = MainFieldType.Desert;
            var length = 8;
            
            _reactiveCollectionMainField.Clear();
            
            for (var loopLimit = 0; loopLimit < int.MaxValue && _reactiveCollectionMainField.Count < 400; loopLimit++)
            {
                for (var i = 0; i < length; i++)
                {
                    _reactiveCollectionMainField.Add(currentMainFieldType);
                }

                if (dictionaryNextMainFieldTypes.TryGetValue(currentMainFieldType, out var nextMainFieldTypes))
                {
                    currentMainFieldType = nextMainFieldTypes[UnityEngine.Random.Range(0, nextMainFieldTypes.Count)];
                }

                length = nextLengthValues[UnityEngine.Random.Range(0, nextLengthValues.Count)];
            }
        }
    }
}
