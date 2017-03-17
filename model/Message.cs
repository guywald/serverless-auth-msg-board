using Amazon.DynamoDBv2.DataModel;

namespace ServerlessAuthMessageBoard.model
{
    public class Message
    {
        [DynamoDBHashKey]  
        public string Id { get; set; }
        public string MessageContent { get; set; }

    }
}