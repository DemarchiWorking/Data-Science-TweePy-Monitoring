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
                    List<AmountPerHour> list = new List<AmountPerHour>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        list.Add(new AmountPerHour
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

    }

}

