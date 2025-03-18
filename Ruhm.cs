using System.Collections.ObjectModel;

namespace MauiApp1
{
    public class Ruhm<K, T> : ObservableCollection<T>
    {
        public K Nimetus { get; private set; } // Название группы (производитель)

        public Ruhm(K nimetus, IEnumerable<T> items)
        {
            Nimetus = nimetus;
            foreach (T item in items)
            {
                Items.Add(item);
            }
        }
    }
}