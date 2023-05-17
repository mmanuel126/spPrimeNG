using System;
namespace sportprofiles.Models.Messages
{
    public class SearchMessagesModel    {
        public string Attachement  {get; set; } = string.Empty;
        public string Body  {get; set; } = string.Empty;
        public string ContactName  {get; set; } = string.Empty;
        public string ContactImage  {get; set; } = string.Empty;
        public string SenderImage  {get; set; } = string.Empty;
        public string ContactID  {get; set; } = string.Empty;
        public string FlagLevel  {get; set; } = string.Empty;
        public string ImportanceLevel  {get; set; } = string.Empty;
        public string MessageID  {get; set; } = string.Empty;
        public string MessageState  {get; set; } = string.Empty;
        public string SenderID  {get; set; } = string.Empty;
        public string Subject  {get; set; } = string.Empty;
        public string MsgDate  {get; set; } = string.Empty;
        public string FromID  {get; set; } = string.Empty;
        public string FirstName  {get; set; } = string.Empty;
        public string FullBody  {get; set; } = string.Empty;
        public string SenderTitle { get; set; } = string.Empty;
    }

    public class MessageInfoModel {
		public string MessageID  {get; set; } = string.Empty;
		public string SentDate  {get; set; } = string.Empty;
		public string From  {get; set; } = string.Empty;
		public string SenderPicture  {get; set; } = string.Empty;
		public string Body  {get; set; } = string.Empty;
		public string Subject  {get; set; } = string.Empty;
		public string AttachmentFile  {get; set; } = string.Empty;
        public string OrginalMsg  {get; set; } = string.Empty;
    }
}
