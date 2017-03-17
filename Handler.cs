using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon;
using Newtonsoft.Json;
using ServerlessAuthMessageBoard.model;

namespace ServerlessAuthMessageBoard
{
    public class Handler
    {
        const string MESSAGE_BOARD_ENV_VAR_NAME = "DYNAMODB_MESSAGES_TABLE";
        IDynamoDBContext DDBContext { get; set; }

        public Handler()
        {
            var messagesTableName = Environment.GetEnvironmentVariable(MESSAGE_BOARD_ENV_VAR_NAME);

            if (messagesTableName == null)
                Console.WriteLine("tableName is null");

            if (!string.IsNullOrEmpty(messagesTableName))
            {
                AWSConfigsDynamoDB.Context.TypeMappings[typeof(Message)] = new Amazon.Util.TypeMapping(typeof(Message), messagesTableName);
            }

            var config = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };

            this.DDBContext = new DynamoDBContext(new AmazonDynamoDBClient(), config);
        }


        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> CreateMessage(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var createMessageRequest = JsonConvert.DeserializeObject<CreateMessageRequest>(request?.Body);
            if (createMessageRequest == null)
                Console.WriteLine("Message not deserialized correctly");
                
            var message = new Message()
            {
                Id = Guid.NewGuid().ToString(),
                MessageContent = createMessageRequest.Message
            };

            await DDBContext.SaveAsync<Message>(message);

            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "{}",
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
            return response;
        }
    }
}
