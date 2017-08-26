#Serverless authenticated message board example

----

<div align=center>WORK IN PROGRESS</div>

----

### Description
Example of backend [Serverless framework](https://serverless.com/) for an abstract message board. Api Gateway endpoints are authenticated with caller credentials from an AWS Cognito Federated Identity pool. The message board uses AWS ApiGateway with Lambda and DynamoDB to store the messages. Api Gateway endpoints are authenticated with AWS_IAM ("Invoke with caller credentials" option) given by Cognito.

While the serverless.yml defines the Api Gateway endpoints, lambda and dynamodb. You need (as instructed below) to create the Cognito Identity pool manually and set it's IAM role (explained).

Important Note: Running the example has a financial cost, please be aware and see the AWS pricing system for each service. Example not for production use!



### Setting up the AWS environemnt

Install [Serverless Framework](https://serverless.com/) - See [Instructions](https://serverless.com/framework/docs/providers/aws/guide/installation/)


(Also see [Using Federated Identities](http://docs.aws.amazon.com/cognito/latest/developerguide/cognito-identity.html))

Facebook Application:
1. Go to Facebook developers console
2. Create a new app
3. Allow login permission
4. Copy app id

Cognito:

1. <i>Manage Federated Identities</i>
2. Create a new Identity pool
3. Fill up pool name
4. Make sure <i>Enable access to unauthenticated identities</i> is <b>unchecked!</b>
5. In <i>Authenticated providers</i>, in Facebook tab fill the <i>Facebook App ID</i>
6. Create pool
7. Create Default roles.

IAM Role:

1. Go to IAM from the AWS Console
2. Find the role previosuly created for authenticated access.
3. In Managed Policies add:
..A) AmazonAPIGatewayInvokeFullAccess
..B) AWSLambdaExecute
..C) AWSLambdaBasicExecutionRole

### Install the Serverless Framework example

1. Clone the project
2. CD to the project's folder/directory
3. sls deploy

Once the service is set, an endpoint URL is created. Copy the path to the created API and paste it in the client as instructed in the [serverless-auth-msg-board-unity3d-client](https://github.com/guywald/serverless-auth-msg-board-unity3d-client) README.md file.


