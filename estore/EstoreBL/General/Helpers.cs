using System.Text;
using System.Transactions;

namespace Estore.BL.General
{
    public static class Helpers
    {
        public static int? StringToIntDef(string str, int? def)
        {
            int value;
            if (int.TryParse(str, out value))
                return value;
            return def;
        }

        public static Guid? StringToGuidDef(string str)
        {
            Guid value;
            if (Guid.TryParse(str, out value))
                return value;
            return null;
        }

        public static TransactionScope CreateTransactionScope(int seconds = 6000)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, seconds),
                TransactionScopeAsyncFlowOption.Enabled
                );
        }

        public static string Translit(string str)
        {
            Dictionary<char, string> lat = new Dictionary<char, string>
                { {'а', "a"}, {'б', "b"}, {'в', "v"},{'г', "g" }, {'д', "d"}, {'е', "e"}, {'ё', "e"},
                {'ж', "gh"}, {'з', "z"}, {'и', "i"}, {'й', "y"}, {'к', "k"}, {'л',"l"}, {'м',"m"},
                {'н', "n"}, {'о',"o"}, {'п',"p"}, {'р',"r"}, {'с',"s"}, {'т',"t"}, {'у',"u"}, {'ф',"f"},
                {'х', "h"}, {'ц', "c"}, {'ч',"ch" }, {'ш',"sh"}, {'щ',"sch"}, {'э', "e"}, {'ю',"yu"},
                {'ы', "i"}, {'я', "ya"}};

            StringBuilder sb = new StringBuilder();
            foreach (char c in str.ToLowerInvariant())
            {
                if (c >= '0' && c <= '9' || c >= 'a' && c <= 'z')
                    sb.Append(c);
                else if (lat.ContainsKey(c))
                    sb.Append(lat[c]);
                else sb.Append("-");
            }
            return sb.ToString().Replace("--", "-");
        }
    }
}

