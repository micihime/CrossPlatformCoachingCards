using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Windows.Input;

namespace CoachingCards.ViewModels
{
    public class SubscribeViewModel : BaseViewModel
    {
        #region CONSTANTS

        private const string apiUrl = "https://api.mailerlite.com/api/v2/subscribers";
        private const string apiToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiI0IiwianRpIjoiYzg2NDM3Y2Q4MDYwOTU0NGY1YjVmMjBlMDI3NjMwNTc5ZTExZTYyZmU1ZDA1MDU2ZmJjYzIxZGNmZjc0NDkyZGFlODNiOTFiZWI1N2ZmMjgiLCJpYXQiOjE2NTI2OTE2MDEuNDkwMTI1LCJuYmYiOjE2NTI2OTE2MDEuNDkwMTMsImV4cCI6NDgwODM2NTIwMS40ODQyMzMsInN1YiI6IjU2ODM4Iiwic2NvcGVzIjpbXX0.tyRAyXul_26ps_dwHAkWQYaf5DBHQ6OH9KiSbIgrZTo5DiByU1uSiG70jrbJbd206LAHcf4gQkyNtteiTUtvGUqNqnq_luX5F5IWyuNLYWBewKOw2J-u1-qpKuS-STZXp22yOXGhFr5nPcfM7gu2JAUNGIrKmKUG8qIY9TivtrCv1wCadAPGprnp4jCU-kgNg2s4a33AISKNNCGhPFQ3B_nrtjDm4JtAV3UyxeZjU4vVaObXx3nBKF_--T8EE-rf2PIW2bgRtV0Vj8NioP66YohY9RZmaLWFGaKpkGILp2OghvJfP2WzySJ3myj-rjXjabcDrTAW7A9_3lY_XDHbvoLBbgfLVT290KH4nv0gkLLTmfL4D4V40ST_Gh2GHUQRj5TuOhzdoHnQnOMg8WDptfGFIWhTL6B2SkqdmXZMgSIoBndnT7PyetG3LSdImxsO4v9Ivn2gwDLu_Q6r7z2UvX73Z5meTXYNMGKyC0RG_iOdisYL5jKcOu-QEF8WqvHq0Dn5YBkfUpseeYoypes-FI2LM4gXrgLEAn_D-pilJhDXJHWh61NFu1kmpaEYJ5QG9XqZ4AystIHxxrL6kmQIHKzlg_9MBvSaL3R7X9aI4WxXYUUpXhU9eax24ug8tINbBG60lbRB4O-4mH04nvQATM-qsRghYCh_c38zR-vE6Xs";
        #endregion

        #region PRIVATE FIELDS

        private string name;
        private string email;
        #endregion

        #region PROPERTIES

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        #endregion

        #region DECLARING COMMANDS

        public ICommand SubscribeCommand { get; }
        #endregion

        public SubscribeViewModel()
        {
            SubscribeCommand = new Command(Subscribe);
        }

        public void Subscribe()
        {
            //var client = new RestClient(apiUrl);
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("x-mailerlite-apikey", apiToken);
            //request.AddHeader("content-type", "application/json");
            //request.AddParameter("application/json", "{\"email\":\"demo@mailerlite.com\", \"name\": \"John\", \"fields\": {\"company\": \"MailerLite\"}}", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
        }
    }
}