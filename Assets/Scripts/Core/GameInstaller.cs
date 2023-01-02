using System.Collections.Generic;
using Audio;
using Core.Animation;
using Core.DisplaceableElement.Bullet;
using Core.DisplaceableElement.Obstacle;
using Core.DisplaceableElement.Obstacle.Enemy;
using Core.DisplaceableElement.PowerUp;
using Core.Level;
using Core.Match;
using Core.Ship;
using Events;
using Objects;
using ServiceRegister;
using UnityEngine;

namespace Core
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private AudioService _audioService;
        
        [Header("Repositories")]
        [SerializeField] private LevelRepository _levelRepository;
        [SerializeField] private MatchRepository _matchRepository;
        [SerializeField] private AnimationRepository _animationRepository;
        [SerializeField] private ShipRepository _shipRepository;
        [SerializeField] private BulletRepository _bulletRepository;
        [SerializeField] private ObstacleRepository _obstacleRepository;
        [SerializeField] private EnemyRepository _enemyRepository;
        [SerializeField] private PowerUpRepository _powerUpRepository;

        private readonly List<ObjectRepresentation> _representations = new List<ObjectRepresentation>();
        
        private void Start()
        {
            var eventDispatcher = new EventDispatcher();
            ServiceLocator.Instance.RegisterService(eventDispatcher);
            ServiceLocator.Instance.RegisterService(_audioService);
            ServiceLocator.Instance.RegisterService(Camera.main);
            
            ServiceLocator.Instance.RegisterService(_animationRepository);
            ServiceLocator.Instance.RegisterService(_bulletRepository);
            ServiceLocator.Instance.RegisterService(_obstacleRepository);
            ServiceLocator.Instance.RegisterService(_enemyRepository);
            ServiceLocator.Instance.RegisterService(_shipRepository);
            ServiceLocator.Instance.RegisterService(_powerUpRepository);
            ServiceLocator.Instance.RegisterService(_levelRepository);
            ServiceLocator.Instance.RegisterService(_matchRepository);
            _representations.Add(_matchRepository.Create());
        }

        private void OnApplicationQuit()
        {
            foreach (var obj in _representations)
            {
                obj.Mediator.Dispose();
            }
        }
    }
}