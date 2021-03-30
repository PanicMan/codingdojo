using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;

namespace Reactive
{
    public class StringObservable : ObservableBase<string>
    {
        List<IObserver<string>> Observers { get; } = new List<IObserver<string>>();

        public void StringChanged(string str)
        {
            var items = Observers.ToList();
            foreach (var item in items)
            {
                item.OnNext(str);
            }
        }

        public void Finish()
        {
            var items = Observers.ToList();
            foreach (var item in items)
            {
                item.OnCompleted();
            }
        }

        protected override IDisposable SubscribeCore(IObserver<string> observer)
        {
            Observers.Add(observer);

            return Disposable.Create(
                () =>
                {
                    Observers.Remove(observer);
                });
        }
    }
}
