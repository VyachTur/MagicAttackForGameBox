using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Player.UI;
using Player.AbilityInfo;
using Player;

namespace Effects
{
    public class AreaEffectInstantiate : MonoBehaviour
    {
        [SerializeField] private GameObject[] _effectGameObjects;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private ExplosionCircleArea _explosionCircleArea;
        [SerializeField] private float _timeToEffectReload = AreaAttackInfo.ReloadTime;
        [SerializeField] private float _damage = AreaAttackInfo.Damage;
        [SerializeField] private EffectType _currentEffectType = AreaAttackInfo.CurrentAttackType;
        [SerializeField] private bool _isActiveAttackAreaEffect;
        [SerializeField] private Image _areaEffectImage;
        [SerializeField] private AreaAttackCardUpdater _areaAttackCardUpdater;
        public event Action OnEffectReloadEvent;
        public float TimeToEffectReload => _timeToEffectReload;

        private Vector3? _effectPosition;
        private bool _isAreaEffectReload;

        private void Awake()
        {
            _areaAttackCardUpdater.OnAreaAttackLevelUpEvent += OnSetAreaAttackData;
        }

        private void OnSetAreaAttackData(EffectType type, float damage, float radius, float time)
        {
            _isActiveAttackAreaEffect = true;
            _areaEffectImage.enabled = true;
            _currentEffectType = type;
            _damage = damage;
            _explosionCircleArea.Radius = radius;
            _timeToEffectReload = time;
        }

        public void BeginEffect()
        {
            if (_isActiveAttackAreaEffect && !_isAreaEffectReload)
            {
                _explosionCircleArea.gameObject.SetActive(true);
                _explosionCircleArea.DrawArea();
            }
        }

        public void EndEffect()
        {
            if (_isActiveAttackAreaEffect && !_isAreaEffectReload)
            {
                _explosionCircleArea.gameObject.SetActive(false);

                Vector3 mousePosition = Mouse.current.position.ReadValue();
                _effectPosition = GetMousePositionOnGround(mousePosition) + new Vector3(0f, 0.001f, 0f);

                EffectMakeOnGround();
            }
        }

        private async UniTask EffectReload() 
        {
            OnEffectReloadEvent?.Invoke();

            _isAreaEffectReload = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_timeToEffectReload));
            _isAreaEffectReload = false;
        }

        private Vector3? GetMousePositionOnGround(Vector3 position) =>
            Physics.Raycast(Camera.main.ScreenPointToRay(position), out RaycastHit hit, 100f, _layerMask) ?
                            hit.point
                            : null;

        private void EffectMakeOnGround()
        {
            if (_effectPosition != null)
            {
                float radiusToEffect = Vector3.Distance(transform.position, (Vector3)_effectPosition);

                if (radiusToEffect < _explosionCircleArea.Radius)
                {                    
                    GameObject areaAttackObject = Instantiate(_effectGameObjects[(int)_currentEffectType],
                                                                (Vector3)_effectPosition,
                                                                Quaternion.identity);

                    areaAttackObject.GetComponent<AreaEffectDamage>().AreaAttackDamage = _damage;


                    Destroy(areaAttackObject, 5f);

                    EffectReload().Forget();
                }  
            }
        }

        private void OnDestroy()
        {
            _areaAttackCardUpdater.OnAreaAttackLevelUpEvent -= OnSetAreaAttackData;
        }
    }
}
