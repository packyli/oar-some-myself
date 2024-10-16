using UnityEngine;
using System.Collections.Generic;

namespace UnityJIS
{

    public class Timer 
{
    public static List<TimerEvent> m_timers = new List<TimerEvent>();

    public static TimerEvent test;

    public static void Init()
    {
        Engine.s_OnApplicationUpdate += Update;
    }

	static void Update () 
    {
        for (int i = 0; i < m_timers.Count;i++ )
        {
            TimerEvent e = m_timers[i];
            e.Update();

            if (e.m_isDone)
            {
                e.CompleteTimer();

                if (e.m_isDone)
                {
                    m_timers.Remove(e);
                    i--;
                }
            }
        }

        if(test != null)
        {
            Debug.Log("Test " + test.m_timerName + " " + test.m_currentTimer + " " + m_timers.Contains(test) + " isDone " + test.m_isDone); 
        }
    }

    public static bool GetIsExistTimer(string timerName)
    {
        for (int i = 0; i < m_timers.Count; i++)
        {
            var e = m_timers[i];
            if (e.m_timerName == timerName)
            {
                return true;
            }
        }

        return false;
    }

    public static TimerEvent GetTimer(string timerName)
    {
        for (int i = 0; i < m_timers.Count; i++)
        {
            var e = m_timers[i];
            if (e.m_timerName == timerName)
            {
                return e;
            }
        }

        throw new System.Exception("Get Timer  Exception not find ->" + timerName + "<-");
    }


    public static TimerEvent DelayCallBack(float delayTime,TimerCallBack callBack,params object[] objs)
    {
        return AddTimer(delayTime, false, 0, null, callBack, objs); 
    }

    public static TimerEvent DelayCallBack(float delayTime, bool isIgnoreTimeScale, TimerCallBack callBack, params object[] objs)
    {
        return AddTimer(delayTime, isIgnoreTimeScale, 0, null, callBack, objs);
    }

    public static TimerEvent CallBackOfIntervalTimer(float spaceTime,TimerCallBack callBack, params object[] objs)
    {
        return AddTimer(spaceTime, false, -1, null, callBack, objs); 
    }

    public static TimerEvent CallBackOfIntervalTimer(float spaceTime, bool isIgnoreTimeScale, TimerCallBack callBack, params object[] objs)
    {
        return AddTimer(spaceTime, isIgnoreTimeScale, -1, null, callBack, objs);
    }

    public static TimerEvent CallBackOfIntervalTimer(float spaceTime, bool isIgnoreTimeScale, string timerName,TimerCallBack callBack, params object[] objs)
    {
        return AddTimer(spaceTime, isIgnoreTimeScale, -1, timerName, callBack, objs);
    }

    public static TimerEvent CallBackOfIntervalTimer(float spaceTime, int callBackCount, TimerCallBack callBack, params object[] objs)
    {
        return AddTimer(spaceTime, false, callBackCount, null, callBack, objs);
    }

    public static TimerEvent CallBackOfIntervalTimer(float spaceTime, bool isIgnoreTimeScale, int callBackCount, TimerCallBack callBack, params object[] objs)
    {
        return AddTimer(spaceTime, isIgnoreTimeScale, callBackCount, null,callBack, objs); ;
    }


    public static TimerEvent CallBackOfIntervalTimer(float spaceTime, bool isIgnoreTimeScale, int callBackCount, string timerName, TimerCallBack callBack, params object[] objs)
    {
        return AddTimer(spaceTime, isIgnoreTimeScale, callBackCount, timerName, callBack, objs);
    }

    public static TimerEvent AddTimer(float spaceTime, bool isIgnoreTimeScale, int callBackCount, string timerName,TimerCallBack callBack, params object[] objs)
    {
        TimerEvent te = new TimerEvent();

        te.m_timerName = timerName ?? te.GetHashCode().ToString();
        te.m_currentTimer = 0;
        te.m_timerSpace = spaceTime;

        te.m_callBack = callBack;
        te.m_objs = objs;

        te.m_isIgnoreTimeScale = isIgnoreTimeScale;
        te.m_repeatCount = callBackCount;

        m_timers.Add(te);

        return te;
    }

    public static void DestroyTimer(TimerEvent timer,bool isCallBack = false)
    {
        //Debug.Log("DestroyTimer " + timer.m_timerName + " isTest " + (timer == test));

        if (m_timers.Contains(timer))
        {
            if (isCallBack)
            {
                timer.CallBackTimer();
            }

            m_timers.Remove(timer);
        }
        else
        {
            Debug.LogError("Timer DestroyTimer error: dont exist timer " + timer);
        }
    }

    public static void DestroyTimer(string timerName, bool isCallBack = false)
    {
        //Debug.Log("DestroyTimer2  ----TIMER " + timerName);
        for (int i = 0; i < m_timers.Count;i++ )
        {
            TimerEvent te = m_timers[i];
            if (te.m_timerName.Equals(timerName))
            {
                DestroyTimer(te, isCallBack);
            }
        }
    }

    public static void DestroyAllTimer(bool isCallBack = false)
    {
        for (int i = 0; i < m_timers.Count; i++)
        {
            if (isCallBack)
            {
                m_timers[i].CallBackTimer();
            }
        }

        m_timers.Clear();
    }

    public static void ResetTimer(TimerEvent timer)
    {
        if(m_timers.Contains(timer))
        {
            timer.ResetTimer();
        }
        else
        {
            Debug.LogError("Timer ResetTimer error: dont exist timer "+ timer);
        }
    }

    public static void ResetTimer(string timerName)
    {
        for (int i = 0; i < m_timers.Count; i++)
        {
            var e = m_timers[i];

            if (e.m_timerName.Equals(timerName))
            {
                ResetTimer(e);
            }
        }
    }
}

}

