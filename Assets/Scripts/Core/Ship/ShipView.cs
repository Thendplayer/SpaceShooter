using System;
using Audio;
using Core.DisplaceableElement.Bullet;
using Flow;
using Objects;
using ServiceRegister;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Ship
{
    public class ShipView : ObjectView, ICollider
    {
        [SerializeField] private Transform[] _bulletSpawnPoints;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private SpriteRenderer _shield;
        [SerializeField, Layer] private int _layer;
        [SerializeField, Layer] private int _bulletLayer;
        
        public Action OnCollisionConfirmed;

        private Camera _mainCamera;

        public int Layer
        {
            get => _layer;
            set => _layer = value;
        }

        public float Radius { get; set; }

        public Vector3 Position
        {
            get => transform.position;
            private set
            {
                value.z = transform.position.z;
                transform.position = value;
            }
        }

        public Sprite Sprite
        {
            set => _spriteRenderer.sprite = value;
        }

        private Camera Camera => _mainCamera ??= ServiceLocator.Instance.GetService<Camera>();

        public void OnCollision(ICollider other)
        {
            OnCollisionConfirmed?.Invoke();
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }

        public void UpdatePosition()
        {
#if UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE
            if (Input.touchCount > 0)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId)) return;
                if (Input.touches[0].phase == TouchPhase.Ended) return;
                
                Position = Camera.ScreenToWorldPoint(Input.touches[0].position);
            }
#endif

#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;
                
                Position = Camera.ScreenToWorldPoint(Input.mousePosition);
            }
#endif
        }

        public void Shoot(BulletData data, int activeSpawnPoints, int bulletsPerSpawnPoint)
        {
            var repository = ServiceLocator.Instance.GetService<BulletRepository>();
            
            if (activeSpawnPoints > _bulletSpawnPoints.Length)
                activeSpawnPoints = _bulletSpawnPoints.Length;
            
            for (var spawnPoint = 0; spawnPoint < activeSpawnPoints; spawnPoint++)
            {
                var evenBullets = bulletsPerSpawnPoint % 2 == 0;
                var half = Mathf.FloorToInt(bulletsPerSpawnPoint * .5f);
                for (var bulletPosition = -half; bulletPosition <= half; bulletPosition++)
                {
                    if (evenBullets && bulletPosition == 0) continue;
                    
                    var bullet = repository.Create(data);
                    ((BulletView)bullet.View).Layer = _bulletLayer;
                    
                    var position = _bulletSpawnPoints[spawnPoint].position;
                    position.x += data.Diameter * bulletPosition * .5f;
                    
                    bullet.View.transform.position = position;
                    bullet.View.transform.up = activeSpawnPoints <= 1 ? transform.up : 
                        Quaternion.AngleAxis(-data.BulletAngleIncremental * bulletPosition, Vector3.forward) * transform.up;
                }
            }
            
            ServiceLocator.Instance.GetService<AudioService>().Play(AudioTrack.Shot);
        }

        public void ActiveShield(bool active) => _shield.enabled = active;
    }
}