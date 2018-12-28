using Newtonsoft.Json.Linq;
using Syncfusion.JavaScript;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApplication1.Models
{
    public class QueryResultModel
    {
        static string searchQuery;

        public int totalCount;
        /// <summary>
        /// Gets or sets a value of the FromDate.The member declare as public.
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets a value of the ToDate.The member declare as public.
        /// </summary>
        public DateTime ToDate { get; set; }
        static List<QueryDetails> queriesInformationStatic = new List<QueryDetails>();

        List<QueryDetails> queriesInformation = new List<QueryDetails>();

        IEnumerable<QueryDetails> QueriesInformationEnumerable;
        public List<QueryDetails> GetSerarchQuereies(DataManager dataManager, string serachField, string startDate, string endDate)
        {
            try
            {
                if (!string.IsNullOrEmpty(startDate))
                {
                    this.FromDate = DateTime.Parse(startDate, null, System.Globalization.DateTimeStyles.None);
                    this.ToDate = DateTime.Parse(endDate, null, System.Globalization.DateTimeStyles.None);
                }

                if (searchQuery != serachField)
                {
                    GetStackOverflowQueries(serachField);
                    GetGithubQueries(serachField);
                }
                else
                {
                    queriesInformation = queriesInformationStatic;
                }

                if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                {
                    queriesInformation = queriesInformation.Where(x => x.creationDate >= FromDate && x.creationDate <= ToDate).ToList();
                }

                if (queriesInformation != null)
                {
                    if (searchQuery != serachField)
                    {
                        queriesInformationStatic = queriesInformation;
                        searchQuery = serachField;
                    }

                    Syncfusion.JavaScript.DataSources.DataOperations operation = new Syncfusion.JavaScript.DataSources.DataOperations();
                    QueriesInformationEnumerable = queriesInformation.AsEnumerable();
                    //// Filtering
                    if (dataManager.Where != null && dataManager.Where.Count > 0)
                    {
                        QueriesInformationEnumerable = operation.PerformWhereFilter(QueriesInformationEnumerable, dataManager.Where, dataManager.Where[0].Operator).Cast<QueryDetails>();
                    }

                    //// Sorting
                    if (dataManager.Sorted != null && dataManager.Sorted.Count > 0)
                    {
                        QueriesInformationEnumerable = operation.PerformSorting(QueriesInformationEnumerable, dataManager.Sorted).Cast<QueryDetails>();
                    }
                    else
                    {
                        dataManager.Sorted = new List<Syncfusion.JavaScript.Sort>();
                        Syncfusion.JavaScript.Sort sortValue = new Syncfusion.JavaScript.Sort();
                        sortValue.Direction = "descending";
                        sortValue.Name = "creationDate";
                        dataManager.Sorted.Add(sortValue);
                        QueriesInformationEnumerable = operation.PerformSorting(QueriesInformationEnumerable, dataManager.Sorted).Cast<QueryDetails>();
                    }

                    this.totalCount = QueriesInformationEnumerable.Count();
                    if (dataManager.Skip != 0)
                    {
                        QueriesInformationEnumerable = operation.PerformSkip(QueriesInformationEnumerable, dataManager.Skip).Cast<QueryDetails>();
                    }

                    if (dataManager.Take != 0)
                    {
                        QueriesInformationEnumerable = operation.PerformTake(QueriesInformationEnumerable, dataManager.Take).Cast<QueryDetails>();
                    }

                    this.queriesInformation = QueriesInformationEnumerable.ToList();

                }

            }
            catch (Exception ex)
            {

            }

            return queriesInformation;
        }

        public void GetGithubQueries(string query)
        {
            List<QueryDetails> githubQueries = new List<QueryDetails>();

            string result = CurlOperation("https://api.github.com/search/issues?q=" + query);
            var jsonObj = JObject.Parse(result);
            var values = (JArray)jsonObj["items"];

            foreach (var value in values)
            {
                var res = value;
                QueryDetails samp = new QueryDetails("Github", (DateTime)value["updated_at"], (DateTime)value["created_at"], (string)value["html_url"], (string)value["title"], (string)value["id"], int.Parse((string)value["comments"]), (int)Math.Ceiling((float)value["score"]));
                queriesInformation.Add(samp);
            }


        }

        public void GetStackOverflowQueries(string search)
        {

            try
            {
                //var queryDetails = new QueryDetails
                //{
                //    source = "Stackoverflow",
                //    title = "Render html in Flask IMAP application",
                //    link = "https://stackoverflow.com/questions/53953597/render-html-in-flask-imap-application",
                //    questionID = "53953597",
                //    creationDate = DateTime.Now,
                //    lastActivityDate = DateTime.Now
                //};

                //queriesInformation.Add(queryDetails);

                //var queryDetails1 = new QueryDetails
                //{
                //    source = "Stackoverflow",
                //    title = "Render html  Flask IMAP application",
                //    link = "https://stackoverflow.com/questions/53953597/render-html-in-flask-imap-application",
                //    questionID = "53953595",
                //    creationDate = DateTime.Now,
                //    lastActivityDate = DateTime.Now
                //};

                //queriesInformation.Add(queryDetails1);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.stackexchange.com/2.2/search/advanced?order=desc&sort=activity&q=" + search + "&site=stackoverflow");
                httpWebRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                httpWebRequest.Method = "GET";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                string responseText;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    responseText = streamReader.ReadToEnd();
                }
                var jsonObj = JObject.Parse(responseText);
                var values = (JArray)jsonObj["items"];

                foreach (var value in values)
                {
                    QueryDetails samp = new QueryDetails("StackOverflow", (bool)value["is_answered"], epoch.AddSeconds((int)value["last_activity_date"]), epoch.AddSeconds((int)value["creation_date"]), (string)value["link"], (string)value["title"], (string)value["question_id"], (int)value["score"], (int)value["view_count"], (int)value["answer_count"]);
                    queriesInformation.Add(samp);

                };
            }
            catch (Exception ex)
            {

            }
        }

        public static string CurlOperation(string arguments)
        {
            if (string.IsNullOrEmpty(arguments) == true)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            Process commandProcess = new Process();
            try
            {
                commandProcess.StartInfo.UseShellExecute = false;
                string path = Environment.ExpandEnvironmentVariables("%ProgramW6432%");
                if (File.Exists(path + @"\cURL\bin\curl.exe"))
                {
                    commandProcess.StartInfo.FileName = path + @"\cURL\bin\curl.exe"; //// this is the path of curl where it is installed;    
                }
                else if (File.Exists(path + @"\Git\usr\bin\curl.exe"))
                {
                    commandProcess.StartInfo.FileName = path + @"\Git\usr\bin\curl.exe"; //// this is the path of curl where it is installed;    
                }
                else
                {
                    commandProcess.StartInfo.FileName = path + @"\Git\mingw64\bin\curl.exe"; //// this is the path of curl where it is installed;    
                }

                commandProcess.StartInfo.Arguments = arguments;
                commandProcess.StartInfo.CreateNoWindow = true;
                commandProcess.StartInfo.RedirectStandardInput = true;
                commandProcess.StartInfo.RedirectStandardOutput = true;
                commandProcess.StartInfo.RedirectStandardError = true;
                commandProcess.Start();
                //commandProcess.WaitForExit();
                string output = commandProcess.StandardOutput.ReadToEnd();
                return output;
            }
            finally
            {
                if (commandProcess != null)
                {
                    commandProcess.Dispose();
                }
            }
        }

        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }

    public class QueryDetails
    {
        public QueryDetails()
        {

        }
        public QueryDetails(string SourceType, bool IsAnswered, DateTime Date, DateTime CreateDate, string Link, string Title, string QnID, int Score, int ViewCount, int AnswerCount)

        {
            this.source = SourceType;
            this.isAnswered = IsAnswered;
            this.lastActivityDate = Date;
            this.creationDate = CreateDate;
            this.link = Link;
            this.title = Title;
            this.questionID = QnID;
            this.score = Score;
            this.viewCount = ViewCount;
            this.answerCount = AnswerCount;
        }

        public QueryDetails(string SourceType, DateTime Date, DateTime CreateDate, string Link, string Title, string QnID, int ViewCount, int Score)

        {
            this.source = SourceType;
            this.lastActivityDate = Date;
            this.creationDate = CreateDate;
            this.link = Link;
            this.title = Title;
            this.questionID = QnID;
            this.viewCount = ViewCount;
            this.score = Score;
        }

        public string source { get; set; }

        public bool isAnswered { get; set; }

        public DateTime lastActivityDate { get; set; }

        public DateTime creationDate { get; set; }
        public DateTime lastEditDate { get; set; }

        public string link { get; set; }
        public string title { get; set; }

        public string questionID { get; set; }
        public int score { get; set; }
        public int viewCount { get; set; }

        public int answerCount { get; set; }
    }
}