using Array2DEditor;
using Core.DisplaceableElement.Obstacle.Enemy;
using Core.Level;
using UnityEditor;

namespace Core.Editor
{
    [CustomPropertyDrawer(typeof(Array2DEnemy))]
    public class Array2DEnemyDrawer : Array2DObjectDrawer<EnemyData> { }
}