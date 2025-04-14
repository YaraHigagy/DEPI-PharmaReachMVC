namespace PharmaReachMVC.Utilities
{
    public class StringUtilities
    {
        public static string GetInitialsFromFullName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return string.Empty;

            var initials = fullName
                .Split(' ')  // Split by space
                .Where(word => !string.IsNullOrWhiteSpace(word))  // Remove empty strings
                .Select(word => word[0].ToString().ToUpper())  // Take the first letter and make it uppercase
                .Take(2)  // Limit to first 2 initials (for names with more than 2 words)
                .Aggregate((first, second) => first + second);  // Join the initials

            return initials;
        }
    }
}
