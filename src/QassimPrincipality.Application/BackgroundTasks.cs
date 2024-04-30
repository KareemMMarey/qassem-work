using Framework.Core.BackgroundJobs;

namespace QassimPrincipality.Application
{
    public class BackgroundTasks : IBackgroundTasks
    {
        public BackgroundTasks()
        {
        }

        public void Init()
        {
            RunBackGroundTasks();
        }

        private void RunBackGroundTasks()
        {
        }
    }
}