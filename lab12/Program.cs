using System;
using System.Reactive.Linq;
using System.Timers;
using System.Reactive.Subjects;
using System.Reactive.Disposables;
using System.Linq;

    static IObservable<double> Sinus(TimeSpan dueTime, TimeSpan period)
    {
        return Observable.Generate(
        Math.Sin(0),//stan początkowy
        i => true,//warunek końca
        i => i + 1,//iteracja
        i => Math.Sin(i * 0.01),//kolejna wartość
        i => i == 0 ? dueTime : period//wybór czasu (w tym wypadku po upłynięciu dueTime wartości będą generowane co period)
        );
    }


    static IObservable<double> LiczbyGenerowane(TimeSpan dueTime, TimeSpan period)
    {
        var random = new Random();
        return Observable.Generate(
            0L, // stan początkowy (interwał)
            _ => true, // warunek końca
            i => i + 1, // iteracja
            _ => random.NextDouble() * 2 - 1, // kolejna wartość
            i => i == 0 ? dueTime : period//wybór czasu (w tym wypadku po upłynięciu dueTime wartości będą generowane co period)

        );
    }
    var sinusSource = Sinus(TimeSpan.Zero, TimeSpan.FromSeconds(1));
    var randomSource = LiczbyGenerowane(TimeSpan.Zero, TimeSpan.FromSeconds(1));

    var filteredSinusSource = sinusSource.Where(value => value >= 0 && value <= 0.3);
    var sinusSubscription = filteredSinusSource.Subscribe(
        value => Console.WriteLine($"Wartość sinusa w przedziale [0,0.3]: {value}"),
            () => Console.WriteLine("Sinus completed"));

    var maxRandomSource = randomSource.Scan((max, current) => Math.Max(max, current));
    var randomSubscription = maxRandomSource.Subscribe(
        value => Console.WriteLine($"Maksymalna wartość: {value}"),
        () => Console.WriteLine("Maksymalna completed"));

    var combinedSource = sinusSource.Merge(randomSource);
    var combinedSubscription = combinedSource.Subscribe(
        value => Console.WriteLine($"Połączone strumienie: {value}"),
        () => Console.WriteLine("połączone strumienie completed"));

    
    Thread.Sleep(TimeSpan.FromSeconds(20));

    
    sinusSubscription.Dispose();
    randomSubscription.Dispose();
    combinedSubscription.Dispose();

    Console.WriteLine("Koniec");
