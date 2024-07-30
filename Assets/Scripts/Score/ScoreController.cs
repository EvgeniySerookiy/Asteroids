using System;
using System.Collections.Generic;
using Enemy.Asteroid.AsteroidsBig;
using Enemy.Asteroid.AsteroidsMedium;
using Enemy.Asteroid.AsteroidsSmall;
using Enemy.UFO;

namespace Score
{
    public class ScoreController
    {
        private ScoreView _scoreView;
        public ScoreController(ScoreView scoreView)
        {
            _scoreView = scoreView;
        }
        
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