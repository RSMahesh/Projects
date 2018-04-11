using System;
using System.Reflection;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    abstract class BaseContextMenue
    {
       protected AppContext appContext;
        protected BaseContextMenue(AppContext appContext)
        {
            this.appContext = appContext;
        }

        protected void ExcuteHandlerWithWaitCursor(object sender, EventArgs e)
        {
            appContext.ChangeCursor(Cursors.WaitCursor);

            var menuItem = (MenuItem)sender;

            CallHandlerMethod(menuItem.Name, sender, e);

            appContext.ChangeCursor(Cursors.Default);

        }

        private void CallHandlerMethod(string methodName, object sender, EventArgs e)
        {
            MethodInfo methodInfo = this.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            methodInfo.Invoke(this, new[] { sender, e });
        }

    }
}