using UnityEngine;
using TMPro;
using Player.AbilityInfo;
using System;
using UnityEngine.UI;
using Effects;

namespace Player.UI
{
    [RequireComponent(typeof(Image))]
    public class AreaAttackCardUpdater : MonoBehaviour
    {
        public event Action<EffectType, float, float, float> OnAreaAttackLevelUpEvent;

        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _levelNumberText;
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private TMP_Text _attackRadiusText;
        [SerializeField] private TMP_Text _reloadTimeText;
        [SerializeField] private TMP_Text _descriptionText;

        [SerializeField] private Button _cardButton;

        [SerializeField] private Button[] _otherButtons;

        private Image _areaAttackImage;

        private string _name;
        private bool _isCardLastUpdate;

        private void Awake()
        {
            _areaAttackImage = GetComponent<Image>();
            _isCardLastUpdate = true;
        }

        private void Start()
        {
            AreaAttackInfo.Reset();
        }

        private void OnEnable()
        {
            if (_isCardLastUpdate == true)
            {
                foreach (Button button in _otherButtons)
                {
                    if (button.interactable == true)
                    {
                        _cardButton.interactable = false;
                        _isCardLastUpdate = false;
                        return;
                    }
                }
            }

            if (AreaAttackInfo.CurrentAttackType == EffectType.FireAreaEffect
                    && AreaAttackInfo.MaxLevel <= AreaAttackInfo.LevelNumber)
            {
                _cardButton.interactable = false;
                return;
            }

            // Переход на новую абилку
            if (AreaAttackInfo.LevelNumber == AreaAttackInfo.MaxLevel)
            {
                if (AreaAttackInfo.CurrentAttackType == EffectType.FireAreaEffect)
                    return;

                EffectType _attackType = AreaAttackInfo.CurrentAttackType;
                AreaAttackInfo.CurrentAttackType = _attackType + 1;

                AreaAttackInfo.LevelNumber = 0;
            }

            switch (AreaAttackInfo.CurrentAttackType)
            {
                case EffectType.ArcaneAreaEffect:
                    // print("Arcane Effect!");
                    _areaAttackImage.sprite = Resources.Load<Sprite>("Sprites/Flame_Blue 1");
                    _name = "Магический взрыв";
                    _descriptionText.text = $"Действует по площади";
                    break;

                case EffectType.NatureAreaEffect:
                    // print("Nature Effect!");
                    _areaAttackImage.sprite = Resources.Load<Sprite>("Sprites/Flame_Green 1");
                    _name = "Природный взрыв";
                    _descriptionText.text = $"Действует по площади, подбрасывает врагов вверх";
                    break;

                case EffectType.FireAreaEffect:
                    // print("Fire Effect!");
                    _areaAttackImage.sprite = Resources.Load<Sprite>("Sprites/Flame_Original 1");
                    _name = "Огненный взрыв";
                    _descriptionText.text = $"Действует по площади, наносит доп. урон огнем";
                    break;
            }

            _cardButton.targetGraphic = _areaAttackImage;


            if (AreaAttackInfo.AttackRadius == 12f)
            {
                UpdateData(_name,
                        AreaAttackInfo.LevelNumber + 1f,
                        AreaAttackInfo.Damage + 2f,
                        AreaAttackInfo.AttackRadius,
                        AreaAttackInfo.ReloadTime);

                return;
            }

            UpdateData(_name,
                        AreaAttackInfo.LevelNumber + 1f,
                        AreaAttackInfo.Damage + 1f,
                        AreaAttackInfo.AttackRadius + 1f,
                        AreaAttackInfo.ReloadTime - 1f);
        }

        private void UpdateData(string name, float level, float damage, float radius, float time)
        {
            _nameText.text = $"{name}";
            _levelNumberText.text = $"Уровень {level}";
            _damageText.text = $"Урон: {damage}";
            _attackRadiusText.text = $"Радиус применения: {radius}";
            _reloadTimeText.text = $"Перезарядка: {time} сек";
        }

        public void LevelUp()
        {
            // Повышение уровня текущей абилки
            if (AreaAttackInfo.LevelNumber < AreaAttackInfo.MaxLevel)
            {
                AreaAttackInfo.LevelNumber += 1;
                AreaAttackInfo.Damage += 1f;

                if (AreaAttackInfo.AttackRadius == 12f)
                    AreaAttackInfo.Damage += 1f;

                if (AreaAttackInfo.AttackRadius < 12f)
                    AreaAttackInfo.AttackRadius += 1f;

                if (AreaAttackInfo.ReloadTime > 1f)
                    AreaAttackInfo.ReloadTime -= 1f;
            }


            UpdateData(_name,
                        AreaAttackInfo.LevelNumber,
                        AreaAttackInfo.Damage,
                        AreaAttackInfo.AttackRadius,
                        AreaAttackInfo.ReloadTime);

            OnAreaAttackLevelUpEvent?.Invoke(AreaAttackInfo.CurrentAttackType,
                                                AreaAttackInfo.Damage,
                                                AreaAttackInfo.AttackRadius,
                                                AreaAttackInfo.ReloadTime);

            _isCardLastUpdate = true;
        }

        private void OnDisable()
        {
            _cardButton.interactable = true;
        }
    }
}

