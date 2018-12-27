using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApplication1.Models
{
    public class QueryResultModel
    {       
        public static List<QueryDetails> GetStackOverflowQueries(string search)
        {
            List<QueryDetails> queriesInformation = new List<QueryDetails>();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.stackexchange.com/2.2/search/advanced?order=desc&sort=activity&q="+search +"&site=stackoverflow");
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
            return queriesInformation;
        }

        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }

    public class QueryDetails
    {
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