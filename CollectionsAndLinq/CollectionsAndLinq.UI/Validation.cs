using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CollectionsAndLinq.UI
{
    public class Validation
    {
        public static bool IsValidNumber(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return new Regex(@"^\d+$").IsMatch(id);
            }
            return false;
        }

        public static bool IsValidDateOfBirth(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                return new Regex(@"(19|20)\d{2}$").IsMatch(date);
            }
            return false;
        }

        public static bool IsValidMenuItem(string item, int count)
        {
            bool isValidItem = false;
            if (int.TryParse(item, out int result))
            {
                isValidItem = result > 0 && result <= count;
            }

            return isValidItem;
        }
    }
}
