namespace SharedKernel.Formatters
{
    /// <summary>
    /// Provides utility methods to normalize string values across the application.
    /// </summary>
    /// <remarks>
    /// This class contains only technical string normalization logic.
    /// It must not include business rules or domain-specific behavior.
    /// </remarks>
    public static class StringNormalizer
    {
        /// <summary>
        /// Pure function that normalizes a string value.
        /// Returns an empty string when the input is null;
        /// otherwise, returns the input without leading or trailing whitespace.
        /// </summary>
        /// <param name="input">The string value to normalize.</param>
        /// <returns>The normalized string value.</returns>
        public static string Normalize(string? input)
        {
            return input is not null ? input.Trim() : string.Empty;
        }
    }
}