import os
from typing import List, Dict
import mysql.connector
# import simplejson as json
from flask import Flask, render_template, Response, redirect, url_for, request
from searchtweets import load_credentials, ResultStream, gen_rule_payload
from searchtweets import collect_results
from datetime import datetime
from flask_bootstrap import Bootstrap5
import tweepy
import requests
from requests.structures import CaseInsensitiveDict
from  flask_sqlalchemy import SQLAlchemy
import json
import re
import datetime
import socket
from opencensus.ext.azure.trace_exporter import AzureExporter
from opencensus.ext.flask.flask_middleware import FlaskMiddleware
from opencensus.trace.samplers import ProbabilitySampler

app = Flask(__name__)
middleware = FlaskMiddleware(
    app,
    exporter=AzureExporter(connection_string='InstrumentationKey=6d69c826-89fb-4275-80b3-f2f4a7f41638;IngestionEndpoint=https://westeurope-5.in.applicationinsights.azure.com/;LiveEndpoint=https://westeurope.livediagnostics.monitor.azure.com/'),
    sampler=ProbabilitySampler(rate=1.0)
)

bootstrap = Bootstrap5(app)

bearer_token = "AAAAAAAAAAAAAAAAAAAAAFrtcAEAAAAAp%2BCUCE7LXUtCgpOxJATpeJE%2F3C0%3DHdVbnoO9BjPdSVOnHIxDxR7IMOMkKLm7Si1GM6E5Lqwqr5Sil1"
consumer_key = "FtiPafgLa5I4T0Y94GoHTLzUD"
consumer_secret = "wHfHwJiC2pdGoHlLJ6tnzDnWuLp0Qbn39Z19D2BJVNJ8g8RTtC"
access_token = "1521978959612719104-DtWsrzojFZInEzXoCvrttzCJsvyVcu"
access_token_secret = "UVi5aGFL3j7Cwous2gsvcbFQ2QN5aqUzw2tT0kppvXiNe"

Client = tweepy.Client(bearer_token, consumer_key, consumer_secret, access_token, access_token_secret)
app.config['SQLALCHEMY_DATABASE_URI'] = 'mysql://root:root@db/tweetpy?charset=utf8mb4'
db = SQLAlchemy(app)

class User(db.Model):                                                       #classe usuário com ORM
    id = db.Column('id', db.Integer, primary_key=True, autoincrement=True)
    idUser = db.Column(db.String(50))
    #idUser = db.Column(db.Integer, ForeignKey ( 'Tweet.idUser' )
    username = db.Column(db.String(150))
    followersCount = db.Column(db.Integer)
    followedCount = db.Column(db.Integer)
    def __init__(self, idUser, username, followersCount, followedCount):
        self.idUser = idUser
        self.username = username
        self.followersCount = followersCount
        self.followedCount = followedCount

class Tweet(db.Model):                                                         #classe tweet com ORM
    id = db.Column('id', db.Integer, primary_key=True, autoincrement=True)
    idUser = db.Column(db.String(50))
    username = db.Column(db.String(100))
    tweet = db.Column(db.String(999))
    createdAt = db.Column(db.DateTime)
    lang = db.Column(db.String(100))
    tag = db.Column(db.String(150))
    def __init__(self, idUser, username, tweet, createdAt, lang, tag):
        self.idUser = idUser
        self.username = username
        self.tweet = tweet
        self.createdAt = createdAt
        self.lang = lang
        self.tag = tag

db.create_all()

def tweetByTagAndDate(tag):
    result =  []                                                                                                                                                                #inserir data futuramente >> #initialDate1 = initialDate.strftime("%Y-%m-%dT%H:%M:%SZ")  #start_date = '2022-05-01T00:00:00Z'#query = Client.search_recent_tweets(query=tag, tweet_fields=['context_annotations', 'created_at','lang'], start_time=start_date, end_time=end_date,#finalDate1 = finalDate.strftime("%Y-%m-%dT%H:%M:%SZ") #end_date = '2022-05-07T15:00:00Z'timeout = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    #timeout.settimeout(5.0)

    query = Client.search_recent_tweets(query=tag, tweet_fields=['context_annotations', 'created_at','lang'],
                                            user_fields=['profile_image_url'], expansions='author_id', max_results=100)

    users = {u["id"]: u for u in query.includes['users']}
    for tweet in query.data:
        if users[tweet.author_id]:
            user = users[tweet.author_id]
            tweet = Tweet(tweet.author_id, str(user), str(tweet.text), tweet["created_at"].strftime('%Y-%m-%d %H:%M:%S'), tweet["lang"], tag)
            db.session.add(tweet)
            db.session.commit()
            result.append(tweet)

    return result

def tweetAndAuthor():
    result =  []
    tagList = ['#openbanking', '#remediation', '#devops', '#sre', '#microservices', '#observability', '#oauth',
               '#metrics', '#logmonitoring', '#opentracing']

    for i in range(9):
        query = Client.search_recent_tweets(query=tagList[i], tweet_fields=['context_annotations', 'created_at','lang'],
                                            user_fields=['profile_image_url'], expansions='author_id', max_results=100)


        users = {u["id"]: u for u in query.includes['users']}
        for tweet in query.data:
            if users[tweet.author_id]:
                user = users[tweet.author_id]
                tweet = Tweet(tweet.author_id, str(user), str(tweet.text), tweet["created_at"].strftime('%Y-%m-%d %H:%M:%S'), tweet["lang"], tagList[i])
                db.session.add(tweet)
                db.session.commit()
                result.append(tweet)

    return result

def userMostFollowers(tweets):
    headers = CaseInsensitiveDict()
    headers["Accept"] = "application/json"
    headers["Authorization"] = "Bearer "+bearer_token
    userInfo = requests.get("https://api.twitter.com/2/users/"+str(tweets.idUser)+"?user.fields=public_metrics",headers=headers)
    return userInfo


@app.route('/lista-sugestao-tweets')
def lastTweetList():
    tweetsByTag = tweetAndAuthor()
    for tweets in tweetsByTag[:50]:
        print(tweets)
        user_info = userMostFollowers(tweets)
        if(str(user_info) == "<Response [429]>"):
            return redirect(url_for('many'))

        user_info = json.loads(user_info.content.decode('utf-8'))
        print(user_info)
        username = user_info['data']['username']
        followersCount = user_info['data']['public_metrics']['followers_count']
        followedCount = user_info['data']['public_metrics']['following_count']

        existUser = User.query.filter_by(idUser=tweets.idUser).first()

        if (existUser is None):
            user = User(tweets.idUser, username, followersCount, followedCount)
            db.session.add(user)

        else:
            existUser.username = username
            existUser.followersCount = followersCount
            existUser.followedCount = followedCount
            User.query.all()

    db.session.commit()

    return render_template("listTweetsBySuggestion.html",user_info=user_info, tweetsByTag=tweetsByTag)

@app.route('/cadastrarTweet', methods=['GET', 'POST'])
def registerTweet():

    if request.method == 'POST':
        tag = request.form.get('tag')
        initialDate = request.form.get('initialDate')
        finalDate = request.form.get('finalDate')
        result =tweetByTagAndDate(tag, initialDate, finalDate)

        #loop nos tweets com requisicao no perfil e cadastro deles no banco
        for tweets in result[:50]:  # só pega os primeiros
            user_info = userMostFollowers(tweets)
            if (str(user_info) == "<Response [429]>"):
                return redirect(url_for('many'))

            user_info = json.loads(user_info.content.decode('utf-8'))
            print(user_info)
            username = user_info['data']['username']
            followersCount = user_info['data']['public_metrics']['followers_count']
            followedCount = user_info['data']['public_metrics']['following_count']

            existUser = User.query.filter_by(idUser=tweets.idUser).first()

            if (existUser is None):
                user = User(tweets.idUser, username, followersCount, followedCount)
                db.session.add(user)

            else:
                existUser.username = username
                existUser.followersCount = followersCount
                existUser.followedCount = followedCount
                User.query.all()

        db.session.commit()

        return render_template("listTweetByTag.html", result=result)

    return render_template("registerTweet.html")

@app.route('/')
def index():
    logger()
    return render_template("index.html")

@app.route('/contatos')
def contact():
    return render_template("contact.html")

@app.route('/funcoes')
def function():
    return render_template("function.html")



@app.route('/many')
def many():
    return render_template("many.html")



def logger():
    app.logger.debug('This is a debug log message.')
    app.logger.info('This is an information log message.')
    app.logger.warning('This is a warning log message.')
    app.logger.error('This is an error message.')
    app.logger.critical('This is a critical message.')

    return "EnviarLog"

@app.route('/error')
def error():
    a = 42
    b = 0
    c =a/b
    return "a / b = {c}".format(c=c)


if __name__ 