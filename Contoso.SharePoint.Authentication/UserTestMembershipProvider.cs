using System;
using System.Web.Security;

namespace Contoso.SharePoint.Authentication
{
    public sealed class UserTestMembershipProvider : MembershipProvider
    {
        public override bool EnablePasswordRetrieval { get { return true; } }

        public override bool EnablePasswordReset { get { return true; } }

        public override bool RequiresQuestionAndAnswer { get { return false; } }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts { get { return 5; } }

        public override int PasswordAttemptWindow { get { return 10; } }

        public override bool RequiresUniqueEmail { get { return true; } }

        public override MembershipPasswordFormat PasswordFormat { get { return MembershipPasswordFormat.Hashed; } }

        public override int MinRequiredPasswordLength { get { return 1; } }

        public override int MinRequiredNonAlphanumericCharacters { get { return 0; } }

        public override string PasswordStrengthRegularExpression { get { return string.Empty; } }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return true;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return true;
        }

        private MembershipUser GetTestUser(string username, string email)
        {
            return new MembershipUser(this.Name, username, new Guid("5722C38A-4221-41EA-96B0-4D019CCE646F"), email, string.Empty, string.Empty, true, false, DateTime.Today, DateTime.Now, DateTime.Now, DateTime.Today, new DateTime());
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            status = MembershipCreateStatus.Success;
            return GetUser(username, false);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            return true;
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var collection = new MembershipUserCollection();

            if (emailToMatch == "nouser@contoso.com")
            {
                totalRecords = 0;
                return collection;
            }

            collection.Add(GetTestUser(emailToMatch.Split('@')[0], emailToMatch));

            totalRecords = 1;
            return collection;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var collection = new MembershipUserCollection();

            if (usernameToMatch == "nouser")
            {
                totalRecords = 0;
                return collection;
            }

            collection.Add(GetTestUser(usernameToMatch, $"{usernameToMatch}@contoso.com"));

            totalRecords = 1;
            return collection;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            var collection = new MembershipUserCollection();
            collection.Add(GetTestUser("spFarmAcc", "spFarmAcc@contoso.com"));

            totalRecords = 1;
            return collection;
        }

        public override int GetNumberOfUsersOnline()
        {
            return 1;
        }

        public override string GetPassword(string username, string answer)
        {
            return "SharePoint02!";
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return GetTestUser("spFarmAcc", "spFarmAcc@contoso.com");
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            if (username == "nouser")
            {
                return null;
            }

            return GetTestUser(username, $"{username}@contoso.com");
        }

        public override string GetUserNameByEmail(string email)
        {
            return email.Split('@')[0];
        }

        public override string ResetPassword(string username, string answer)
        {
            return GetPassword(username, answer);
        }

        public override bool UnlockUser(string userName)
        {
            return true;
        }

        public override void UpdateUser(MembershipUser user)
        { }

        public override bool ValidateUser(string username, string password)
        {
            return true;
        }
    }
}
