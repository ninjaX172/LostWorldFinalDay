using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpriteShadowCastingGenerator : MonoBehaviour
{
    static readonly FieldInfo meshField = typeof(ShadowCaster2D).GetField("m_Mesh", BindingFlags.NonPublic | BindingFlags.Instance);
    static readonly FieldInfo shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.NonPublic | BindingFlags.Instance);
    static readonly FieldInfo shapePathHashField = typeof(ShadowCaster2D).GetField("m_ShapePathHash", BindingFlags.NonPublic | BindingFlags.Instance);
    static readonly MethodInfo generateShadowMeshMethod = typeof(ShadowCaster2D)
        .Assembly
        .GetType("UnityEngine.Rendering.Universal.ShadowUtility")
        .GetMethod("GenerateShadowMesh", BindingFlags.Public | BindingFlags.Static);
    
    public void GenerateShadowCasting(SpriteRenderer obj, Vector3[] paths)
    {
        ShadowCaster2D shadowCasterComponent = obj.gameObject.AddComponent<ShadowCaster2D>();
        shapePathField.SetValue(shadowCasterComponent, paths);
        shapePathHashField.SetValue(shadowCasterComponent, Random.Range(int.MinValue, int.MaxValue));
        meshField.SetValue(shadowCasterComponent, new Mesh());
        generateShadowMeshMethod.Invoke(shadowCasterComponent, new object[] { meshField.GetValue(shadowCasterComponent), shapePathField.GetValue(shadowCasterComponent)});
    }
    
    public void GenerateShadowCasting(SpriteRenderer obj, List<Vector2> tempPath)
    {
        var paths = ConvertVector2IntListToVector3Array(tempPath);
        ShadowCaster2D shadowCasterComponent = obj.gameObject.AddComponent<ShadowCaster2D>();
        shapePathField.SetValue(shadowCasterComponent, paths);
        shapePathHashField.SetValue(shadowCasterComponent, Random.Range(int.MinValue, int.MaxValue));
        meshField.SetValue(shadowCasterComponent, new Mesh());
        generateShadowMeshMethod.Invoke(shadowCasterComponent, new object[] { meshField.GetValue(shadowCasterComponent), shapePathField.GetValue(shadowCasterComponent)});
    }

    public static Vector3[] ConvertVector2IntListToVector3Array(List<Vector2> vector2IntList)
    {
        Vector3[] array = new Vector3[vector2IntList.Count];
        for (int i = 0; i < vector2IntList.Count; i++)
        {
            array[i] = new Vector3(vector2IntList[i].x, vector2IntList[i].y, 0);
        }

        return array;
    }
}
