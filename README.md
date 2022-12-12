# HqvKodtest

Hej,

Här är mitt bidrag till kodprovet.

Det var mycket olika verktyg som jag använt för första gången då jag inte tidigare skrivit WPF, använt asynkrona anrop i klientapplikationer eller skrivit enhetstester tidigare.

Jag har gjort mitt bästa med att läsa på hur detta fungerar dock och jag hoppas att det visar att jag är villig att lära mig och att jag också kan hitta svar på saker som jag inte kunnat tidigare.

Avsteg som inte nämns i koden:
* Jag är osäker på hur mycket av WPF-koden som kan klassas som MVVM men jag har iaf kopplat datat till kontextet.
* Concurrent Queue var något jag tittade på för att kunna ge stöd för flertrådad datahantering men som avstyrdes.
* JSON-schemat fick jag inte att validera korrekt så det fick jag släppa.
* Loggning var inte en del av uppgiften och implementerades inte heller.
* Grundläggande felhantering visas nu som MessageBoxes för användaren. Skulle kunna loggas.
* Jag labbade med Dependency Injection för hjälpklasserna men jag hade behövt lägga mer tid för att få PeriodicTimer att funka. Även xUnit-testerna stökade med File-hjälpklassen som Dependency Injection så det fick också bli ett avsteg. https://github.com/RobThree/ITimer/blob/main/ITimer/PeriodicTimer.cs
* Då jag under mina 12 år på något magiskt sätt aldrig sett ett enhetstest live och alla mina googlingar endast leder till "assert 4+8=12"-liknande exempel har jag avgränsat mig till att göra ett par enkla tester av FileHelper-klassens metoder.
