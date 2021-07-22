using UnityEditor;

namespace Arugula.Extensions.Editor
{
    internal class ReferenceResolverAssetPostprocessor : AssetPostprocessor
    {
        public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            ReferenceResolver.ResolveReferenceAttributes();
        }
    }
}