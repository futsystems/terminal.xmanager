using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TradingLib.API
{
    /// <summary>
    /// 管理端控件接口
    /// 用于添加到Workbench并进行显示
    /// </summary>
    public interface IMoniterControl
    {
        /// <summary>
        /// MC 控件名称
        /// </summary>
        //string UUID { get; }

        /// <summary>
        /// 用于显示在工具栏的过滤按钮
        /// </summary>
        Control FilterToolBar { get; set; }
    }
}
