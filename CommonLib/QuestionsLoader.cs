using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Security.Cryptography.Xml;

namespace CommonLib
{
    public delegate void SetMenuEnabled(string name, bool enable);
    public delegate void SetTestName(string name);
    public delegate void EditingData();
    public delegate void SetQuestionLimit(int limit);
    public class QuestionsLoader
    {
        private int currentQuesIndex = 0;
        private int ValidAnswer = 0;
        private XmlDocument document;
        private Questions questions;
        private string path = "";
        private string caption;
        private int _questionLimit;
        private bool _changed = false;
        private bool _inLoad;
        public int QuestionLimit { get { return _questionLimit; } 
            set{
                _questionLimit = value;
                SetChanged(true);
        } }
        public bool Changed { get { return _changed; } }

        public event SetMenuEnabled SetMenuEnabledHandler;

        public event SetTestName SetTestNameHandler;
        public event SetQuestionLimit SetQuestionLimitHandler;
        public int Count { get { return questions.Count; } }
        public bool InLoad { get { return _inLoad; } }
        public string Path { get { return path; } }
        public string Caption
        {
            get { return caption; } 
            set  {
                    caption = value;
                    SetChanged(true);
                    
                }
        }
        public QuestionsLoader()
        {
            questions = new Questions();
            questions.SetChengedEvent += SetChanged;
            CallBackMy.EditingQuestionEnentHandler += EditingData;
            CallBackMy.SetChangedEventHendler += SetChanged;
            document = new XmlDocument();
            QuestionLimit = 2;
        }
        public void LoadQuestions(string path)
        {
            this.path = path;
            Question question = null;
            _inLoad = true;
            try
            {
                
                document.Load(path);
                Decrypt(Create3DES("MyPasswword"), document);
                XmlNode captionNode = document.DocumentElement.Attributes.GetNamedItem("caption");
                if (captionNode != null)
                {
                    caption = captionNode.Value;
                    if(SetTestNameHandler!=null)
                        SetTestNameHandler(caption);
                }

                XmlNode questionlimitNode = document.DocumentElement.Attributes.GetNamedItem("questionlimit");
                if (questionlimitNode != null)
                {
                    int res = 10;
                    if (Int32.TryParse(questionlimitNode.Value, out res))
                        QuestionLimit = res;
                    else
                        QuestionLimit = 10;
                    if (SetQuestionLimitHandler != null)
                        SetQuestionLimitHandler(QuestionLimit);
                }

                foreach (XmlNode node in document.DocumentElement)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.Name == "text")
                        {
                            question = questions.Add(childNode.InnerText);
                        }
                        if (childNode.Name == "answers")
                        {
                            foreach (XmlNode answerNode in childNode.ChildNodes)
                            {
                                if (answerNode.Name == "answer")
                                {
                                    XmlNode attr = answerNode.Attributes.GetNamedItem("verity");
                                    if (attr != null && question != null)
                                    {
                                        question.Add(answerNode.InnerText, bool.Parse(attr.Value));
                                    }
                                }

                            }

                        }
                    }
                }
                if (CallBackMy.AddFormCaptionEnentHandler != null)
                {
                    CallBackMy.AddFormCaptionEnentHandler(path);
                }
                _inLoad = false;
            }
            catch (XmlException e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void SaveQuestions()
        {
            SaveQuestionsAs(path);
        }
        public void SaveQuestionsAs(string path)
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);


            XmlElement element1 = doc.CreateElement(string.Empty, "test", string.Empty);
            XmlAttribute attr = doc.CreateAttribute("caption");
            attr.Value = Caption;
            element1.Attributes.Append(attr);
            XmlAttribute attr2 = doc.CreateAttribute("questionlimit");
            attr2.Value = QuestionLimit.ToString();
            element1.Attributes.Append(attr2);


            doc.AppendChild(element1);
            foreach (Question ques in questions.QuestionsList)
            {
                XmlElement question = doc.CreateElement(string.Empty, "question", string.Empty);
                XmlElement text = doc.CreateElement(string.Empty, "text", string.Empty);
                text.InnerText = ques.Text;
                question.AppendChild(text);
                XmlElement answers = doc.CreateElement(string.Empty, "answers", string.Empty);
                foreach (Answer answ in ques.Answers.AnswerList)
                {
                    XmlElement answer = doc.CreateElement(string.Empty, "answer", string.Empty);
                    answer.InnerText = answ.Data;
                    XmlAttribute verity = doc.CreateAttribute("verity");
                    verity.InnerText = answ.Verity.ToString();
                    answer.Attributes.Append(verity);
                    answers.AppendChild(answer);
                }
                question.AppendChild(answers);
                element1.AppendChild(question);

            }
                Encrypt(Create3DES("MyPasswword"), doc);
                doc.Save(path);
                _changed = false;
            
        }

        public void EditingData(string data)
        {
            if (!_inLoad)
            {
                SetMenuEnabledHandler("save", true);
            }
        }

        public Questions Questions { get { return questions; } }

        public void Shuffle(IList<Question> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Question value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public void StartTest()
        {
            Shuffle(questions.QuestionsList);
            currentQuesIndex = 0;
            if (questions.QuestionsList.Count > 0)
            {
                CallBackMy.ShowQuestionEventHendler(questions.QuestionsList[currentQuesIndex], currentQuesIndex + 1, (QuestionLimit > questions.QuestionsList.Count ? questions.QuestionsList.Count : QuestionLimit));
            }
        }
        public void Next()
        {
            currentQuesIndex += 1;
            if (currentQuesIndex < questions.QuestionsList.Count && currentQuesIndex < QuestionLimit)
            {
                if (CallBackMy.ShowQuestionEventHendler != null)
                {
                   CallBackMy.ShowQuestionEventHendler(questions.QuestionsList[currentQuesIndex], currentQuesIndex + 1, (QuestionLimit > questions.QuestionsList.Count?questions.QuestionsList.Count:QuestionLimit));

                }
            }
            else
            {
                if (CallBackMy.ShowTestResult != null)
                {
                    CallBackMy.ShowTestResult(Student.Name, Student.Group, ValidAnswer, (QuestionLimit > questions.QuestionsList.Count ? questions.QuestionsList.Count : QuestionLimit) - ValidAnswer);
                }

            }

        }

        public void CompareResult(List<int> resList)
        {
            List<int> origValue = new List<int>();
            bool result = true;
            if (currentQuesIndex >= 0 && currentQuesIndex < questions.QuestionsList.Count)
            {
                Question question = questions.QuestionsList[currentQuesIndex];
                if (questions != null)
                {
                    Answer answer;
                    for (int i = 0; i < question.Answers.AnswerList.Count; i++)
                    {
                        answer = question.Answers.AnswerList[i];
                        if (answer.Verity)
                        {
                            origValue.Add(i);
                        }
                    }

                    result = resList.SequenceEqual(origValue);

                }

                if (result)
                {
                    ValidAnswer++;
                }
            }

        }

        private static TripleDESCryptoServiceProvider Create3DES(string password)
        {
            TripleDESCryptoServiceProvider encKey = new TripleDESCryptoServiceProvider();

            MD5 md5 = new MD5CryptoServiceProvider();
            encKey.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(password));
            return encKey;
        }

        private static void Decrypt(SymmetricAlgorithm key, XmlDocument xmlDoc)
        {
            var encryptedXml = new EncryptedXml(xmlDoc);

            encryptedXml.AddKeyNameMapping("MyKey", key);

            encryptedXml.DecryptDocument();
        }

        private static void Encrypt(SymmetricAlgorithm key, XmlDocument xmlDoc)
        {
            var encryptedXml = new EncryptedXml(xmlDoc);

            var inputElement = (XmlElement)xmlDoc.SelectSingleNode("//test");

            encryptedXml.AddKeyNameMapping("MyKey", key);
            var ed = encryptedXml.Encrypt(inputElement, "MyKey");

            EncryptedXml.ReplaceElement(inputElement, ed, false);
        }

        public void SetChanged(bool changed)
        {
            if (!InLoad) 
            _changed = changed;
        }
        
    }
}
