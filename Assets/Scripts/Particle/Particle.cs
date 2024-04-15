using UnityEngine;

public class Particle : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 acceleration;
    public float lifetime;
    public Color color;

    // New properties for charge and mass
    public float charge; // Magnitude of the charge
    public float mass;

    // Constructor for creating particles with specified properties
    public Particle(Vector3 initialVelocity, float initialLifetime, Color initialColor, float initialCharge, float initialMass)
    {
        velocity = initialVelocity;
        acceleration = Vector3.zero; // You may set an initial acceleration if needed
        lifetime = initialLifetime;
        color = initialColor;
        charge = initialCharge;
        mass = initialMass;
    }
}
