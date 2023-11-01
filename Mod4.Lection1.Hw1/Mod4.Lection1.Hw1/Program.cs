namespace Mod4.Lection1.Hw1
{
    internal class Program
    {
        internal static async Task Main()
        {
            //await ReqresApi.GetListUsersAsync();
            await ReqresApi.GetSingleUserAsync();
            await ReqresApi.GetSingleUserNotFound();
            await ReqresApi.GetListRecourceAsync();
            await ReqresApi.GetSingleRecourceAsync();
            await ReqresApi.GetSingleRecourceNotFound();
            await ReqresApi.GetDelayedResponseAsync();

            //await ReqresApi.PostCreateAsync();
            //await ReqresApi.PostRegisterSuccessfulAsync();
            //await ReqresApi.PostRegisterUnsuccessfulAsync();
            //await ReqresApi.PostLoginSuccessfulAsync();
            //await ReqresApi.PostLoginUnsuccessfulAsync();

            //await ReqresApi.PutUpdateAsync();
            //await ReqresApi.PatchUpdateAsync();

            //await ReqresApi.DeleteInfoAsync();
        }
    }
}