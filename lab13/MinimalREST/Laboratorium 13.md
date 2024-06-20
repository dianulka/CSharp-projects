# Laboratorium 13: Zdalne zarządzanie danymi przy pomocy REST api
## Programowanie zaawansowane 2


Napisz program, który przy pomocy REST api umożliwi zdalne zarządzanie danymi, które można pobrać spod adresu:
https://request.openradiation.net/download.html
(https://request.openradiation.net/openradiation_dataset.tar.gz)
Dane znajdują się w pliku measurements_withoutEnclosedObject.csv
- Dane mają być zaimportowane do bazy danych SQLite, ogranicz sie do pierwszych 100 rekordów z pliku oraz pierwszych 10 kolumn. Dla uproszczenia możesz założyć, że wszystkie pola są tekstowe. Możesz dodać dodatkową kolumnę ID typu int, która będzie kluczem głównym.
- Rest API ma wykorzystywać model danych oraz kontroler, który będzie obsługiwać metody GET, POST, GET{id}, PUT{id}, DELETE{id}
- Skonfiguruj Swagger, aby można było przetestować działanie aplikacji

Powodzenia!

ฅ^•ﻌ•^ฅ