using System.Collections.Generic;

namespace HideAndSeek.Utils
{
    public class Subscriptions : ISubscription
    {
        private readonly List<ISubscription> _subscriptions;

        public Subscriptions()
        {
            _subscriptions = new List<ISubscription>();
        }

        public Subscriptions Add(ISubscription subscription)
        {
            _subscriptions.Add(subscription);
            return this;
        }

        public Subscriptions Add(IEnumerable<ISubscription> subscriptions)
        {
            _subscriptions.AddRange(subscriptions);
            return this;
        }

        public void UnsubscribeAll()
        {
            foreach (var subscription in _subscriptions)
            {
                subscription.Unsubscribe();
            }

            _subscriptions.Clear();
        }

        public void Unsubscribe()
        {
            UnsubscribeAll();
        }
    }
}