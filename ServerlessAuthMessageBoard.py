from __future__ import print_function
import boto3
import uuid
import json
import os

def create_message(event, context):

    messages_table_name = os.environ['DYNAMODB_MESSAGES_TABLE']
    dynamodb_region = os.environ['DYNAMODB_MESSAGES_TABLE_REGION']

    dynamodb = boto3.resource('dynamodb', region_name=dynamodb_region)
    table = dynamodb.Table(messages_table_name)


    message_id = str(uuid.uuid1())
    try:
        put_response = table.put_item(
            Item = {
                'Id': message_id,
                'MessageContent': event['Body'] if 'Body' in event else 'Event body not found'
            }
        )
    except Exception as e:
        err = e.response['Error']['Message']
        print(err)
        body = {
            "error": err
        }

        return {
            "statusCode": 500,
            "body": json.dumps(body)
        }
    else:
        body = {
            "message_id": message_id
        }

        return {
            "statusCode": 200,
            "body": json.dumps(body)
        }
