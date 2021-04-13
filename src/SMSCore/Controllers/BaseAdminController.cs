using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SMSCore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BaseAdminController:Controller
    {
        #region Utility Methods
        [NonAction]
        protected bool IsValidateId(int id)
        {
            return id != 0;
        }

        [NonAction]
        protected string TrimStringContent(string str)
        {
            if (str != null)
                return str.Trim();
            else
                return string.Empty;
        }

        [NonAction]
        protected string DecodeHtml(string htmlEncodedString)
        {
            return WebUtility.HtmlDecode(htmlEncodedString);
        }

        [NonAction]
        protected void AllowContinueEditing(bool continueEditing)
        {
            TempData["continueEditing"] = continueEditing;
        }

        [NonAction]
        protected bool IsContinueEditingAllowed()
        {
            return TempData["continueEditing"] != null;
        }

        #endregion
    }
}
