namespace ShopFood.Domain.Utils
{
    /// <summary>
    /// Class with methos generics and utils
    /// </summary>
    public static class Utils
    {
        private const string Value = "\\";

        /// <summary>
        /// Method to combine server path with complement path user
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Path url completed</returns>
        public static string CombinePaths(string path)
        {
            var rootPath = Environment.CurrentDirectory;
            var parentDir = Directory.GetParent(rootPath.EndsWith(Value) ? rootPath : string.Concat(rootPath, Value));
            return Path.Combine(parentDir.Parent.FullName, path);
        }
    }
}
