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

##Technológiák
* Adatelérés: Entity Framework Core v3.1
* Kommunikáció, szerveroldal: ASP.NET Core v3.1
* Kliensoldal: WPF

## Megvalósítás
Az alkalmazás többrétegű. A WPF kliens HTTP kéréseket küld a RESTful szervernek, ami egy 
üzleti logikai rétegnek továbbítja a kérést, mely műveleteket hajt végre az adatelérési rétegben
leírt, localDB adatbázison.
