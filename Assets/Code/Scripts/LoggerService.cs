using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoggerService
{
    public Queue<PlayerController.Distance> distance = new Queue<PlayerController.Distance>();
    public Queue<PlayerController.Power> power = new Queue<PlayerController.Power>();
    public Queue<PlayerController.RPM> rpm = new Queue<PlayerController.RPM>();

    private static string LOGGER_PATH;
    private const string GAME_DATA = "_game_data.csv";  // 使用统一的CSV文件

    public LoggerService()
    {
        // 设置日志文件保存路径和名称
        LOGGER_PATH = Application.persistentDataPath + "/LOGGER_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

        // 创建并添加文件头（CSV列名）
        File.AppendAllText(LOGGER_PATH + GAME_DATA, "ParticipateID,Round,SystemTime,TimeCount,Power,Distance,RPM\n");


        // 打印文件保存路径
        Debug.Log("CSV file is being saved at: " + LOGGER_PATH);
    }

    // 保存数据到文件
    public void Log(string participateID, int round, int timeCount)
    {
        // 只有当队列中有数据时才进行写入操作
        while (power.Count > 0 && distance.Count > 0 && rpm.Count > 0)
        {
            PlayerController.Power powerData = power.Dequeue();
            PlayerController.Distance distanceData = distance.Dequeue();
            PlayerController.RPM rpmData = rpm.Dequeue();

            // 将轮次、系统时间、timeCount、ParticipateID、力量、距离和RPM写入到日志文件
            string logEntry = $"{participateID},{round},{powerData.time},{timeCount},{powerData.power},{distanceData.distance},{rpmData.rpm}\n";
            File.AppendAllText(LOGGER_PATH + GAME_DATA, logEntry);
        }
    }

}

