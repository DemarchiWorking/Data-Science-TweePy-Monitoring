using Application.Service.Interfaces;
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
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FunctionService] Exception in PostsGroupedByHour!");
            }
            return null;
        }

        public Response NumberTweetPerLanguage()
        {
            try
            {
                var response = _functionRepository.NumberTweetPerLanguage();
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
