Jag vill ha feedback på:

Arkitektur och ansvar
Är ansvarsfördelningen mellan "Garage" och "Program" tydlig?
• Jag har lagt mycket meny- och inmatningslogik i Program. Är det rimligt? eller 
borde jag skapa en separat MenuHandler eller UserInterface-klass?”
• Hur kan jag förbättra hanteringen av felaktig inmatning?
• Min Garage-klass hanterar både fordon, kapacitet, sökning och utskrift. Har den 
för många ansvar?

OOP och klassdesign
• Är det tydligt vilka egenskaper som ska ligga i basklassen Vehicle och vilka som 
borde ligga i subklasserna?
• Hur bör jag hantera kapacitet när olika fordon tar olika mycket plats, till exempel 
motorcykel en tredjedels plats och båt två platser?

Garage och List
• Hur bör jag hantera kapacitet när olika fordon tar olika mycket plats, till exempel 
motorcykel en tredjedels plats och båt två platser?

Sökning och filtrering
• Jag söker på registreringsnummer, färg och antal hjul separat. Hur kan jag bygga 
en sökning med flera valfria kriterier?

Felhantering och robusthet
• Hur ska programmet bete sig om någon försöker parkera ett fordon med ett 
registreringsnummer som redan finns?
• Vad händer om garaget är fullt och användaren försöker parkera?
