using System.Linq;

namespace SiteCoreTechnicalAssignment
{
    public class PalindromeChecker
    {
        // Checks if string is palindrome while ignoring trash symbols and case
        // O(n) time complexity with O(1) space - uses no additional string creation
        public static bool IsPalindrome(string inputString, string trashSymbols)
        {
            // Input validation
            if (string.IsNullOrEmpty(inputString)) return false;
            if (trashSymbols == null) trashSymbols = string.Empty;

            // Two-pointer technique for single pass check
            int left = 0;
            int right = inputString.Length - 1;

            while (left <= right)
            {
                // Skip trash symbols from left
                while (left < right && trashSymbols.Contains(inputString[left]))
                    left++;
                // Skip trash symbols from right
                while (left < right && trashSymbols.Contains(inputString[right]))
                    right--;

                // Skip if all characters were trash
                if (left > right) return true;

                // Case-insensitive comparison
                if (char.ToLower(inputString[left]) != char.ToLower(inputString[right]))
                    return false;

                left++;
                right--;
            }
            return true;
        }
    }
}