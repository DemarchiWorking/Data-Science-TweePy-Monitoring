using Application.Service.Interfaces;
using Domain.Model.Dao;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Serilog;
using System;

namespace Application.Service
{
    public class FunctionService : IFunctionService
    {
        private readonly ILogger _logger;
        private readonly IFunctionRepository _functionRepository;

        public FunctionService(
            ILogger logger,
            IFunctionRepository functionRepository
            )
        {
            _logger = logger;
            _functionRepository = functionRepository;
        }

        public Response TopFollowersUsersList()
        {
            try
            {
                var response = _functionRepository.TopFollowersUsersList();

                var rank = 1;
                foreach (var item in response.List)
                {
                    User payload = new User();
                    payload.idUser = Convert.ToString(item.idUser);
                    payload.username = Convert.ToString(item.username);
                    payload.followersCount = Convert.ToInt32(item.followersCount);
                    payload.followedCount = Convert.ToInt32(item.followedCount);

                    var insertDb = _functionRepository.InsertDbTopFollowersUsersList(payload, rank);
                    rank++;
                }

                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FunctionService] Exception in TopFollowersUsersList!");
            }
            return null;
        }
        public Response TweetsGroupedByHour()
        {
            try
            {
                var response = _functionRepository.TweetsGroupedByHour();

                foreach (var item in response.List)
                {
                    TweetsPerHour payload = new TweetsPerHour();
                    payload.hour = Convert.ToInt32(item.hour);
                    payload.count = Convert.ToInt32(item.count);

                    var insertDb = _functionRepository.InsertDbTweetsGroupedByHour(payload);
                }

                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FunctionService] Exception in TweetsGroupedByHour!");
            }
            return null;
        }

        public Response NumberTweetPerLanguage()
        {
            try
            {
                var response = _functionRepository.NumberTweetPerLanguage();

                foreach (var item in response.List)
                {

                    CountTweetPerTagAndLang payload = new CountTweetPerTagAndLang();
                    payload.tag = Convert.ToString(item.tag);
                    payload.lang = Convert.ToString(item.lang);
                    payload.count = Convert.ToInt32(item.count);

                    var insertDb = _functionRepository.InsertDbNumberTweetPerLanguage(payload);
                }

                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FunctionService] Exception in NumberTweetPerLanguage!");
            }
            return null;
        }
    }
}
