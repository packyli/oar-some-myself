using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoggerService
{
    public Queue<PlayerController.Distance> distance = new Queue<PlayerController.Distance>();
    public Queue<PlayerController.Power> power = new Queue<PlayerController.Power>();
    public Queue<PlayerController.RPM> rpm = new Queue<PlayerController.RPM>();

    private static string LOGGER_PATH;
    private const string GAME_DATA = "_game_data.csv";

    public LoggerService()
    {

        LOGGER_PATH = Application.persistentDataPath + "/LOGGER_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

        File.AppendAllText(LOGGER_PATH + GAME_DATA, "ParticipantID,Round,SystemTime,TimeCount,AvatarConditionImproved,Power,Distance,RPM\n");


        Debug.Log("CSV file is being saved at: " + LOGGER_PATH);
    }


    public void Log(string participantID, int round, int timeCount, string avatarType)
    {

        while (power.Count > 0 && distance.Count > 0 && rpm.Count > 0)
        {
            PlayerController.Power powerData = power.Dequeue();
            PlayerController.Distance distanceData = distance.Dequeue();
            PlayerController.RPM rpmData = rpm.Dequeue();

            string logEntry = $"{participantID},{round},{powerData.time},{timeCount},{avatarType},{powerData.power},{distanceData.distance},{rpmData.rpm}\n";
            File.AppendAllText(LOGGER_PATH + GAME_DATA, logEntry);
        }
    }
}

