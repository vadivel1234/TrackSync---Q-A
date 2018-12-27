using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class QueryResultModel
    {
        public static List<QuerySearchFields> GetQueryResult(string serachQuery)
        {
            var listQuery = new List<QuerySearchFields>();
            try
            {
                var question = new QuerySearchFields
                {
                    isAnswered = true,
                    lastActivityDate = DateTime.Now,
                    creationDate = DateTime.Now.AddDays(-2),
                    lastEditDate = DateTime.Now.AddDays(-1),
                    link = "https://stackoverflow.com/questions/53943076/aws-services-enable-another-region",
                    title = "AWS services- enable another region",
                    questionID = "53943076",
                    source = "Stackoverflow",
                    score = 1,
                    viewCount = 10,
                    answerCount=1
                };

                listQuery.Add(question);

                var question1 = new QuerySearchFields
                {
                    isAnswered = true,
                    lastActivityDate = DateTime.Now.AddDays(-3),
                    creationDate = DateTime.Now.AddDays(-4),
                    lastEditDate = DateTime.Now.AddDays(-1),
                    link = "https://meta.stackexchange.com/questions/319274/responsive-design-themes-what-can-sites-customize-and-how-can-they-get-changes?cb=1",
                    title = "Responsive Design Themes - What can sites customize and how can they get changes implemented?",
                    questionID = "319274",
                    source = "Stackoverflow",
                    score = 3,
                    viewCount = 5,
                    answerCount = 2
                };

                listQuery.Add(question1);

                var question2 = new QuerySearchFields
                {
                    isAnswered = true,
                    lastActivityDate = DateTime.Now,
                    creationDate = DateTime.Now.AddDays(-3),
                    lastEditDate = DateTime.Now.AddDays(-3),
                    link = "https://meta.stackexchange.com/questions/312365/rollout-of-new-network-site-themes?noredirect=1&lq=1",
                    title = "Rollout of new network site themes",
                    questionID = "312365",
                    source = "Stackoverflow",
                    score = 4,
                    viewCount = 8,
                    answerCount = 2
                };

                listQuery.Add(question2);

                var question3 = new QuerySearchFields
                {
                    isAnswered = false,
                    lastActivityDate = DateTime.Now.AddDays(-12),
                    creationDate = DateTime.Now.AddDays(-20),
                    lastEditDate = DateTime.Now.AddDays(-15),
                    link = "https://meta.stackexchange.com/questions/312180/favorite-tags-is-now-tag-watching?noredirect=1&lq=1",
                    title = "'Favorite Tags' is now 'Tag Watching'",
                    questionID = "312180",
                    source = "Stackoverflow",
                    score = 1,
                    viewCount = 3,
                    answerCount = 1
                };

                listQuery.Add(question3);

                var question4 = new QuerySearchFields
                {
                    isAnswered = false,
                    lastActivityDate = DateTime.Now,
                    creationDate = DateTime.Now.AddDays(-2),
                    lastEditDate = DateTime.Now.AddDays(-1),
                    link = "https://worldbuilding.stackexchange.com/questions/134675/how-can-santa-defend-himself",
                    title = "How can Santa defend himself?",
                    questionID = "134675",
                    source = "Stackoverflow",
                    score = 13,
                    viewCount = 50,
                    answerCount = 49
                };

                listQuery.Add(question4);
            }
            catch(Exception ex)
            {

            }

            return listQuery;
        }
    }

    public class QuerySearchFields
    {
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