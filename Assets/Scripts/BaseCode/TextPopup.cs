using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

namespace BaseCode
{
    public class TextPopup : MonoBehaviour
    {
        [SerializeField] private GameObject _popupDamageText;
        [SerializeField] private GameObject _popupHealText;
        [SerializeField] private GameObject _popupExpText;
        [SerializeField] private Transform _transmitter;
        [SerializeField] private float _popupRangeX = 0.5f;
        [SerializeField] private float _deltaMoveable = 1.6f;

        public void MakePopupText(string text, PopupTextType _popupTextType)
        {
            GameObject popupPrefab;

            switch (_popupTextType)
            {
                case PopupTextType.DamagePopupText:
                    popupPrefab = _popupDamageText;
                    break;
                case PopupTextType.HealPopupText:
                    popupPrefab = _popupHealText;
                    break;
                case PopupTextType.ExpPopupText:
                    popupPrefab = _popupExpText;
                    break;
                default:
                    popupPrefab = _popupDamageText;
                    break;
            }

            GameObject popupInstanse = Instantiate(popupPrefab, _transmitter.position, Quaternion.identity);
            popupInstanse.transform.SetParent(_transmitter);
            popupInstanse.TryGetComponent(out TMP_Text popupText);
            popupText.text = text;

            MovePopupText(popupInstanse).Forget();
        }

        private async UniTaskVoid MovePopupText(GameObject popupInstanse)
        {
            if (popupInstanse == null) return;

            Transform popupTransform = popupInstanse.transform;

            float posY = popupTransform.position.y;
            float posX = popupTransform.position.x;
            float posZ = popupTransform.position.z;
            posX = Random.Range(posX - _popupRangeX, posX + _popupRangeX);
            popupTransform.position = new Vector3(posX, posY, posZ);

            float scaleX = popupTransform.localScale.x;
            float scaleY = popupTransform.localScale.y;
            float scaleZ = popupTransform.localScale.z;

            float popupRandomMultiplier = Random.Range(0.6f, 1.3f);

            for (float y = posY; y < posY + _deltaMoveable * popupRandomMultiplier; y += Time.deltaTime * popupRandomMultiplier)
            {
                if (popupInstanse == null) return;

                popupTransform.position = new Vector3(popupTransform.position.x, y, popupTransform.position.z);

                float scaler = Mathf.Clamp01(Mathf.Sin((y - posY) * 3f)) * 1.2f;
                popupTransform.localScale = new Vector3(scaleX, scaleY, scaleZ) * scaler;

                await UniTask.Yield();
            }

            Destroy(popupInstanse);

            await UniTask.Yield();
        }
    }
}

