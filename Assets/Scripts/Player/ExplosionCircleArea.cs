using UnityEngine;
using Player.AbilityInfo;

namespace Player
{
    public class ExplosionCircleArea : MonoBehaviour
    {
        [SerializeField] private LineRenderer _circleRenderer;
        public float Radius = AreaAttackInfo.AttackRadius;
        private const int Steps = 100;

        public void DrawArea() =>
            DrawCircle(Steps, Radius);

        private void DrawCircle(int steps, float radius)
        {
            _circleRenderer.positionCount = steps;

            for (int step = 0; step < steps; step++)
            {
                float circlestepProgress = (float)step / steps;

                float radian = circlestepProgress * 2 * Mathf.PI;

                float xScaled = Mathf.Cos(radian);
                float zScaled = Mathf.Sin(radian);

                float x = xScaled * radius;
                float z = zScaled * radius;

                Vector3 position = new Vector3(x, 0f, z);

                _circleRenderer.SetPosition(step, position);
            }
        }
    }
}
