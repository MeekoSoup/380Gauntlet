using UnityEngine;

namespace Enemies
{
    public class EnemyAppearanceController : MonoBehaviour
    {
        public MeshRenderer[] meshRenderers;

        public void SetAlpha(float alpha)
        {
            foreach (var meshRenderer in meshRenderers)
            {
                foreach (var material in meshRenderer.materials)
                {
                    var color = material.color;
                    color.a = Mathf.Clamp01(alpha);
                    material.color = color;
                }
            }
        }

        public void SetColor(Color color)
        {
            foreach (var meshRenderer in meshRenderers)
            {
                foreach (var material in meshRenderer.materials)
                {
                    material.color = color;
                }
            }
        }
    }
}