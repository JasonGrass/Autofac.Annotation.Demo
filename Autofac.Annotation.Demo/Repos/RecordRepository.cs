namespace Autofac.Annotation.Demo.Repos
{
    [Component(AutofacScope = AutofacScope.SingleInstance)]
    public class RecordRepository : IRecordRepository
    {
        public void Record()
        {
            
        }
    }
}