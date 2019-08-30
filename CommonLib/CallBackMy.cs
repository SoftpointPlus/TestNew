using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public delegate void SetCahnged(bool value);
    public static class Student
    {
        public static string Name;
        public static string Group;
    }
    public static class CallBackMy
    {
        public delegate void callbackEvent(string data);

        public delegate void showQuestion(Question question, int currIndex, int count);
        public delegate void setTestResult(string student, string group, int valid, int novalid);
        public delegate void setChanged(bool changed);

        public delegate void fillFileName(List<string> list);

        public static callbackEvent callbackEventHandler;
        public static callbackEvent AddQuestionTextEventHandler;
        public static callbackEvent EditQuestionTextEventHandler;
        public static callbackEvent EditingQuestionEnentHandler;
        public static callbackEvent AddFormCaptionEnentHandler;
        public static callbackEvent QuestionResultEnentHandler;
        public static showQuestion ShowQuestionEventHendler;
        public static setTestResult ShowTestResult;
        public static setTestResult ShowTestResultDetail;
        public static setChanged SetChangedEventHendler;
        public static fillFileName FillFileNameEventHendler;

    }
}
