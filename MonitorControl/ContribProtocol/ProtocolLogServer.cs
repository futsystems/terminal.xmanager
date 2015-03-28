﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;



namespace TradingLib.Contirb.Protocol
{
    public class LogTaskEvent
    {

        /// <summary>
        /// 数据库ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 结算日
        /// </summary>
        public int Settleday { get; set; }

        /// <summary>
        /// 任务所属对象UUID
        /// </summary>
        public string UUID { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public QSEnumTaskType TaskType { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        public string TaskMemo { get; set; }

        /// <summary>
        /// 任务完成日期
        /// </summary>
        public int Date { get; set; }

        /// <summary>
        /// 任务完成时间
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// 任务运行结果
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 任务异常内容
        /// </summary>
        public string Exception { get; set; }
    }
}
