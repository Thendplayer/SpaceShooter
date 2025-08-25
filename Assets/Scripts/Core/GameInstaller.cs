using System.Collections.Generic;
using SpaceShooter.Audio;
using SpaceShooter.Core.Animation;
using SpaceShooter.Core.DisplaceableElement.Bullet;
using SpaceShooter.Core.DisplaceableElement.Obstacle;
using SpaceShooter.Core.DisplaceableElement.Obstacle.Enemy;
using SpaceShooter.Core.DisplaceableElement.PowerUp;
using SpaceShooter.Core.Level;
using SpaceShooter.Core.Match;
using SpaceShooter.Core.Ship;
using SpaceShooter.Events;
using SpaceShooter.Objects;
using SpaceShooter.ServiceRegister;
using UnityEngine;

namespace SpaceShooter.Core
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