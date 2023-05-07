using Models;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Views;

namespace EventHandlers
{
    public class OnTanekkoBulletReleased : IInitializable
    {
        private readonly TanekkoModel _tanekkoModel;
        private readonly TanekkoBulletFactory _tanekkoBulletFactory;

        [Inject]
        public OnTanekkoBulletReleased(TanekkoModel tanekkoModel, TanekkoBulletFactory tanekkoBulletFactory)
        {
            _tanekkoModel = tanekkoModel;
            _tanekkoBulletFactory = tanekkoBulletFactory;
        }
    
        public void Initialize()
        {
            _tanekkoBulletFactory.OnReleasedBullet.Subscribe(_ =>
            {
                _tanekkoModel.IncreaseBulletCount();
            });
        }
    }
}