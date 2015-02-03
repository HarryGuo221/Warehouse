using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Base
{
    class MenuCfgChild
    {
        private int sysId;
        /// <summary>
        /// 系统编号
        /// </summary>
        public int SysId
        {
            get { return sysId; }
            set { sysId = value; }
        }

        private int mainMenuId;
        /// <summary>
        /// 主菜单编号
        /// </summary>
        public int MainMenuId
        {
            get { return mainMenuId; }
            set { mainMenuId = value; }
        }

        private string subMenu;
        /// <summary>
        /// 子菜单项
        /// </summary>
        public string SubMenu
        {
            get { return subMenu; }
            set { subMenu = value; }
        }
        private string shortCut;
        /// <summary>
        /// 快捷键
        /// </summary>
        public string ShortCut
        {
            get { return shortCut; }
            set { shortCut = value; }
        }
        private int orderIndex;
        /// <summary>
        /// 顺序号
        /// </summary>
        public int OrderIndex
        {
            get { return orderIndex; }
            set { orderIndex = value; }
        }
    }
}
