1. Projekto pavadinimas

Forumas

2. Sistemos paskirtis

Sistema skirta klausimų/teiginių publikavimui, diskusijoms. Ją sudaro (pagal hierarchinį ryšį) 3 taikomosios srities objektai: sritis(skirta grupuoti įrašus) <- įrašas <- komentaras/atsakymas.

3. Funkciniai reikalavimai

Kiekvienai sričiai bus taikomi 5 sąsajos metodai: CRUD) ir 1 metodas grąžinantis sąrašą.

Forume bus 3 rolės: svečias, narys, administratorius.
Svečias galės:
1. Prisiregistruoti
2. Peržiūrėti sritis
3. Peržiūrėti įrašus
4. Peržiūrėti komentarus/atsakymus

Narys paveldės visus svečio galimus veiksmus ir taip pat galės:
1. Prisijungti
2. Atsijungti
3. Paskelbti įrašą
4. Ištrinti savo įrašą
5. Redaguoti savo įrašą
6. Rašyti komentarus/atsakymus
7. Redaguoti savo komentarus/atsakymus

Administratorius paveldės visus nario galimus veiksmus ir taip pat galės:
1. Šalinti narius
2. Šalinti įrašus
3. Šalinti komentarus/atsakymus
4. Sukurti naują sritį
5. Pašalinti sritį
6. Sukurti naują narį

4. Pasirinktos technologijos

- Kliento pusė (ang. Front-End) – naudojant React.js
- Serverio pusė (angl. Back-End) – naudojant .NET Core. Duomenų bazė – MySQL

Sistemos talpinimui bus naudojamas Azure serveris.
