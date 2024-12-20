using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.DI;

namespace FinalGame.Develop.CommonServices.Timer
{
    public class TimerServiceFactory
    {
        private DIContainer _container;


        public TimerServiceFactory(DIContainer container)
        {
            _container = container;
        }

        public TimerService Create(float cooldown) =>
            new TimerService(cooldown, _container.Resolve<ICoroutinePerformer>());
    }
}