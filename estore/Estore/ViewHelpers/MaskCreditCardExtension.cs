using Microsoft.AspNetCore.Mvc.Rendering;

namespace Estore.ViewHelpers;

public static class MaskCreditCardExtension {
    public static string MaskCreditCard(this IHtmlHelper helper, string ccnumber) {
        if (String.IsNullOrEmpty(ccnumber) || ccnumber.Length < 15) {
            return "*********";
        }
        
        return "**** **** **** " + ccnumber.Substring(ccnumber.Length - 4);
    }
}