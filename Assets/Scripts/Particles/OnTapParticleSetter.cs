using UnityEngine;

namespace Scripts.Particles
{
	public class OnTapParticleSetter : MonoBehaviour
	{
		[SerializeField] private new Camera camera;
		[SerializeField] private ParticlePool particlePool;

		private void Update()
		{
			if(!Input.GetMouseButtonDown(0) || Time.timeScale == 0)
			{
				return;
			}

			Vector3 worldPosition = camera.ScreenToWorldPoint(Input.mousePosition) +
									Vector3.forward * camera.nearClipPlane;
			particlePool.GetFreeParticle().EnableOnPosition(worldPosition);
		}
	}
}
