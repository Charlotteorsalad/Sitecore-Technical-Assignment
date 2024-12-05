using System;
using System.Linq;

namespace SiteCoreTechnicalAssignment
{
    public class PalindromeChecker
    {
        public static bool IsPalindrome(string inputString, string trashSymbols)
        {
            // Validation
            if (string.IsNullOrEmpty(inputString)) return false;
            if (trashSymbols == null) trashSymbols = string.Empty;

            int left = 0;
            int right = inputString.Length - 1;

            while (left <= right)
            {
                while (left < right && trashSymbols.Contains(inputString[left]))
                    left++;
                while (left < right && trashSymbols.Contains(inputString[right]))
                    right--;

                // Skip if all characters were trash
                if (left > right) return true;

                if (char.ToLower(inputString[left]) != char.ToLower(inputString[right]))
                    return false;

                left++;
                right--;
            }
            return true;
        }
    }
}