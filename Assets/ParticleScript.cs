using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    private ParticleSystem ps;
    private PlayerScript _player;
    public float hSliderValue = 1.0F;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        _player = FindObjectOfType<PlayerScript>();
    }

    void Update()
    {
        var main = ps.main;
        main.simulationSpeed = hSliderValue + (_player.speed / 10);
        
    }
}
