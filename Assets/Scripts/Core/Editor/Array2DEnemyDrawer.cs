using Array2DEditor;
using SpaceShooter.Core.DisplaceableElement.Obstacle.Enemy;
using SpaceShooter.Core.Level;
using UnityEditor;

namespace SpaceShooter.Core.Editor
{
    [CustomPropertyDrawer(typeof(Array2DEnemy))]
    public class Array2DEnemyDrawer : Array2DObjectDrawer<EnemyData> { }
}