
namespace sportprofiles.Models.Settings
{
    public class NotificationsSettingModel
    {
        public int MemberID { get; set; }
        public bool LG_SendMsg { get; set; }
        public bool LG_AddAsFriend { get; set; }
        public bool LG_ConfirmFriendShipRequest { get; set; }
        public bool GP_InviteYouToJoin { get; set; }
        public bool GP_MakesYouAGPAdmin { get; set; }
        public bool GP_RepliesToYourDiscBooardPost { get; set; }
        public bool GP_ChangesTheNameOfGroupYouBelong { get; set; }
        public bool EV_InviteToEvent { get; set; }
        public bool EV_DateChanged { get; set; }
        public bool HE_RepliesToYourHelpQuest { get; set; }
    }

    public class PasswordData
    {
        public string Pwd { get; set; } = string.Empty;
        public string MemberID { get; set; } = string.Empty;
    }
}


