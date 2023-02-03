using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    Waypoint Waypoint => target as Waypoint;

    private void OnSceneGUI()
    {
        Handles.color = Color.cyan;
        for(int i=0;i<Waypoint.Points.Length;i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 currentWaypointPoint = Waypoint.CurrentPosition + Waypoint.Points[i];
            Vector3 newWayPointPoint = Handles.FreeMoveHandle(currentWaypointPoint, Quaternion.identity, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 16;
            textStyle.normal.textColor = Color.white;
            Vector3 textAlign = Vector3.down * 0.35f + Vector3.right * 0.35f;
            Handles.Label(Waypoint.CurrentPosition + Waypoint.Points[i] + textAlign, $"{i + 1}", textStyle);
            EditorGUI.EndChangeCheck();

            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Move Handle");
                Waypoint.Points[i] = newWayPointPoint - Waypoint.CurrentPosition;
            }
        }

    }
}
