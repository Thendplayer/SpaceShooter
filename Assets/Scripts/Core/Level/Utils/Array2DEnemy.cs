using System;
using Array2DEditor;
using Core.DisplaceableElement.Obstacle.Enemy;
using UnityEngine;

namespace Core.Level
{
    [Serializable]
    public class Array2DEnemy : Array2D<EnemyData>
    {
        [SerializeField]
        CellRowEnemy[] cells = new CellRowEnemy[Consts.defaultGridSize];

        protected override CellRow<EnemyData> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }
    
    [Serializable]
    public class CellRowEnemy : CellRow<EnemyData> { }
}