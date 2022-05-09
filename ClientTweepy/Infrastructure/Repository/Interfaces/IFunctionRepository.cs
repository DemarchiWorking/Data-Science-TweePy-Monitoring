using Domain.Model.Dao;
using Domain.Model.Response;

namespace Infrastructure.Repository.Interfaces
{
    public interface IFunctionRepository
    {
        Response TopFollowersUsersList();
        Response TweetsGroupedByHour();
        Response NumberTweetPerLanguage();

        Response InsertDbNumberTweetPerLanguage(CountTweetPerTagAndLang countTweetPerTagAndLang);
        Response InsertDbTweetsGroupedByHour(TweetsPerHour tweetsPerHour);
        Response InsertDbTopFollowersUsersList(User user, int rank);


    }
}
