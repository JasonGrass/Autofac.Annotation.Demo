using Autofac.Annotation.Demo.Repos;

namespace Autofac.Annotation.Demo.Services
{
    [Component(AutofacScope = AutofacScope.SingleInstance)]
    public class RecordService : IRecordService
    {
        [Autowired]
        private IRecordRepository Repository { get; set; }

        public void Record()
        {
            Repository.Record();
        }
    }
}