# KoncertManager - Szoftverfejlesztés .NET platformra HF

## Feladat
Az alkalmazás koncerteket, együtteseket, és helyszíneket tárol egy adatbázisban. A koncerteknek van egy helyszíne, ideje,
maximum 6 együttese, és mutatja, hogy kapható-e rá jegy. Az együtteseknek van neve, származási országa
és alakulási éve, a helyszíneknek van neve, címe és befogadókapacitása. 
Koncertre, együttesre és helyszínre is lehet:
* Kiírni az adatbázisban levő elemeket
* Szerkeszteni egy kiválasztott elemet
* Törölni egy kiválasztott elemet
* Újat létrehozni, koncert esetén a meglévő együttesekből és helyszínekből lehet válogatni

## Technológiák
* Adatelérés: Entity Framework Core v3.1
* Kommunikáció, szerveroldal: ASP.NET Core v3.1
* Kliensoldal: WPF

## Megvalósítás
Az alkalmazás többrétegű. A WPF kliens HTTP kéréseket küld a RESTful szervernek OData v4 alapon, ami egy 
üzleti logikai rétegnek továbbítja a kérést, mely műveleteket hajt végre az adatelérési rétegben
leírt, MySql adatbázison.

## Pontozás a feladat értékeléséhez
* OData szolgáltatás megvalósítása Microsoft.AspNetCore.OData csomaggal, Odata v4 alapon, lekérdezés, létrehozás, módosítás, törlés is - 10 pont
* MySql adatbázis használata LocalDB helyett EFCore-al - 10 pont
* Adatbetöltés (seeding) migráció segítségével (HasData) - 3 pont
* Kifejezésfa (ExpressionTree) értelmezése és manipulálása, kapcsolódó kollekcióban dinamikusan - 10 pont
* Unit tesztek 15 függvényhez, In-Memory SQLite adatbázissal - 11 pont
* ObjectMapper használata DTO-k létrehozására - AutoMapper - 3 pont
* Külső könyvtárak használata - 7 pont
  * Pomelo.EntityFrameWorkCore.MySql
  * XUnit
