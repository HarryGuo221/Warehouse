using System;
using System.Collections.Generic;
using System.Linq; 
using System.Windows.Forms;

namespace Warehouse
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //登录窗口
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();

            string userId = loginForm.getUserId();
            string workMonth = loginForm.getCurWorkMonth();
            if (loginForm.DialogResult == DialogResult.OK)
            {
                //登录成功
                Application.Run(new MainForm(userId,workMonth));
            }           

            //Application.Run(new MainForm("liuyahui"));
        }
    }
}
