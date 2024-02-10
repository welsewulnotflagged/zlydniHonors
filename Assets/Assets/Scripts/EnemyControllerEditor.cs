#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[CustomEditor(typeof(EnemyController))]
public class EnemyControllerEditor : Editor {
    private void OnSceneGUI()
    {
        EnemyController enemyController = (EnemyController)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemyController.transform.position, Vector3.up, Vector3.forward, 360, enemyController.DetectionRadius);

        Vector3 viewAngle01 = DirectionFromAngle(enemyController.transform.eulerAngles.y, -enemyController.DetectionAngle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(enemyController.transform.eulerAngles.y, enemyController.DetectionAngle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(enemyController.transform.position, enemyController.transform.position + viewAngle01 * enemyController.DetectionRadius);
        Handles.DrawLine(enemyController.transform.position, enemyController.transform.position + viewAngle02 * enemyController.DetectionRadius);

        if (enemyController.CanSeePlayer())
        {
            Handles.color = Color.green;
            Handles.DrawLine(enemyController.transform.position, enemyController.GetPlayerPosition());
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
#endif