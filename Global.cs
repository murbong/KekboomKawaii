using System.Text;
using System.Windows;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace KekboomKawaii
{

    [Flags]
    public enum ChatClassEnum : int
    {
        None = 0,
        World = 1,
        Team = 4,
        Recruit = 64,
        Guild = 128
    }

    public enum GenderEnum : int
    {
        None = 0,
        Female = 1,
        Male = 0x70
    }
    public enum GuildPostEnum : int
    {
        Member = 0,
        Manager = 2,
        Admin = 5,
        Owner = 6
    }
    public static class Global
    {

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string section, string key, string val, string filePath);


        public static void ShowToast(string appId, string title, string message, string image)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(
            string.IsNullOrEmpty(image) ? ToastTemplateType.ToastText02 : ToastTemplateType.ToastImageAndText02);

            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
            stringElements[0].AppendChild(toastXml.CreateTextNode(title));
            stringElements[1].AppendChild(toastXml.CreateTextNode(message));

            if (string.IsNullOrEmpty(image) == false)
            {
                string imagePath = image;
                XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
                imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;
            }

            ToastNotification toast = new ToastNotification(toastXml);

            ToastNotificationManager.CreateToastNotifier(appId).Show(toast);
        }


        public static Dictionary<ChatClassEnum, string> ChatClassDictionary = new Dictionary<ChatClassEnum, string>
        {
            { ChatClassEnum.World,"세계" },
            { ChatClassEnum.Team,"팀" },
            { ChatClassEnum.Recruit,"모집" },
            { ChatClassEnum.Guild,"길드" },

        };

        public static Dictionary<string, string> TitleDictionary = new Dictionary<string, string>
        {
            {"None","칭호 없음" },
                {"1_1_10_2", "나이트워커"},
                {"1_1_10_3", "시티헌터"},
                {"1_1_11_1", "심연 경청자"},
                {"1_1_12_1", "늪의 탐구자"},
                {"1_1_6_2", "섬 관찰자"},
                {"1_1_7_1", "모래 폭풍 극복자"},
                {"1_1_7_2", "모래 폭풍 정복자"},
                {"1_1_8_1", "극암 등반자"},
                {"1_1_8_2", "극암 완등자"},
                {"1_1_9_1", "사막 탐색자"},
                {"1_1_9_2", "사막 정복자"},
                {"1_4_3_1", "시퀀스 재건자"},
                {"1_4_3_2", "시퀀스 지배자"},
                {"1_4_3_3", "시퀀스 수호자"},
                {"1_4_3_4", "시퀀스 관리자"},
                {"1_4_3_5", "시퀀스 감시자"},
                {"1_4_4_1", "최초의 시퀀스"},
                {"1_4_4_2", "암흑 시퀀스 사도"},
                {"1_4_4_3", "기존 시퀀스 하인"},
                {"1_4_4_4", "시퀀스 근위"},
                {"1_4_4_5", "초월 방문자"},
                {"1_4_4_6", "초월 워커"},
                {"1_4_4_7", "초월 지도자"},
                {"1_4_5_1", "그랜드 마스터"},
                {"1_4_6_1", "전쟁의 씨앗"},
                {"1_4_6_2", "무너진 장벽"},
                {"1_4_6_3", "전쟁의 물결"},
                {"1_4_6_4", "병사의 운명"},
                {"1_4_6_5", "경탄의 노래"},
                {"1_4_6_6", "굳센 의지"},
                {"1_4_7_1", "스피드 챔피언"},
                {"1_4_7_2", "스피드 스타"},
                {"1_4_7_3", "스피드 시커"},
                {"1_4_7_4", "스피드 레이서"},
                {"1_6_1_2", "성급 파티시에"},
                {"1_6_1_3", "별 포획자"},
                {"1_6_1_4", "별 관찰자"},
                {"1_6_1_5", "벨라 파일럿"},
                {"1_6_1_6", "월병 파티셰"},
                {"1_7_1_1", "무기수집가"},
                {"1_7_1_2", "웨폰 익스퍼드"},
                {"1_7_1_3", "웨폰 마스터"},
                {"Overseas2_10", "배틀 엔젤"},
                {"Overseas2_1", "새로운 개척자"},
                {"Overseas2_2", "벨라 개척자"},
                {"Overseas2_3", "벨라 관찰자"},
                {"Overseas2_4", "지심 추락"},
                {"Overseas2_5", "세베크 킬러"},
                {"Overseas2_6", "GPS 미러시티"},
                {"Overseas2_7", "헬퍼"},
                {"Overseas2_8", "판타지 타워 예술가"},
                {"Overseas2_9", "새벽 이검"},
                {"Overseas_11", "상락궁 첫 방문"},
                {"Overseas_12", "카드게임의 고수"},
                {"Overseas_13", "브론즈 코인의 대부"},
                {"Overseas_1", "선봉 개척자"},
                {"Overseas_2", "헬가드 관찰자"},
                {"iw_1", "알 수 없는 칭호1"},
                {"iw_2", "알 수 없는 칭호2"},
                {"iw_3", "알 수 없는 칭호3"},
                {"iw_4", "알 수 없는 칭호4"},
                {"iw_5", "알 수 없는 칭호5"},
                {"iw_6", "알 수 없는 칭호6"},
                {"iw_7", "알 수 없는 칭호7"},
                {"iw_8", "황금 오른발"},
                {"korea_1", "최초의 용사"},
                {"korea_2", "고독한 용사"},
                {"korea_3", "아이다 개척자"},
                {"korea_4", "버그 사냥꾼"},
                {"korea_5", "레드 데빌"},
                {"korea_6", "어비스 프론티어"},
                {"korea_7", "패스파인더"},
                {"Title_koreafestival_1", "계묘년 검은 토끼"},
                {"Title_koreafestival_2", "T.O.F 공략왕"},
                {"Title_koreafestival_3", "행운 탐험가"},
                {"1_1_1_1", "아스트라 디자이너"},
                {"1_1_2_1", "뱅기스 실습생"},
                {"1_1_3_1", "놀이공원 관리자"},
                {"1_1_4_1", "광산 탐험가"},
                {"1_1_5_1", "설원 조사원"},
                {"1_1_6_1", "섬 개척자"},
                {"1_2_10_1", "자유등반"},
                {"1_2_11_1", "민첩한 사냥꾼"},
                {"1_2_12_1", "파도 바람"},
                {"1_2_13_1", "자유로운 선율"},
                {"1_2_14_1", "축구 고수"},
                {"1_2_15_1", "균형의 길"},
                {"1_2_1_1", "상공 비상"},
                {"1_2_2_1", "날쌘돌이"},
                {"1_2_3_1", "사뿐사뿐"},
                {"1_2_4_1", "백발백중"},
                {"1_2_5_1", "비상한 기억력"},
                {"1_2_6_1", "새로운 길 개척"},
                {"1_2_7_1", "선뜻한 바람"},
                {"1_2_8_1", "용맹한 전진"},
                {"1_2_9_1", "메가 브레인 박사"},
                {"1_3_1_1", "지휘관"},
                {"1_3_1_2", "장군"},
                {"1_3_1_3", "제독"},
                {"1_3_1_4", "총사령관"},
                {"1_3_1_5", "일등병"},
                {"1_3_1_6", "정예병"},
                {"1_3_1_7", "하사관"},
                {"1_3_2_1", "정원사"},
                {"1_4_1_1", "찬란한 왕관"},
                {"1_4_1_2", "별을 쫓는 영웅"},
                {"1_4_1_3", "별을 쫓는 투사"},
                {"1_4_1_4", "라이벌"},
                {"1_4_1_5", "도전자"},
                {"1_4_2_1", "가시덤불 왕관"},
                {"1_4_2_2", "가시밭길의 영웅"},
                {"1_4_2_3", "가시밭길의 용자"},
                {"1_4_2_4", "탐색가"},
                {"1_4_2_5", "어드밴서"},
                {"tencent_1", "ToF 서포터"},
                {"tencent_2", "그린필드 레전드"},
                {"tencentfestival_1", "린 yeah"},
                {"tencentfestival_2", "린 씨의 골수팬"},

        };

        public const string UserSettingsFileName = @"\Settings.ini";

        private static string filterString;
        public static string FilterString
        {
            get { return filterString; }
            set
            {
                filterString = value;
                SetiniValue("Setting", "FilterString", value);
            }
        }

        private static int ethernetIndex;
        public static int EthernetIndex
        {
            get { return ethernetIndex; }
            set
            {
                ethernetIndex = value;
                SetiniValue("Setting", "EthernetIndex", value.ToString());
            }
        }


        public static PacketSniffer Sniffer { get; set; }

        private static string GetiniValue(string section, string key)
        {
            var temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, null, temp, 255, AppDomain.CurrentDomain.BaseDirectory + UserSettingsFileName);
            return temp.ToString();
        }

        private static void SetiniValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, AppDomain.CurrentDomain.BaseDirectory + UserSettingsFileName);
        }


        static Global()
        {
            filterString = GetiniValue("Setting", "FilterString");
            if (int.TryParse(GetiniValue("Setting", "EthernetIndex"), out int index))
            {
                ethernetIndex = index;
            }
            Sniffer = new PacketSniffer();
        }
    }
}
