from __future__ import print_function
import boto3
import json
import os

from botocore.exceptions import ClientError

def get(event, context):
    messages_table_name = os.environ['DYNAMODB_MESSAGES_TABLE']
    dynamodb_region = os.environ['DYNAMODB_MESSAGES_TABLE_REGION']

    dynamodb = boto3.resource('dynamodb', region_name=dynamodb_region)
    table = dynamodb.Table(messages_table_name)

    print('Event: '+json.dumps(event))
    message_id = event['pathParameters']['id']

    try:
        response = table.get_item(
            Key={
                'Id': message_id
            }
        )
    except ClientError as e:
        err = e.response['Error']['Message']
        print(err)
        body = {
            "error": err,
            "event": event
        }
        return {
            "statusCode": 500,
            "body": json.dumps(body)
        }
    else:
        item = response['Item']
        return {
            "statusCode": 200,
            "body": json.dumps(item)
        }


