using Domain.Model.Response;

namespace Application.Service.Interfaces
{
    public interface IFunctionService
    {
        Response TopFollowersUsersList();
        Response TweetsGroupedByHour();
        Response NumberTweetPerLanguage();

    }
}