# Hotel
# HotelManager - Dokumentacja

## Opis projektu
HotelManager to aplikacja napisana w ASP.NET Core MVC, której celem jest zarządzanie hotelami, w tym klientami, pokojami oraz rezerwacjami. Projekt ten umożliwia:
- Tworzenie nowych rezerwacji
- Zarządzanie klientami
- Zarządzanie pokojami i ich dostępnością

Repozytorium projektu: [HotelManager na GitHubie](https://github.com/Pawolik/HotelManager)

## HotelService
W ramach aplikacji HotelManager zaimplementowano klasę `HotelService`, która zawiera poniższe metody do zarządzania klientami, pokojami i rezerwacjami.

## Klasa Hotel

### Metody:
#### Dodanie nowego klienta do hotelu
- **Opis:** Tworzy nowego klienta i dodaje go do hotelu.
- **Sygnatura:** `public int AddClient(string firstName, string lastName, DateTime dateOfBirth)`
- **Zwracana wartość:** Identyfikator klienta `clientId`.

#### Pobranie danych klienta
- **Opis:** Zwraca imię i nazwisko klienta na podstawie podanego `clientId`.
- **Sygnatura:** `public string GetClientName(int clientId)`
- **Wyjątki:** `ClientNotFoundException` - jeśli klient nie istnieje.

#### Liczba niepełnoletnich klientów
- **Opis:** Zwraca liczbę niepełnoletnich klientów w hotelu.
- **Sygnatura:** `public int GetUnderageClientCount()`

#### Dodanie nowego pokoju
- **Opis:** Tworzy nowy pokój i dodaje go do hotelu.
- **Sygnatura:** `public int AddRoom(double area, bool hasKingSizeBed, int floor)`
- **Zwracana wartość:** Identyfikator pokoju `roomId`.

#### Powierzchnia pokoju
- **Opis:** Zwraca powierzchnię pokoju na podstawie `roomId`.
- **Sygnatura:** `public double GetRoomArea(int roomId)`
- **Wyjątki:** `RoomNotFoundException` - jeśli pokój nie istnieje.

#### Liczba pokoi z dużym łóżkiem na piętrze
- **Opis:** Zwraca liczbę pokoi z dużym łóżkiem (`king size bed`) na określonym piętrze.
- **Sygnatura:** `public int GetRoomsWithKingSizeBedOnFloor(int floor)`

#### Tworzenie rezerwacji
- **Opis:** Tworzy i zapisuje nową rezerwację dla klienta i pokoju na wybrany dzień.
- **Sygnatura:** `public int CreateReservation(int clientId, int roomId, DateTime date)`
- **Zwracana wartość:** Identyfikator rezerwacji `reservationId`.
- **Wyjątki:**
  - `ClientNotFoundException` - jeśli klient nie istnieje.
  - `RoomNotFoundException` - jeśli pokój nie istnieje.
  - `RoomReservedException` - jeśli pokój jest już zarezerwowany w wybranym dniu.

#### Sprawdzenie rezerwacji pokoju
- **Opis:** Zwraca informację, czy wybrany pokój jest zarezerwowany w danym dniu.
- **Sygnatura:** `public bool IsRoomReserved(int roomId, DateTime date)`
- **Wyjątki:** `RoomNotFoundException` - jeśli pokój nie istnieje.

#### Liczba niepotwierdzonych rezerwacji
- **Opis:** Zwraca liczbę niepotwierdzonych rezerwacji w danym dniu.
- **Sygnatura:** `public int GetUnconfirmedReservationsCount(DateTime date)`

#### Identyfikatory pokoi zarezerwowanych przez klienta
- **Opis:** Zwraca unikalne identyfikatory pokoi, które kiedykolwiek zostały zarezerwowane przez danego klienta.
- **Sygnatura:** `public IEnumerable<int> GetClientReservedRooms(int clientId)`
- **Wyjątki:** `ClientNotFoundException` - jeśli klient nie istnieje.

## Wyjątki
- **`ClientNotFoundException`**: Wyrzucany, gdy klient o podanym `clientId` nie istnieje.
- **`RoomNotFoundException`**: Wyrzucany, gdy pokój o podanym `roomId` nie istnieje.
- **`RoomReservedException`**: Wyrzucany, gdy pokój jest już zarezerwowany w wybranym dniu.

## Wymagania techniczne
- **Język programowania:** C#
- **Framework:** .NET 8 lub nowszy
- **Baza danych:** MS SQL Server
- **Biblioteka ORM:** Entity Framework Core
- **Kod źródłowy:** [HotelManager na GitHubie](https://github.com/Pawolik/HotelManager)

## Uwagi implementacyjne
Podczas implementacji metod zaleca się:
1. Używanie wzorca Repository do zarządzania danymi w bazie danych.
2. Walidację danych wejściowych (np. sprawdzenie poprawności formatów dat).
3. Testowanie jednostkowe każdej metody w `HotelService`.

## Autor
Twórcą projektu jest **Pawolik** ([GitHub](https://github.com/Pawolik)).

