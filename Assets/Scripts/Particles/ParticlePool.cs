using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Particles
{
	public class ParticlePool : MonoBehaviour
	{
		[SerializeField] private Particle particlePrefab;
		[Min(1)]
		[SerializeField] private int startPoolSize = 5;

		private Queue<Particle> _pool;

		private void Awake()
		{
			_pool = new Queue<Particle>(startPoolSize);
		}

		private void Start()
		{
			for(int i = 0; i < startPoolSize; i++)
			{
				InstantiateAndPush();
			}
		}

		public Particle GetFreeParticle()
		{
			if(_pool.Count == 0)
			{
				InstantiateAndPush();
			}

			return _pool.Dequeue();
		}

		private void InstantiateAndPush()
		{
			Particle newParticle = Instantiate(particlePrefab, transform);
			newParticle.OnParticleSystemStoppedEvent += PushToQueuePool;
			PushToQueuePool(newParticle);
		}

		private void PushToQueuePool(Particle particle)
		{
			_pool.Enqueue(particle);
			particle.gameObject.SetActive(false);
		}
	}
}