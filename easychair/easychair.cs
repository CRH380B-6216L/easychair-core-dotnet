﻿using System;
using System.Runtime.InteropServices;
/// <summary>
/// EasyChair 程序使用的类型、函数和变量等。
/// </summary>
namespace EasyChair
{
    #region Enums

    /// <summary>
    /// 定义程序的状态。
    /// </summary>
    public enum AppStatus
    {
        /// <summary>
        /// 程序空闲中。
        /// </summary>
        Idle = 0,
        /// <summary>
        /// 程序正在执行点名流程。
        /// </summary>
        RollCall = 1,
        /// <summary>
        /// 程序正在执行名单发言流程。
        /// </summary>
        SpeakersList = 2,
        /// <summary>
        /// 程序正在执行动议流程。
        /// </summary>
        Motion = 3,
        /// <summary>
        /// 程序正在执行文件展示流程。
        /// </summary>
        FileView = 4,
        /// <summary>
        /// 程序正在执行表决流程。
        /// </summary>
        Vote = 5,
        /// <summary>
        /// 程序正在使用计时器。
        /// </summary>
        TimerOnly = 6
    };
    /// <summary>
    /// 定义文件类型。
    /// </summary>
    public enum FileCategory
    {
        /// <summary>
        /// 工作文件。
        /// </summary>
        WorkingPaper = 0,
        /// <summary>
        /// 决议草案。
        /// </summary>
        DraftResolution = 1,
        /// <summary>
        /// 修正案。
        /// </summary>
        Amendment = 2,
        /// <summary>
        /// 指令草案。
        /// </summary>
        DraftDirective = 3,
        /// <summary>
        /// 危机文件。
        /// </summary>
        Crisis = 4,
        /// <summary>
        /// 政策建议。
        /// </summary>
        PolicySuggect = 5,
        /// <summary>
        /// 其他杂项文件。
        /// </summary>
        Miscellaneous = 6
    };

    /// <summary>
    /// 定义主发言名单剩余时间的让渡方式。
    /// </summary>
    public enum YieldTo
    {
        /// <summary>
        /// 无让渡操作定义。
        /// </summary>
        NoYield = 0,
        /// <summary>
        /// 让渡给其他国家。
        /// </summary>
        ToNation = 1,
        /// <summary>
        /// 让渡给问题。
        /// </summary>
        ToQuestion = 2,
        /// <summary>
        /// 让渡给评论。
        /// </summary>
        ToComment = 3,
        /// <summary>
        /// 让渡给主席。
        /// </summary>
        ToDais = 4
    };

    // 定义工作语言。
    public enum WorkLanguage
    {
        /// <summary>
        /// 简体中文。
        /// </summary>
        ChineseS = 1,
        /// <summary>
        /// English.
        /// </summary>
        English = 2
    };

    /// <summary>
    /// 定义会议使用的议事规则。
    /// </summary>
    public enum UsingRule
    {
        Robert = 0,
        European = 1,
        Security = 2
    };

    /// <summary>
    /// 定义议题选定情况。
    /// </summary>
    public enum TopicSelection
    {
        /// <summary>
        /// 选定第一议题，或仅有单一议题。
        /// </summary>
        Topic1 = 0,
        /// <summary>
        /// 选定第二议题。
        /// </summary>
        Topic2 = 1,
        /// <summary>
        /// 议题等待选定。
        /// </summary>
        Unchosen = 2
    };

    /// <summary>
    /// 定义投票立场。
    /// </summary>
    public enum VoteOpinion
    {
        /// <summary>
        /// 赞成。
        /// </summary>
        Yes = 1,
        /// <summary>
        /// 反对。
        /// </summary>
        No = 2,
        /// <summary>
        /// 弃权。
        /// </summary>
        Abst = 3
    };

    #endregion

    /// <summary>
    /// 定义会议基本信息。
    /// </summary>
    public class Conference
    {
        public WorkLanguage Language { get; set; }
        public TopicSelection TopicSel { get; private set; }
        public UsingRule MyRule { get; set; }
        public int XmlVersion { get; set; }
        public string ConferenceTitle { get; set; }
        public string Committee { get; set; }
        public string[] Topic = new string[1];
        public int Session { get; private set; }

        /// <summary>
        /// 新建会议。
        /// </summary>
        /// <param name="title">会议名称。</param>
        /// <param name="committee">会议的委员会名称。</param>
        /// <param name="topic">会议议题。</param>
        /// <param name="lang">会议的工作语言。</param>
        public Conference(string title, string committee, string topic, WorkLanguage lang)
        {
            ConferenceTitle = title;
            Committee = committee;
            Topic[0] = topic;
            Language = lang;
            TopicSel = TopicSelection.Topic1;
        }

        /// <summary>
        /// 新建会议。
        /// </summary>
        /// <param name="title">会议名称。</param>
        /// <param name="committee">会议的委员会名称。</param>
        /// <param name="topic1">会议议题。</param>
        /// <param name="topic2">创建双议题会议时会议的第二议题，并将 TopicSel 字段设定为 Unchosen。</param>
        /// <param name="lang">会议的工作语言。</param>
        public Conference(string title, string committee, string topic1, string topic2, WorkLanguage lang) : this(title, committee, topic1, lang)
        {
            Topic[0] = topic1;
            Topic[1] = topic2;
            TopicSel = TopicSelection.Unchosen;
        }

        /// <summary>
        /// 获取会议议题。
        /// </summary>
        /// <returns>会议的议题。</returns>
        public string GetTopic
        {
            get
            {
                if ((int)TopicSel < 2) return Topic[(int)TopicSel];
                return "议题等待决定中";
            }
        }
    }

    /// <summary>
    /// 表示单个国家。
    /// </summary>
    public class Nation
    {
        /// <summary>
        /// 国家名称。
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 代表姓名。
        /// </summary>
        public List<Delegate> Delegates = new();
        /// <summary>
        /// 该国投票权限，默认值为 1（一票）。
        /// </summary>
        public int Competence { get; set; } = 1;
        /// <summary>
        /// 该国立场文件提交情况。
        /// </summary>
        public string? PresentationPaper { get; set; }
        /// <summary>
        /// 该国出席情况。
        /// </summary>
        public bool Attendence { get; set; } = false;
        /// <summary>
        /// 该国是否拥有一票否决权。
        /// </summary>
        public bool VetoPower { get; set; } = false;

        /// <summary>
        /// 初始化国家。
        /// </summary>
        /// <param name="name">国家的名称。</param>
        /// <param name="delegates">可选。该国的所有代表。</param>
        public Nation(string name, List<Delegate>? delegates = null)
        {
            Name = name;
            if (delegates != null) Delegates = delegates;
        }

        /// <summary>
        /// 初始化国家并设定投票权重。
        /// </summary>
        /// <param name="name">国家的名称。</param>
        /// <param name="delegates">可选。该国的所有代表。</param>
        /// <param name="competence">表示该国的投票权重，默认值为 1（一票）。</param>
        public Nation(string name, int competence, List<Delegate>? delegates = null) : this(name, delegates)
        {
            Competence = competence;
        }

        /// <summary>
        /// 初始化国家并设定投票权重和一票否决权。
        /// </summary>
        /// <param name="name">国家的名称。</param>
        /// <param name="delegates">可选。该国的所有代表。</param>
        /// <param name="competence">表示该国的投票权重，默认值为 1（一票）。</param>
        /// <param name="vetopower">表示该国是否具有一票否决权。</param>
        public Nation(string name, int competence, bool vetopower, List<Delegate>? delegates = null) : this(name, competence, delegates)
        {
            VetoPower = vetopower;
        }

        /// <summary>
        /// 返回所有代表的姓名。
        /// </summary>
        /// <param name="delimiter">可选。任何字符串，用于在返回的字符串中分隔子字符串。如果省略该参数，则使用空白字符 (" ")。如果 Delimiter 是零长度字符串 ("") 或 Nothing，则列表中的所有项目都串联在一起，中间没有分隔符。</param>
        /// <returns>该国代表的姓名。</returns>
        public string GetDelegateNames(string delimiter) 
        {
            if (Delegates == null)
                return "";
            string delnames = "";
            foreach (Delegate d in Delegates)
            {
                if (!d.Equals(Delegates.FirstOrDefault())) delnames += delimiter;
                delnames += d.ToString();
            }
            return delnames;
        }

        /// <summary>
        /// 将当前国家的投票权限转换为其等效的字符串表示形式。
        /// </summary>
        /// <returns>以数字表示的投票权限，数字代表其票数；星号（*）标记代表该国家具有一票否决权。</returns>
        public string GetCompetence() 
        {
            string cp = Competence.ToString();
            if (VetoPower) cp += "*";
            return cp;
        }

        /// <summary>
        /// 将当前国家的名称转换为其等效的字符串表示形式。 
        /// </summary>
        /// <returns>当前国家名称的字符串表示形式。</returns>
        public override string ToString()
        {
            return Name;
        }
    }

    public class Delegate
    {
        //兼容在线系统中的代表类，本API中使用以下数据：
        //1. 代表姓名；
        //2. 学校；
        //3. 年级。
    }

    /// <summary>
    /// 表示包含国家的列表。
    /// </summary>
    public class NationList : List<Nation>
    {
        /// <summary>
        /// 根据国家名获取国家。
        /// </summary>
        /// <param name="name">需要获取的国家名称。如果该国在列表中不存在，则返回 null 值。</param>
        /// <returns>对应名称的国家对象。</returns>
        public Nation? GetNationFromName(string name)
        {
            return FindNation(name, this);
        }

        /// <summary>
        /// 根据国家名从国家列表获取国家。
        /// </summary>
        /// <param name="name">需要获取的国家名称。如果该国在列表中不存在，则返回 null 值。</param>
        /// <param name="list">指定的国家列表。</param>
        /// <returns>对应名称的国家对象。</returns>
        public static Nation? FindNation(string name, NationList list)
        {
            foreach (Nation n in list)
            {
                if (n.Name == name) return n;
            }
            return null;
        }

        /// <summary>
        /// 返回国家的出席数。
        /// </summary>
        /// <returns>出席国家计数。</returns>
        public int GetAttendentNumber()
        {
            int a = 0;
            foreach (Nation i in this)
            {
                if (i.Attendence == true) a++;
            }
            return a;
        }

        /// <summary>
        /// 返回所有国家的名称。
        /// </summary>
        /// <param name="delimiter">可选。任何字符串，用于在返回的字符串中分隔子字符串。如果省略该参数，则使用空白字符 (" ")。如果 Delimiter 是零长度字符串 ("") 或 Nothing，则列表中的所有项目都串联在一起，中间没有分隔符。</param>
        /// <returns>所有国家名称的字符串表示形式。</returns>
        public string ToString(string delimiter = " ")
        {
            if (Count == 0)
                return "";
            string nationname = "";
            foreach (Nation n in this)
            {
                if (!n.Equals(this.FirstOrDefault())) nationname += delimiter;
                nationname += n.ToString();
            }
            return nationname;
        }
    }

    /// <summary>
    /// 定义发言列表。
    /// </summary>
    public class SpeakersList
    {
        /// <summary>
        /// 发言列表的名称。
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 单个代表的发言时间，单位为秒。
        /// </summary>
        public int SingleTime { get; set; }
        /// <summary>
        /// 发言列表的总时间，单位为秒。如果 TotalTime 值为零，则总时间无限制。
        /// </summary>
        public int TotalTime { get; private set; }
        /// <summary>
        /// 表示发言标记所在的位置。
        /// </summary>
        public int Current { get; private set; }
        /// <summary>
        /// 表示该发言列表是否接受让渡。
        /// </summary>
        public bool AllowYield { get; private set; }
        /// <summary>
        /// 发言列表包含的国家。
        /// </summary>
        public NationList slNations = new();
        /// <summary>
        /// 与发言国家对应的发言状态，数值代表发言记录编号。
        /// </summary>
        public List<int> slIsSpoken = new();
        /// <summary>
        /// 与发言国家对应的让渡状态。
        /// </summary>
        public List<YieldTo>? yieldMethod;

        /// <summary>
        /// 以默认发言时间 120 s 初始化无限总时长的新的发言列表。
        /// </summary>
        /// <param name="name">发言列表的名称。</param>
        /// <param name="yield">可选。设置该发言列表是否允许让渡选项。</param>
        public SpeakersList(string name, bool yield = false)
        {
            Name = name;
            SingleTime = 120;
            AllowYield = yield;
            if (AllowYield) yieldMethod = new List<YieldTo>();
        }

        /// <summary>
        /// 以指定的发言时间初始化无限总时长的新的发言列表。 
        /// </summary>
        /// <param name="name">发言列表的名称。</param>
        /// <param name="time">指定的发言时间，单位为秒。</param>
        /// <param name="yield">可选。设置该发言列表是否允许让渡选项。</param>
        public SpeakersList(string name, int time, bool yield = false) : this(name, yield)
        {
            SingleTime = time;
        }

        /// <summary>
        /// 以指定的发言时间初始化有限的新的发言列表。
        /// </summary>
        /// <param name="name">发言列表的名称。</param>
        /// <param name="time">指定的发言时间。</param>
        /// <param name="totaltime">指定的发言列表总时间，单位为秒。</param>
        /// <param name="yield">可选。设置该发言列表是否允许让渡选项。</param>
        public SpeakersList(string name, int time, int totaltime, bool yield = false) : this(name, time, yield)
        {
            TotalTime = totaltime;
        }

        /// <summary>
        /// 向发言列表添加国家。
        /// </summary>
        /// <param name="n">被添加的国家。</param>
        /// <returns>返回当前发言列表的国家总数。如果该发言列表是有限的，则返回剩余可添加至发言列表的国家数。</returns>
        public int AddNation(Nation n)
        {
            int available = TotalTime / SingleTime;
            if (available >= slNations.Count) throw new Exception("发言列表已满！");
            slNations.Add(n);
            if (available > 0) return available - slNations.Count;
            return slNations.Count;
        }

        /// <summary>
        /// 结束当前发言，并将发言标记移动至下一个国家。
        /// </summary>
        /// <param name="isSpoken"></param>
        /// <returns>移动后发言标记的值。</returns>
        public int SpeakNext(int isSpoken)
        {
            slIsSpoken.Add(isSpoken);
            return ++Current;
        }
    }

    /// <summary>
    /// 定义计时器。实现该类型需要外部 Timer 控件支持。
    /// </summary>
    public class Timer
    {
        /// <summary>
        /// 计时器启停状态。
        /// </summary>
        public bool IsUp { get; private set; }
        /// <summary>
        /// 计时器剩余时长。
        /// </summary>
        public int RemainingSecs { get; private set; }
        /// <summary>
        /// 计时器总时长。
        /// </summary>
        internal readonly int TotalSecs;
        /// <summary>
        /// 计时器剩余时间不足发出提示时的剩余时间。
        /// </summary>
        public int WarningSecs { get; set; }

        /// <summary>
        /// 初始化计时器。
        /// </summary>
        /// <param name="totalsec">设置计时器的总时长，单位为秒。</param>
        /// <param name="warning">设置计时器剩余时间不足的提示时长，单位为秒。</param>
        public Timer(int totalsec, int warning = 20)
        {
            TotalSecs = totalsec;
            RemainingSecs = totalsec;
            WarningSecs = warning;
        }

        /// <summary>
        /// 启动计时器。
        /// </summary>
        public virtual void Up()
        {
            IsUp = true;
            TimerUp?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// 停止计时器。
        /// </summary>
        public virtual void Down()
        {
            IsUp = false;
            TimerDown?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// 将计时器重置为设定时长。
        /// </summary>
        public void Reset()
        {
            RemainingSecs = TotalSecs;
            TimerReset?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// 计时器倒数一次。需要外部 Timer 控件支持，并每秒实施一次。
        /// </summary>
        public virtual void Tick()
        {
            if (RemainingSecs == 0)
            {
                TimeOut?.Invoke(this, new EventArgs());
                Down();
            }
            else
            {
                if (--RemainingSecs == WarningSecs) TimeWarning?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// 引发 TimeOut 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 EventArgs。</param>
        protected virtual void OnTimeOut(EventArgs e)
        {
            IsUp = false;
        }

        /// <summary>
        /// 在计时器启动时发生。
        /// </summary>
        public event EventHandler? TimerUp;
        /// <summary>
        /// 在计时器停止时发生。
        /// </summary>
        public event EventHandler? TimerDown;
        /// <summary>
        /// 在计时器重置时发生。
        /// </summary>
        public event EventHandler? TimerReset;
        /// <summary>
        /// 在计时器时间耗尽时发生。
        /// </summary>
        public event EventHandler? TimeOut;
        /// <summary>
        /// 在计时器时间剩余不足设定值时发生。
        /// </summary>
        public event EventHandler? TimeWarning;
    }

    /// <summary>
    /// 定义含有总时长和分时长的双重计时器。实现该类型需要外部 Timer 控件支持。
    /// </summary>
    public class DoubleTimer : Timer
    {
        /// <summary>
        /// 该双重计时器携带的分时长计时器。
        /// </summary>
        public Timer SingleTimer;

        /// <summary>
        /// 初始化双重计时器。
        /// </summary>
        /// <param name="totalsec">总时长计时器的设定时长，单位为秒。</param>
        /// <param name="singlesec">分时长计时器的设定时长，单位为秒。</param>
        /// <param name="warning">设置分时长计时器剩余时间不足的提示时长，单位为秒。</param>
        public DoubleTimer(int totalsec, int singlesec, int warning) : base(totalsec)
        {
            SingleTimer = new Timer(singlesec, warning);
            SingleTimer.TimeOut += SingleTimer_TimeOut;
            WarningSecs = -1;
        }

        /// <summary>
        /// 启动计时器。将同时启动总时长和分时长计时器。
        /// </summary>
        public override void Up()
        {
            base.Up();
            SingleTimer.Up();
        }

        /// <summary>
        /// 停止计时器。将同时停止总时长和分时长计时器。
        /// </summary>
        public override void Down()
        {
            base.Down();
            SingleTimer.Down();
        }

        /// <summary>
        /// 将分时长计时器重置为设定时长。
        /// </summary>
        public void ResetSingletimer()
        {
            SingleTimer.Reset();
        }

        public override void Tick()
        {
            base.Tick();
            SingleTimer.Tick();
        }

        /// <summary>
        /// 获取可加入发言列表的名单长度。
        /// </summary>
        /// <returns>可加入名单的总数。</returns>
        public int GetAvailableSpeeches()
        {
            return TotalSecs / SingleTimer.TotalSecs;
        }

        /// <summary>
        /// 分时长计时器时间耗尽触发 TimeOut 事件时，停止总时长计时器。
        /// </summary>
        /// <param name="sender">对引发事件的对象的引用。</param>
        /// <param name="e">包含事件数据的 EventArgs。</param>
        private void SingleTimer_TimeOut(object? sender, EventArgs e)
        {
            Down();
        }
    }
}