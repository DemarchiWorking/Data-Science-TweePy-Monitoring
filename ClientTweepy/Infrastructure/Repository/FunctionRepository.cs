using Dapper;
using Domain.Model.Dao;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repository
{
    public class FunctionRepository : IFunctionRepository
    {

        private readonly ILogger _logger;
        private readonly string _connectionString;
        private readonly MySqlConnection _connection;

        public FunctionRepository
            (
                ILogger logger,
                IConfiguration configuration
            )
        {
                _logger = logger;
                _connectionString = configuration.GetConnectionString("conexaoMySQL");
                _connection = new MySqlConnection(_connectionString);
        }
      
        public Response TopFollowersUsersList()
        {
            try
            {

                string sql = $@"                                
                               SELECT DISTINCT idUser, username, followersCount,followedCount FROM `user` u
                                  GROUP BY u.idUser, u.username, u.followersCount, u.followedCount
                                  ORDER BY followersCount 
                                  DESC LIMIT 5;";
                
                var result = _connection.Query<dynamic>(sql);

                if (result != null && result.ToList().Count > 0)
                {
                    List<User> list = new List<User>();

                    result.ToList<dynamic>().ForEach(it =>
                    {                       
                            list.Add(new User
                            {
                                idUser = it.idUser,
                                username = it.username,
                                followersCount = it.followersCount,
                                followedCount = it.followedCount
                            });
                    });

                    return new Response()
                    {
                        List = list,
                        isSuccess = true                       
                    };
                }
                else if (result != null && result.ToList().Count == 0)
                {
                    return new Response()
                    {
                        isEmpty = true,
                        isSuccess = true
                    };

                }
                else
                {
                    return new Response()
                    {
                        isSuccess = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FunctionRepository] Exception in TopFollowersUsersList!");
            }

            return null;
        }

        public Response TweetsGroupedByHour()
        {
            try
            {

                string sql = $@"                  
                                SELECT HOUR(createdAt) AS HOUR, COUNT(*) AS COUNT
                                                        FROM tweet t 
                                                        GROUP BY HOUR(createdAt)";

                var result = _connection.Query<dynamic>(sql);

                if (result != null && result.ToList().Count > 0) 
                {
                    List<TweetsPerHour> list = new List<TweetsPerHour>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        list.Add(new TweetsPerHour
                        {
                            hour = Convert.ToInt32(it.HOUR),
                            count = Convert.ToInt32(it.COUNT)
                        });
                    });

                    return new Response()
                    {
                        List = list,
                        isSuccess = true
                    };
                }
                else if (result != null && result.ToList().Count == 0)
                {
                    return new Response()
                    {
                        isEmpty = true,
                        isSuccess = true
                    };

                }
                else
                {
                    return new Response()
                    {
                        isSuccess = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FunctionRepository] Exception in PostsGroupedByHour!");
            }

            return null;
        }
        public Response NumberTweetPerLanguage()
        {
            try
            {
                string sql = $@"                  
                               SELECT t.lang AS LANG,t.tag AS TAG,
	                                                    COUNT(t.tag) as COUNT
	                                                    FROM tweet t
	                                                    GROUP BY t.lang, t.tag";



                var result = _connection.Query<dynamic>(sql);

                if (result != null && result.ToList().Count > 0)
                {
                    List<CountTweetPerTagAndLang> list = new List<CountTweetPerTagAndLang>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        list.Add(new CountTweetPerTagAndLang
                        {
                            tag = Convert.ToString(it.TAG),
                            lang = Convert.ToString(it.LANG),
                            count = Convert.ToInt32(it.COUNT)
                        });
                    });

                    return new Response()
                    {
                        List = list,
                        isSuccess = true
                    };
                }
                else if (result != null && result.ToList().Count == 0)
                {
                    return new Response()
                    {
                        isEmpty = true,
                        isSuccess = true
                    };

                }
                else
                {
                    return new Response()
                    {
                        isSuccess = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FunctionRepository] Exception in NumberTweetPerLanguage!");
            }

            return null;
        }
        
        public Response InsertDbTopFollowersUsersList(User user, int rank)
        {
            try
            {
                string query = $@"
                                   INSERT INTO TOP_USERS
                                     (
                                                    ID_USER
                                                    , USERNAME
                                                    , FOLLOWERS
                                                    , FOLLOWING
                                                    , RANK
                                                    )
                                                    VALUES
                                                    (
                                                    '{Convert.ToString(user.idUser)}'
                                                    , '{Convert.ToString(user.username)}'
                                                    , '{Convert.ToInt32(user.followersCount)}'
                                                    , '{Convert.ToInt32(user.followedCount)}'
                                                    , '{Convert.ToInt32(rank)}'
                                      )";

                var result = _connection.Execute(query);

                if (result != 0)
                {
                    _logger.Information($"[TopFollowersUsersList] Request [DBRegister]!");
                    return new Response()
                    {
                        isSuccess = true
                    };
                }
                else
                {
                    return new Response()
                    {
                        isSuccess = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FunctionRepository] Exception in InsertDbTopFollowersUsersList!");

            }

            return null;
        }
        public Response InsertDbTweetsGroupedByHour(TweetsPerHour tweetsPerHour)
        {
            try
            {
                string query = $@"
                                   INSERT INTO TWEETS_PER_HOUR
                                     (
                                                    HOUR
                                                    , COUNT_TWEETS
                                                    )
                                                    VALUES
                                                    (
                                                    '{tweetsPerHour.hour}'
                                                    , '{tweetsPerHour.count}'
                                      )";

                var result = _connection.Execute(query);

                if (result != 0)
                {
                    _logger.Information($"[InsertDbTweetsGroupedByHour] Request [DBRegister]!");
                    return new Response()
                    {
                        isSuccess = true
                    };
                }
                else
                {
                    return new Response()
                    {
                        isSuccess = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FunctionRepository] Exception in InsertDbTweetsGroupedByHour!");

            }

            return null;
        }


        public Response InsertDbNumberTweetPerLanguage(CountTweetPerTagAndLang countTweetPerTagAndLang)
        {
            try
            {
                string query = $@"              
                                INSERT INTO NUMBER_TWEET_PER_LANGUAGE
                                     (
                                                    TAG
                                                    , LANG
                                                    , COUNT_TWEET
                                                    )
                                                    VALUES
                                                    (
                                                    '{Convert.ToString(countTweetPerTagAndLang.tag)}'
                                                    , '{Convert.ToString(countTweetPerTagAndLang.lang)}'
                                                    , '{Convert.ToInt32(countTweetPerTagAndLang.count)}'
                                      )";

                var result = _connection.Execute(query);

                if (result != 0)
                {
                    _logger.Information($"[InsertDbNumberTweetPerLanguage] Request [DBRegister]!");
                    return new Response()
                    {
                        isSuccess = true
                    };
                }
                else
                {
                    return new Response()
                    {
                        isSuccess = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FunctionRepository] Exception in InsertDbNumberTweetPerLanguage!");

            }

            return null;
        }
    }

}

