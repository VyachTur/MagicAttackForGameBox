using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using BaseCode;
using Enemies;
using Enemies.RedDragon;
using Player;

using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private bool _isSpawnerActive;

        #region  Pool
        [SerializeField] private int _poolCount = 50;
        [SerializeField] private bool AutoExpand = true;
        private ObjectPool<EnemyId> _enemiesPool;
        #endregion

        #region Prefabs
        [SerializeField] private EnemyId _slimePrefab;
        [SerializeField] private EnemyId _turtlePrefab;
        [SerializeField] private EnemyId _swordsmanPrefab;
        [SerializeField] private EnemyId _wariorPrefab;
        [SerializeField] private EnemyId _orcPrefab;
        [SerializeField] private EnemyId _lichPrefab;
        [SerializeField] private EnemyId _golemPrefab;

        // BOSS
        [SerializeField] private GameObject _redDragonPrefab;
        private bool _isBossSpawned;
        #endregion

        #region Spawner Settings
        [SerializeField] private float _everyEnemySpawnSeconds = 1f; // периодичность спавна врагов в одной волне (метод CycleInstantiate)
        [SerializeField] private int _countSlimeSpawnOneWay = 3;
        [SerializeField] private int _countTurtleSpawnOneWay = 2;
        [SerializeField] private int _countSwordsmanSpawnOneWay = 2;
        [SerializeField] private int _countWariorSpawnOneWay = 1;
        [SerializeField] private int _countOrcSpawnOneWay = 1;
        [SerializeField] private int _countLichSpawnOneWay = 1;
        [SerializeField] private int _countGolemSpawnOneWay = 1;
        #endregion

        public event Action OnAllEnemiesKillEvent;

        private float _timer;
        private Transform _playerTransform;
        private bool _isDragonDie;

        #region Bounds
        [SerializeField] private Transform _leftBound;
        [SerializeField] private Transform _rightBound;
        [SerializeField] private Transform _topBound;
        [SerializeField] private Transform _bottomBound;
        #endregion

        private void Awake()
        {
            _playerTransform = FindObjectOfType<PlayerHealth>().gameObject.transform;

            _enemiesPool = new ObjectPool<EnemyId>(_slimePrefab, _poolCount, transform);
            _enemiesPool.AutoExpand = AutoExpand;

            RedDragonDie.OnDragonDieEvent += OnDragonDie;
        }

        private void OnDragonDie() => _isDragonDie = true;

        private void Start() => _timer = 0f;

        private void Update()
        {
            if (_isSpawnerActive)
            {
                if (_isDragonDie && _enemiesPool.ActiveElementsCount <= 0)
                {
                    _playerTransform.GetComponent<PlayerWin>()?.Win();
                    OnAllEnemiesKillEvent?.Invoke();
                }

                if (_isBossSpawned) return;

                _timer += Time.deltaTime;

                if (_timer % 7f <= Time.deltaTime)
                {
                    // print("Slimes Spawn!");
                    CycleInstantiate(_slimePrefab, _countSlimeSpawnOneWay).Forget();
                }

                if (_timer > 4f * 60f && _timer % 9f <= Time.deltaTime)
                {
                    // print("Turtles Spawn!");
                    CycleInstantiate(_turtlePrefab, _countTurtleSpawnOneWay).Forget();
                }

                if (_timer > 10f * 60f && _timer % 9f <= Time.deltaTime)
                {
                    // print("Swordsman Spawn!");
                    CycleInstantiate(_swordsmanPrefab, _countSwordsmanSpawnOneWay).Forget();
                }

                if (_timer > 14f * 60f && _timer % 9f <= Time.deltaTime)
                {
                    // print("Orcs Spawn!");
                    CycleInstantiate(_orcPrefab, _countOrcSpawnOneWay).Forget();
                }

                if (_timer > 18f * 60f && _timer % 12f <= Time.deltaTime)
                {
                    // print("Wariors Spawn!");
                    CycleInstantiate(_wariorPrefab, _countWariorSpawnOneWay).Forget();
                }

                if (_timer > 23f * 60f && _timer % 14f <= Time.deltaTime)
                {
                    // print("Liches Spawn!");
                    CycleInstantiate(_lichPrefab, _countLichSpawnOneWay).Forget();
                }

                if (_timer > 30f * 60f && _timer % 30f <= Time.deltaTime)
                {
                    // print("Golems Spawn!");
                    CycleInstantiate(_golemPrefab, _countGolemSpawnOneWay).Forget();
                }

                // Инстанциируем босса (пещерного дракона)
                if (_timer > 48f * 60f && !_isBossSpawned)
                {
                    // print("Red Dragon Spawn!");
                    Instantiate(_redDragonPrefab, _playerTransform.position, Quaternion.AngleAxis(0f, Vector3.up));
                    _isBossSpawned = true;
                }
            }
        }

        // Волна врагов
        private async UniTaskVoid CycleInstantiate(EnemyId enemy, int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (_leftBound != null)
                {
                    float xPos = Random.Range(_leftBound.position.x + _leftBound.localScale.x / 2f + 4f, _rightBound.position.x - _leftBound.localScale.x / 2f - 4f);
                    float zPos = Random.Range(_bottomBound.position.z + _leftBound.localScale.x / 2f + 4f, _topBound.position.z - _leftBound.localScale.x / 2f - 4f);
                    Vector3 instantiatePos = new Vector3(xPos, 0f, zPos);

                    EnemyId currentEnemy = _enemiesPool.GetFreeElement(enemy);
                    currentEnemy.transform.position = instantiatePos;
                }

                await UniTask.Delay(TimeSpan.FromSeconds(_everyEnemySpawnSeconds));
            }
        }

        private void OnDestroy()
        {
            RedDragonDie.OnDragonDieEvent -= OnDragonDie;
        }
    }
}
