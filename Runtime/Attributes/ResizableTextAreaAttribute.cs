namespace SolidUtilities
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Draws text in a wide field with lines wrap and auto-adjusting height.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ResizableTextAreaAttribute : PropertyAttribute { }
}