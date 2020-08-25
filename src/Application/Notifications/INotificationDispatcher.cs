namespace Aviant.DDD.Application.Notifications
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INotificationDispatcher
    {
        #region Pre Commit Notifications

        List<INotification> GetPreCommitEvents();

        void AddPreCommitNotification(INotification notification);

        void RemovePreCommitNotification(INotification notification);

        Task FirePreCommitNotifications();

        #endregion

        #region Post Commit Notifications

        List<INotification> GetPostCommitNotifications();

        void AddPostCommitNotification(INotification notification);

        void RemovePostCommitNotification(INotification notification);

        Task FirePostCommitNotifications();

        #endregion
    }
}