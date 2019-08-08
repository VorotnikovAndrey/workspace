using System.Collections.Generic;
using UnityEngine;

namespace UniverseTeam.Utils
{
    public static class Utils
    {
        public static string AddColorTag(Color color, string tag)
        {
            var result = $"<color=#{GetColorHexString(color)}>{tag}</color>";
            return result;
        }

        public static string AddColorTag(this string message, Color color)
        {
            var result = $"<color=#{GetColorHexString(color)}>{message}</color>";
            return result;
        }

        public static string AddColorTag(this object message, Color color)
        {
            var result = $"<color=#{GetColorHexString(color)}>{message}</color>";
            return result;
        }

        private static string GetColorHexString(Color color)
        {
            var colorString = string.Empty;
            colorString += ((int)(color.r * 255)).ToString("X02");
            colorString += ((int)(color.g * 255)).ToString("X02");
            colorString += ((int)(color.b * 255)).ToString("X02");
            return colorString;
        }

        public static T GetRandom<T>(this IList<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
    }
}
