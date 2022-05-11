CREATE DATABASE tweetpy CHARACTER SET utf8mb4;
use tweetpy;


CREATE TABLE IF NOT EXISTS tweepy(
                        ID_TWEET INTEGER AUTO_INCREMENT PRIMARY KEY
						, TWEET VARCHAR(900) NOT NULL
						, ID_USER INTEGER
						)


CREATE TABLE IF NOT EXISTS TOP_USERS(
                        ID_TOP_USER INTEGER AUTO_INCREMENT PRIMARY KEY
						, ID_USER VARCHAR(50)
						, USERNAME VARCHAR(150)
						, FOLLOWERS INTEGER
						, FOLLOWING INTEGER	
						, RANK INTEGER
						, CREATED_AT TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP						
						)

CREATE TABLE IF NOT EXISTS TWEETS_PER_HOUR(
                        ID_TWEETS_PER_HOUR INTEGER AUTO_INCREMENT PRIMARY KEY
						, HOUR VARCHAR(50)
						, COUNT_TWEETS INTEGER
						, CREATED_AT TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP						
						)

CREATE TABLE IF NOT EXISTS NUMBER_TWEET_PER_LANGUAGE(
                        ID_NUMBER_TWEET_PER_LANGUAGE INTEGER AUTO_INCREMENT PRIMARY KEY
						, TAG VARCHAR(150)
						, LANG VARCHAR(150)
						, COUNT_TWEET INTEGER
						, CREATED_AT TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP						
						)




