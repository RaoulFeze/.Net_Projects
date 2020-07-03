using COECommon.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Marigold.UserControls
{
    public partial class InfoUserControl : System.Web.UI.UserControl
    {
        #region String Constatnts
        private const string STR_ALERT_PRIMARY = "alert alert-primary";
        private const string STR_ALERT_SUCCESS = "alert alert-success";
        private const string STR_ALERT_DANGER = "alert alert-danger";
        private const string STR_ALERT_INFO = "alert alert-info";

        private const string STR_ICON_SUCCESS = "glyphicon glyphicon-ok-circle";
        private const string STR_ICON_DANGER = "glyphicon glyphicon-minus-sign";
        private const string STR_ICON_INFO = "glyphicon glyphicon-info-sign";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            InfoPanel.Visible = false;
        }

        /// <summary>
        /// Processes a request within a try/catch block
        /// </summary>
        /// <param name="MyDelegate">A delegate method called within a try/cath block</param>
        public void TryRun(ProcessRequest MyDelegate)
        {
            TryCatch(MyDelegate);
        }

        /// <summary>
        /// Processes a request through a delegate within a try/catch block
        /// </summary>
        /// <param name="Mydelegate">A delegate method called within try/catch block</param>
        /// <param name="message"> Message to be displayed if MyDelegate executed without raising an exception</param>
        public void TryRun(ProcessRequest Mydelegate, string message)
        {
            if (TryCatch(Mydelegate))
                ShowInfo(message, STR_ICON_SUCCESS, STR_ALERT_SUCCESS);
        }
        /// <summary>
        /// Processes a request through a delegate in a try/catch block
        /// </summary>
        /// <param name="MyDelegate"></param>
        /// <returns>returns true if MyDelegate executed without exception and false otherwise</returns>
        private bool TryCatch(ProcessRequest MyDelegate)
        {
            try
            {
                MyDelegate();
                return true;
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }
            return false;
        }

        /// <summary>
        /// Handles all Exceptions
        /// </summary>
        /// <param name="ex"> general exception</param>
        private void HandleException(Exception ex)
        {
            ShowInfo(ex.Message, STR_ICON_DANGER, STR_ALERT_DANGER);
        }
        /// <summary>
        /// Display a message that indicates what the user is currently doing
        /// </summary>
        /// <param name="message"></param>
        public void ShowInfo(string message)
        {
            ShowInfo(message, STR_ICON_INFO, STR_ALERT_PRIMARY);
        }

        /// <summary>
        /// Displays the message the panel.
        /// </summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="infoIcon">Icon </param>
        /// <param name="cssClass">CssClass to be applied to the panel</param>
        private void ShowInfo(string message, string infoIcon, string cssClass)
        {
            Message.Text = message;
            InfoIcon.CssClass = infoIcon;
            InfoPanel.CssClass = cssClass;
            InfoPanel.Visible = true;
        }
    }
}