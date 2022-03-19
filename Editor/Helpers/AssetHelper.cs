namespace SolidUtilities.Editor
{
    using System;
    using Editor;
    using JetBrains.Annotations;
    using SolidUtilities;
    using UnityEditor;
    
    public static class AssetHelper
    {
        /// <summary>
        /// Gets the GUID of an asset where the type is located.
        /// </summary>
        /// <param name="type">Type to search for in assets.</param>
        /// <param name="GUID">GUID of the asset where the type is located, or <c>null</c> if the asset was not found.</param>
        /// <param name="monoScript">MonoScript of the asset where the type is located, or <c>null</c> if the asset was not found.</param>
        /// <returns><c>true</c> if the asset with the specified type was found.</returns>
        [PublicAPI]
        [ContractAnnotation("=> true, GUID: notnull; => false, GUID: null")]
        public static bool GetAssetDetails(Type type, [CanBeNull] out string GUID, out MonoScript monoScript)
        {
            GUID = string.Empty;
            monoScript = null;

            if (type == null)
                return false;

            if (type.IsGenericType)
                type = type.GetGenericTypeDefinition();

            string typeNameWithoutSuffix = type.Name.StripGenericSuffix();

            foreach (string guid in AssetDatabase.FindAssets($"t:MonoScript {typeNameWithoutSuffix}"))
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<MonoScript>(assetPath);

                if (asset is null || asset.GetClassType(typeNameWithoutSuffix) != type)
                    continue;

                GUID = guid;
                monoScript = asset;
                return true;
            }

            return false;
        }

        [NotNull]
        public static string GetClassGUID(Type type) =>
            GetAssetDetails(type, out string guid, out MonoScript _) ? guid : string.Empty;

        public static MonoScript GetMonoScriptFromType(Type type)
        {
            GetAssetDetails(type, out string _, out MonoScript monoScript);
            return monoScript;
        }
    }
}