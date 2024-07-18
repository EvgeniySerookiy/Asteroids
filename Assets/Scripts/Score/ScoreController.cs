using System;
using System.Collections.Generic;
using Enemy.Asteroid.AsteroidBig;
using Enemy.Asteroid.AsteroidMedium;
using Enemy.Asteroid.AsteroidSmall;
using Enemy.UFO;
using UnityEngine;

namespace Score
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private ScoreView _scoreView;
        
        private Dictionary<Type, int> _scores = new Dictionary<Type, int>
        {
            { typeof(AsteroidBig), 5 },
            { typeof(AsteroidMedium), 15 },
            { typeof(AsteroidSmall), 30 },
            { typeof(UFO), 50 }
        };

        public void HandleBulletKill(Type enemyType)
        {
            if (_scores.TryGetValue(enemyType, out int points))
            {
                _scoreView.AddScore(points);
            }
        }
    }
}