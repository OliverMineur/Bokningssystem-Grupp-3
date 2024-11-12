# Bokningssystem

○ Hur man startar och använder programmet

  Öppna programmet från Bokningssytem.sln  
  Huvudmeny startas med tre val:
  1.Hantera bokningar för att kunna skapa bokning, uppdatera bokning, ta bort bokning, visa alla bokningar, filtrera alla bokningar
  2.Hantera lokaler för att kunna skapa sal, grupprum, visa alla salar och grupprum
  3.Spara avsluta

○ Eventuella kända begränsningar

  Svensk tidzon

○ Val och motiveringar för implementation

  Vi har implementerat IBookable, Sal, Grupprum och Lokal i bokningssystemet för att få ett fungerande program

  Val av Interface (IBookable)
  Motivering: Genom att använda ett interface kan vi säkerställa att alla bokningsbara objekt har gemensamma metoder och egenskaper. 
  Detta gör koden mer flexibel och lättare att underhålla eftersom vi kan lägga till nya typer av bokningsbara objekt utan att ändra befintlig kod.

  Val av Klasser (Sal och Grupprum)
  Motivering: Genom att skapa specifika klasser för Sal och Grupprum kan vi hantera deras unika egenskaper, som projektortillgänglighet i en Sal och eluttag i ett Grupprum. 
  gör koden mer modulär och lättare att förstå.

  Val av Bas- eller Abstrakt Klass (Lokal)
  Motivering: Genom att ha en bas- eller abstrakt klass Lokal kan vi definiera gemensamma egenskaper och metoder för alla typer av lokaler. 
  Detta minskar kodduplicering och gör det lättare att utöka systemet med nya typer av lokaler i framtiden.

○ Beskrivning av filformat och struktur

  Vi använder JSON (JavaScript Object Notation) för att lagra och överföra data i vårt bokningssystem. 
  JSON är ett lättviktigt datautbytesformat som är lätt för människor att läsa och skriva, och lätt för maskiner att parsa och generera.

○ Vilken student har huvudansvaret för vilka delar
