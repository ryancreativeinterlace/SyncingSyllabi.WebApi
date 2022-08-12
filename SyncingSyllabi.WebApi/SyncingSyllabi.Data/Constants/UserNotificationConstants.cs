using SyncingSyllabi.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Constants
{
    public class UserNotificationConstants
    {
        public static IField[] All
        {
            get
            {
                return new IField[]
                {
                    new UserNotificationFieldsIds()
                };
            }
        }

        public interface IField
        {
            int[] All();
            int ObjectId();
            string GetFieldName(int fieldId);
            string GetFieldValue(int fieldId, UserNotificationModel userNotification);
        }

        public class ObjectIds
        {
            public const int UserNotificationFieldsIds = 1;
        }

        public class UserNotificationFieldsIds : IField
        {
            public const int Id = 1;
            public const int UserId = 2;
            public const int Title = 3;
            public const int Message = 4;
            public const int DateCreated = 5;
            public const int DateUpdated = 6;
            public const int NotificationStatus = 7;
            public const int NotificationStatusName = 8;
            public const int IsActive = 9;
            public const int IsRead = 10;

            public int[] All()
            {
                return new int[]
                {
                    Id,
                    UserId,
                    Title,
                    Message,
                    DateCreated,
                    DateUpdated,
                    NotificationStatus,
                    NotificationStatusName,
                    IsActive,
                    IsRead
                };
            }

            public List<int> UserNoficaitionFields()
            {
                return new List<int>
                {
                    Id,
                    UserId,
                    Title,
                    Message,
                    DateCreated,
                    DateUpdated,
                    NotificationStatus,
                    NotificationStatusName,
                    IsActive,
                    IsRead
                };
            }

            public int ObjectId()
            {
                return ObjectIds.UserNotificationFieldsIds;
            }

            public static string GetName(int id)
            {
                foreach (var field in typeof(UserNotificationFieldsIds).GetFields())
                {
                    if ((int)field.GetValue(null) == id)
                        return field.Name;
                }
                return "";
            }

            public string GetFieldName(int fieldId)
            {
                switch (fieldId)
                {
                    case Id: return "UserNotificationId";
                    case UserId: return "UserId";
                    case Title: return "Title";
                    case Message: return "Message";
                    case DateCreated: return "DateCreated";
                    case DateUpdated: return "DateUpdated";
                    case NotificationStatus: return "NotificationStatus";
                    case NotificationStatusName: return "NotificationStatusName";
                    case IsActive: return "Active";
                    case IsRead: return "Read";

                    default: return "Unknown";
                }
            }

            public string GetFieldValue(int fieldId, UserNotificationModel userNotification)
            {
                switch (fieldId)
                {
                    case Id: return userNotification.Id.ToString();
                    case UserId: return userNotification.UserId.ToString();
                    case Title: return userNotification.Title.ToString();
                    case Message: return userNotification.Message.ToString() ?? string.Empty;
                    case DateCreated: return userNotification.DateCreated.ToString("M/d/yyyy h:mm:ss tt") ?? string.Empty;
                    case DateUpdated: return userNotification.DateUpdated.ToString("M/d/yyyy h:mm:ss tt") ?? string.Empty;
                    case NotificationStatus: return userNotification.NotificationStatus.ToString();
                    case NotificationStatusName: return userNotification.NotificationStatusName.ToString();
                    case IsActive: return userNotification.IsActive.ToString() ?? string.Empty;
                    case IsRead: return userNotification.IsRead.ToString() ?? string.Empty;

                    default: return string.Empty;
                }
            }
        }
    }
}
