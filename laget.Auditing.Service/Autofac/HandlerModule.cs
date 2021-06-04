using Autofac;
using laget.Auditing.Service.Handlers;

namespace laget.Auditing.Service.Autofac
{
    public class HandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RecordHandler>().As<IRecordHandler>().SingleInstance();
        }
    }
}
