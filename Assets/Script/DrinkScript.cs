using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Script
{
    public class DrinkScript : MonoBehaviour
    {
        [SerializeField]
        private DrinkColorScript _colorScript;

        [SerializeField]
        private ParticleSystem _particleSystem;

        private AudioSource audio;

        private void Start()
        {
            audio = gameObject.GetComponent<AudioSource>();
        }

        public Color getCurrentColor()
        {
            return _colorScript.getCurrentColor();
        }

        public void PlayParticle()
        {
            if(_particleSystem != null)
                _particleSystem.Play();
            audio.Stop();
            audio.Play();
        }
        
        public void AddColor(Color c)
        {
            _colorScript.AddColorFade(c);
        }

        public void SetColorFade(Color c)
        {
            _colorScript.InitFade(c);
        }

        public void SetColor(Color c)
        {
            _colorScript.SetColor(c);
        }
    }
}