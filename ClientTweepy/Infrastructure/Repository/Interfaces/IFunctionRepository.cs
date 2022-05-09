using Domain.Model.Response;

namespace Infrastructure.Repository.Interfaces
{
    public interface IFunctionRepository
    {
        Response TopFollowersUsersList();
        Response TweetsGroupedByHour();
        Response NumberTweetPerLanguage();


    }
}
