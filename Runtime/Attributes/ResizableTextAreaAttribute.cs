namespace SolidUtilities.Attributes
{
    using System;
    using JetBrains.Annotations;
    using UnityEngine;

    [AttributeUsage(AttributeTargets.Field)]
    [BaseTypeRequired(typeof(string))]
    public class ResizableTextAreaAttribute : PropertyAttribute { }
}