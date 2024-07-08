using System;
using UnityEngine;

namespace Scripts.Particles
{
	[RequireComponent(typeof(ParticleSystem))]
	public class Particle : MonoBehaviour
	{
		private ParticleSystem _particleSystem;

		public event Action<Particle> OnParticleSystemStoppedEvent;

		private void Awake()
		{
			_particleSystem = GetComponent<ParticleSystem>();
		}

		private void OnParticleSystemStopped()
		{
			gameObject.SetActive(false);
			OnParticleSystemStoppedEvent?.Invoke(this);
		}

		public void EnableOnPosition(Vector3 position)
		{
			transform.position = position;
			gameObject.SetActive(true);
			_particleSystem.Play();
		}
	}
}
