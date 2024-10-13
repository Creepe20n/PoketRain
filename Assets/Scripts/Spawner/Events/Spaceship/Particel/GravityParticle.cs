using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityParticle : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    public float gravitationalPull = 5.0f;  // Adjust the strength of the pull
    public float damping = 0.9f;  // Adjust to make particles oscillate

    void Start() {
        particleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
    }

    void LateUpdate() {
        int numParticlesAlive = particleSystem.GetParticles(particles);

        for(int i = 0;i < numParticlesAlive;i++) {
            Vector3 directionToCenter = (Vector3.zero - particles[i].position).normalized;
            Vector3 velocity = particles[i].velocity;

            // Apply gravitational pull
            velocity += directionToCenter * gravitationalPull * Time.deltaTime;

            // Apply damping to create the overshooting effect
            velocity *= damping;

            particles[i].velocity = velocity;
        }

        particleSystem.SetParticles(particles,numParticlesAlive);
    }
}
