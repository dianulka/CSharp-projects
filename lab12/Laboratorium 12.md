# Laboratorium 12: Podstawy programowania reaktywnego w C# przy użyciu Rx
## Programowanie zaawansowane 2

Utwórz dwa źródła szeregów czasowych (IObservable<double>):
- Zwracające co 1 sekundę kolejne wartości funkcji sinus (proszę modyfikować argument o 0.01 przy każdej emisji).
- Zwracające co 1 sekundę losowe wartości rzeczywiste z przedziału [-1,1].

Zacznij obserwować pierwsze ze źródeł filtrując wartości w ten sposób, aby pozostawiać jedynie wartości w przedziale [0,0.3]. Wypisuj sukcesywnie obserwowane wartości do konsoli.

Zacznij obserwować drugie źródło znajdując wartości maksymalne spośród tych, które zostały do tej pory wygenerowane (użyj Scan).  Wypisuj sukcesywnie obserwowane maksymalne wartości do konsoli.

Zacznij obserwować połączone strumienie z obu źródeł. Wypisuj sukcesywnie obserwowane  wartości do konsoli.

Wszystkie obserwacje mają być wypisywanie współbieżnie.

Po 20 sekundach zakończ emisję źródeł. Każdy obiekt obserwujący ma otrzymać od obiektu obserwowane informację o zakończeniu obserwacji i wypisać ten fakt do konsoli.
