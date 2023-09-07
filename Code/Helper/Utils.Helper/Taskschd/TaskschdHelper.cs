using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Helper.TXT;
using TaskScheduler;

namespace Utils.Helper.Taskschd
{
    /// <summary>
    /// 任务计划帮助类
    /// 创建日期:2017年09月18日
    /// </summary>
    public class TaskschdHelper
    {
        /// <summary>
        /// 创建任务计划
        /// </summary>
        /// <param name="strCreator">作者</param>
        /// <param name="strTaskName">任务名称</param>
        /// <param name="strPath">任务计划路径</param>
        /// <param name="strInterval">任务触发时间(PT1M:1分钟,PT1H30M:90分钟)</param>
        /// <param name="strStartBoundary">任务开始时间(yyyy-MM-ddTHH:mm:ss)</param>
        /// <param name="strDescription">任务描述</param>
        /// <returns>任务状态</returns>
        public static bool CreateTaskschd(string strCreator, string strTaskName, string strPath, string strInterval, string strStartBoundary, string strDescription)
        {
            try
            {
                if (IsExists(strTaskName))
                {
                    DeleteTaskschd(strTaskName);
                }

                // new scheduler
                TaskSchedulerClass scheduler = new TaskSchedulerClass();
                // pc-name/ip,username,domain,password
                scheduler.Connect(null, null, null, null);
                // get scheduler folder
                ITaskFolder folder = scheduler.GetFolder("\\");

                // set base attr 
                ITaskDefinition task = scheduler.NewTask(0);
                // creator
                task.RegistrationInfo.Author = strCreator;
                // description
                task.RegistrationInfo.Description = strDescription;

                // set trigger  (IDailyTrigger ITimeTrigger)
                ITimeTrigger tt = (ITimeTrigger)task.Triggers.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_TIME);
                // format PT1H1M==1小时1分钟 设置的值最终都会转成分钟加入到触发器
                tt.Repetition.Interval = strInterval;
                // start time
                tt.StartBoundary = strStartBoundary;

                // set action
                IExecAction action = (IExecAction)task.Actions.Create(_TASK_ACTION_TYPE.TASK_ACTION_EXEC);
                // 计划任务调用的程序路径
                action.Path = strPath;
                // 运行任务时间超时停止任务吗? PTOS 不开启超时
                task.Settings.ExecutionTimeLimit = "PT0S";
                // 只有在交流电源下才执行
                task.Settings.DisallowStartIfOnBatteries = false;
                task.Settings.RunOnlyIfIdle = false;//仅当计算机空闲下才执行

                IRegisteredTask regTask = folder.RegisterTaskDefinition(strTaskName, task,
                                                                    (int)_TASK_CREATION.TASK_CREATE, null, //user
                                                                    null, //password
                                                                    _TASK_LOGON_TYPE.TASK_LOGON_INTERACTIVE_TOKEN,
                                                                    "");
                IRunningTask runTask = regTask.Run(null);
                // return runTask.State;
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                // return _TASK_STATE.TASK_STATE_UNKNOWN;
                return false;
            }
        }

        /// <summary>
        /// 删除任务计划
        /// </summary>
        /// <param name="strTaskName">任务计划名称</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool DeleteTaskschd(string strTaskName)
        {
            try
            {
                TaskSchedulerClass taskScheduler = new TaskSchedulerClass();
                taskScheduler.Connect(null, null, null, null);
                ITaskFolder taskFolder = taskScheduler.GetFolder("\\");
                taskFolder.DeleteTask(strTaskName, 0);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 获得所有任务计划
        /// </summary>
        /// <returns>所有任务计划</returns>
        public static IRegisteredTaskCollection GetAllTaskschd()
        {
            try
            {
                TaskSchedulerClass taskScheduler = new TaskSchedulerClass();
                taskScheduler.Connect(null, null, null, null);
                ITaskFolder taskFolder = taskScheduler.GetFolder("\\");
                IRegisteredTaskCollection tasks_exists = taskFolder.GetTasks(1);
                return tasks_exists;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 任务计划是否存在
        /// </summary>
        /// <param name="strTaskName">任务计划名称</param>
        /// <returns></returns>
        public static bool IsExists(string strTaskName)
        {
            try
            {
                bool isExists = false;
                IRegisteredTaskCollection tasks_exists = GetAllTaskschd();
                for (int i = 1; i <= tasks_exists.Count; i++)
                {
                    IRegisteredTask registeredTask = tasks_exists[i];
                    if (registeredTask.Name.Equals(strTaskName))
                    {
                        isExists = true;
                        break;
                    }
                }
                return isExists;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }
    }
}
