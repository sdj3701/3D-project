using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class Extensions 
{

    public static void Singleton<T>(this T target, ref T instance) where T : Object
    {
        if(instance && instance != target)
        {
            GameObject.Destroy(target);
        }
        else
        {
            instance = target;
        }
    }

    public static T GetNearset<T>(this List<T> targetList, Vector3 origin, float maxDistance) where T : MonoBehaviour
    {
        float nearesDistance = maxDistance;
        T nearesCharacter =null;

        foreach (T current in targetList)
        {
            if(current == null) continue;

            float currentDistance = (current.transform.position - origin).magnitude;

            if(currentDistance < nearesDistance)
            {
                nearesCharacter = current;

                nearesDistance = currentDistance;
            }
        }

        return nearesCharacter;
    }

    public static float ToHorizontalAngle(this Vector3 value) //해당 백터의 수평각도를 확인하기
    {
        value.y = 0; //수평만 확인하고 싶으니까 y축을 제거
        value.Normalize(); //길이를 1로 다시 회복

        return Mathf.Atan2(value.z, value.x) * Mathf.Rad2Deg;
    }

    public static Vector3 ToDirection(this float value)
    {
        return new Vector3(Mathf.Cos(value * Mathf.Deg2Rad), Mathf.Sin(value * Mathf.Deg2Rad)); //곱하면 360도 2파이가 됨
    }

    public static Vector3 ToDirection(this Vector2 angles)
    {
        Vector3 result;

        //각도를 라디안 값으로 조정
        angles.x *= Mathf.Deg2Rad;
        angles.y *= Mathf.Deg2Rad;

        result.x = Mathf.Cos(angles.x) * Mathf.Abs(Mathf.Cos(angles.y));
        result.z = Mathf.Sin(angles.x) * Mathf.Abs(Mathf.Cos(angles.y));
        result.y = Mathf.Sin(angles.y);

        return result;

    }

    public static Vector3 ToDirection(float horAngle, float verAngle)
    {
        return ToDirection(new Vector2(horAngle, verAngle));
    }

}
