**Cos'è un indirizzo IP, e a cosa serve? Se un nodo della rete vuole mettere in esecuzione 2 o più servizi software accessibili  dall'esterno, conoscere l'indirizzo IP del nodo è sufficiente per comunicare con i suoi servizi?** 

L'indirizzo IP è una sequenza di quattro byte ciascuno rappresentante un nomero intero, vengono separati da punto. Servono per identificare univocamente un nodo della rete. 
No, non è sufficiente: c'è bisogno 


**Cosa rappresentano i codici di risposta della famiglia 400 in una response HTTP? È un codice di "ok"?**

I codici 400 rappresentano un errore lato client. Non sono codici di OK


**Cos’è un metodo HTTP; spiegane la ragione ed elenca i principali.**

Un metodo HTTP è l'identificativo che permette al server di processare la richiesta nel modo corretto.
Corrispondono ad un'azione eseguita lato server.
I principali sono: Get, Post, Put, Delete.


**Un'applicazione web ASP.NET è stata costruita con due middleware, M1 e M2 (in quest'ordine). Si supponga che l'applicazione riceva una request, e M1 non sappia rispondere (cioè non sappia cosa scrivere sulla Response). Cosa succede?**

 - M1 può passare la request al modulo successivo (M2), e quando M2 genera la response, M1 può modificarla prima di restituirla al    client;
 

**In un'applicazione web MVC ho tre ruoli separati: Model, View e Controller.
Se una classe deve occuparsi di accettare o rifiutare chiamate fatte con un certo metodo HTTP piuttosto che un altro... a quale dei tre ruoli appartiene (e perché)?**

La classe in questione è un controller in quanto ha il compito di gestire le chiamate.

**Cosa posso aggiungere nel codice in modo che una chiamata come ad esempio www.miosito.com/Prodotti/Edit/1 possa essere fatta solo con i metodi POST e PUT?**

Un attributo.


**Cos'è un ViewModel, e che scopo ha dentro il pattern MVC.**

Un ViewModel all'interno del pattern MVC è una classe con la responsabilità di mostrare graficamente i risultati al client. Solitamente questo avviene attraverso una pagina Razor.

**Si supponga che l'id passato come parametro dalla Request non sia presente in nessun Product della collezione Products del DbContext.
Delle due versioni del controller, qual è la migliore? Perché?**

La seconda, in quanto gestisce l'eccezione e ritorna l'errore opportuno.
**Che differenza c'è tra usare First() o FirstOrDefault() in LINQ?**

`First()` lancia eccezione se nessun elemento della collezione rispetta il predicato.
`FirstOrDefault()` ritorna il default<T> nella medesima circostanza.
  
**Quale status code viene restituito nella risposta HTTP nei due casi?**
  
Nel primo caso un server error della famiglia 5##. Nel secondo un 404(Not Found) se il codice è attendibile.

**Un manager ti dice: _"Dobbiamo realizzare un'applicazione web in tempi brevissimi, quindi per validare i dati delle pagine web creami soltanto delle funzioni javascript sul client"_.Come rispondi? Spiega.**
 
 Rispondo che non è una buona idea perché è sempre meglio separare le competenze. Il lato client deve occuparsi solo di mostrare le view, tutto la logica applicativa va gestita lato server. Incluse le validazioni. Altrimenti si rischia di esporsi agli attacchi di sicurezza senza adeguate difese/precauzioni.
  
**Nel protocollo HTTPS quale chiave codifica i dati? La pubblica o la privata? E nei sistema di certificati?**
  
  La chiave pubblica. Mentre per il sistema dei cetificati vale il viceversa
  
