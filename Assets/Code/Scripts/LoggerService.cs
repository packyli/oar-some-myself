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
    private const string GAME_DATA = "_game_data.csv";  // ʹ��ͳһ��CSV�ļ�

    public LoggerService()
    {
        // ������־�ļ�����·��������
        LOGGER_PATH = Application.persistentDataPath + "/LOGGER_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

        // ����������ļ�ͷ��CSV������
        File.AppendAllText(LOGGER_PATH + GAME_DATA, "ParticipateID,Round,SystemTime,TimeCount,Power,Distance,RPM\n");


        // ��ӡ�ļ�����·��
        Debug.Log("CSV file is being saved at: " + LOGGER_PATH);
    }

    // �������ݵ��ļ�
    public void Log(string participateID, int round, int timeCount)
    {
        // ֻ�е�������������ʱ�Ž���д�����
        while (power.Count > 0 && distance.Count > 0 && rpm.Count > 0)
        {
            PlayerController.Power powerData = power.Dequeue();
            PlayerController.Distance distanceData = distance.Dequeue();
            PlayerController.RPM rpmData = rpm.Dequeue();

            // ���ִΡ�ϵͳʱ�䡢timeCount��ParticipateID�������������RPMд�뵽��־�ļ�
            string logEntry = $"{participateID},{round},{powerData.time},{timeCount},{powerData.power},{distanceData.distance},{rpmData.rpm}\n";
            File.AppendAllText(LOGGER_PATH + GAME_DATA, logEntry);
        }
    }

}

