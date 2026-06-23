using System;
using System.Collections.Generic;
using System.Linq;

/*
 * TEMPLATE ESAME C# - NEGOZIO ONLINE
 *
 * Regola scelta per il template:
 * - i metodi di visualizzazione sono già implementati, così lo studente può concentrarsi
 *   sulle operazioni richieste dalla traccia.
 * - i metodi operazionali contengono TODO guidati: lo studente deve completarli senza
 *   modificare firma, nome, parametri o tipo di ritorno.
 *
 * Vincolo richiesto: tutto il codice è in un unico file .cs e senza namespace.
 */

public class Program
{
    public static void Main()
    {
        // Punto di ingresso della Console App.
        
        // applicazione.Avvia();
        TestNegozioOnline.EseguiTuttiITest();
    }
}

public class ApplicazioneNegozio
{
    private readonly CatalogoProdotti catalogoProdotti;
    private readonly CarrelloUtente carrelloUtente;
    private readonly StoricoAcquisti storicoAcquisti;
    private readonly ServizioNegozio servizioNegozio;

    public ApplicazioneNegozio()
    {
        catalogoProdotti = new CatalogoProdotti();
        carrelloUtente = new CarrelloUtente();
        storicoAcquisti = new StoricoAcquisti();
        servizioNegozio = new ServizioNegozio(catalogoProdotti, carrelloUtente, storicoAcquisti);

        CaricaDatiIniziali();
    }

    public void Avvia()
    {
        // TODO: implementare il ciclo principale della Console App.
        // Suggerimento:
        // 1. mostrare un messaggio di benvenuto;
        // 2. chiedere se l'utente vuole entrare come "utente" o "amministratore";
        // 3. chiamare GestisciMenuUtente oppure GestisciMenuAmministratore;
        // 4. permettere l'uscita dal programma con una scelta dedicata.
        throw new NotImplementedException("Completare il metodo Avvia.");
    }

    private void CaricaDatiIniziali()
    {
        // Metodo già implementato: fornisce prodotti di partenza per testare subito il sistema.
        catalogoProdotti.AggiungiProdotto(new Prodotto("P001", "Tastiera meccanica", 79.90m, 10));
        catalogoProdotti.AggiungiProdotto(new Prodotto("P002", "Mouse wireless", 24.50m, 25));
        catalogoProdotti.AggiungiProdotto(new Prodotto("P003", "Monitor 24 pollici", 149.99m, 7));
        catalogoProdotti.AggiungiProdotto(new Prodotto("P004", "Cavo USB-C", 9.99m, 40));
    }

    private string ScegliRuolo()
    {
        // TODO: leggere da console il ruolo scelto.
        // Valori consigliati: "utente", "amministratore", "esci".
        // Gestire input vuoti e maiuscole/minuscole con Trim() e ToLower().
        throw new NotImplementedException("Completare il metodo ScegliRuolo.");
    }

    private void GestisciMenuUtente()
    {
        // TODO: implementare il menu utente.
        // Operazioni richieste dalla traccia:
        // - visualizzare catalogo;
        // - aggiungere prodotto al carrello;
        // - visualizzare carrello;
        // - modificare quantità nel carrello;
        // - rimuovere prodotto dal carrello;
        // - svuotare carrello;
        // - confermare acquisto;
        // - visualizzare storico acquisti dell'utente.
        throw new NotImplementedException("Completare il metodo GestisciMenuUtente.");
    }

    private void GestisciMenuAmministratore()
    {
        // TODO: implementare il menu amministratore.
        // Operazioni richieste dalla traccia:
        // - visualizzare catalogo completo;
        // - aggiungere prodotto;
        // - eliminare prodotto;
        // - modificare prezzo;
        // - aumentare o diminuire quantità disponibile;
        // - visualizzare tutti gli acquisti;
        // - visualizzare quantità iniziale, venduta e disponibile per prodotto.
        throw new NotImplementedException("Completare il metodo GestisciMenuAmministratore.");
    }


    private void MostraCatalogo()
    {
        // Metodo già implementato: mostra a video tutti i prodotti del catalogo.
        List<Prodotto> prodotti = catalogoProdotti.OttieniTuttiIProdotti();

        Console.WriteLine();
        Console.WriteLine("=== CATALOGO PRODOTTI ===");

        if (prodotti.Count == 0)
        {
            Console.WriteLine("Il catalogo è vuoto.");
            return;
        }

        foreach (Prodotto prodotto in prodotti)
        {
            Console.WriteLine(
                prodotto.CodiceProdotto + " - " +
                prodotto.Nome + " - " +
                prodotto.Prezzo.ToString("0.00") + " euro - " +
                "Disponibili: " + prodotto.QuantitaDisponibile);
        }
    }

    private void MostraCarrello()
    {
        // Metodo già implementato: mostra contenuto del carrello e totale corrente.
        List<ElementoCarrello> elementi = carrelloUtente.OttieniElementi();

        Console.WriteLine();
        Console.WriteLine("=== CARRELLO ===");

        if (elementi.Count == 0)
        {
            Console.WriteLine("Il carrello è vuoto.");
            return;
        }

        foreach (ElementoCarrello elemento in elementi)
        {
            Console.WriteLine(
                elemento.ProdottoSelezionato.CodiceProdotto + " - " +
                elemento.ProdottoSelezionato.Nome + " - " +
                "Quantità: " + elemento.QuantitaScelta + " - " +
                "Prezzo unitario: " + elemento.PrezzoUnitario.ToString("0.00") + " euro - " +
                "Parziale: " + elemento.CalcolaTotaleParziale().ToString("0.00") + " euro");
        }

        Console.WriteLine("Totale carrello: " + carrelloUtente.CalcolaTotale().ToString("0.00") + " euro");
    }

    private void MostraStoricoUtente()
    {
        // Metodo già implementato: chiede un nome e mostra gli acquisti collegati.
        Console.Write("Inserisci nome utente: ");
        string? nomeUtente = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nomeUtente))
        {
            Console.WriteLine("Nome utente non valido.");
            return;
        }

        List<Acquisto> acquistiUtente = storicoAcquisti.OttieniAcquistiPerUtente(nomeUtente);

        Console.WriteLine();
        Console.WriteLine("=== STORICO ACQUISTI DI " + nomeUtente.Trim() + " ===");

        if (acquistiUtente.Count == 0)
        {
            Console.WriteLine("Nessun acquisto trovato per questo utente.");
            return;
        }

        foreach (Acquisto acquisto in acquistiUtente)
        {
            servizioNegozio.StampaAcquisto(acquisto);
        }
    }

   private int LeggiInteroPositivo(string messaggio)
{
    int valore;
    while (true)
    {
        Console.Write(messaggio);
        string? input = Console.ReadLine();
        if (int.TryParse(input, out valore) && valore > 0)
        {
            return valore;
        }
        Console.WriteLine("Input non valido. Inserisci un numero intero maggiore di zero.");
    }
}
private decimal LeggiPrezzoPositivo(string messaggio)
{
    decimal valore;
    while (true)
    {
        Console.Write(messaggio);
        string? input = Console.ReadLine();
        if (decimal.TryParse(input, out valore) && valore > 0)
        {
            return valore;
        }
        Console.WriteLine("Input non valido. Inserisci un prezzo (decimale) maggiore di zero.");
    }
}
}

public interface IGestioneCatalogo
{
    void AggiungiProdotto(Prodotto prodotto);
    bool EliminaProdotto(string codiceProdotto);
    Prodotto? CercaProdottoPerCodice(string codiceProdotto);
    List<Prodotto> OttieniTuttiIProdotti();
    bool ModificaPrezzoProdotto(string codiceProdotto, decimal nuovoPrezzo);
    bool ModificaQuantitaProdotto(string codiceProdotto, int variazioneQuantita);
}

public interface IGestioneCarrello
{
    bool AggiungiAlCarrello(Prodotto prodotto, int quantita);
    bool ModificaQuantitaNelCarrello(string codiceProdotto, int nuovaQuantita);
    bool RimuoviDalCarrello(string codiceProdotto);
    void SvuotaCarrello();
    decimal CalcolaTotale();
    List<ElementoCarrello> OttieniElementi();
}

public interface IGestioneAcquisti
{
    void RegistraAcquisto(Acquisto acquisto);
    List<Acquisto> OttieniTuttiGliAcquisti();
    List<Acquisto> OttieniAcquistiPerUtente(string nomeUtente);
}

public class Utente
{
    public string Nome { get; private set; }

    public Utente(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("Il nome utente non può essere vuoto.");
        }

        Nome = nome.Trim();
    }
}

public class Prodotto
{
    public string CodiceProdotto { get; private set; }
    public string Nome { get; private set; }
    public decimal Prezzo { get; private set; }
    public int QuantitaDisponibile { get; private set; }
    public int QuantitaIniziale { get; private set; }

    public Prodotto(string codiceProdotto, string nome, decimal prezzo, int quantitaDisponibile)
    {
        CodiceProdotto = codiceProdotto;
        Nome = nome;
        Prezzo = prezzo;
        QuantitaDisponibile = quantitaDisponibile;
        QuantitaIniziale = quantitaDisponibile;
    }

    public void CambiaPrezzo(decimal nuovoPrezzo)
    {
        // Metodo già implementato: centralizza la validazione del prezzo.
        if (nuovoPrezzo <= 0)
        {
            throw new ArgumentException("Il prezzo deve essere maggiore di zero.");
        }

        Prezzo = nuovoPrezzo;
    }

    public void CambiaQuantita(int variazioneQuantita)
    {
        // Metodo già implementato: impedisce di portare il magazzino sotto zero.
        int nuovaQuantita = QuantitaDisponibile + variazioneQuantita;

        if (nuovaQuantita < 0)
        {
            throw new InvalidOperationException("La quantità disponibile non può diventare negativa.");
        }

        QuantitaDisponibile = nuovaQuantita;
    }

    public int CalcolaQuantitaVenduta()
    {
        // Metodo già implementato: serve per il report amministratore.
        return QuantitaIniziale - QuantitaDisponibile;
    }
}

public class ElementoCarrello
{
    public Prodotto ProdottoSelezionato { get; private set; }
    public int QuantitaScelta { get; private set; }
    public decimal PrezzoUnitario { get; private set; }

    public ElementoCarrello(Prodotto prodottoSelezionato, int quantitaScelta)
    {
        ProdottoSelezionato = prodottoSelezionato;
        QuantitaScelta = quantitaScelta;
        PrezzoUnitario = prodottoSelezionato.Prezzo;
    }

    public decimal CalcolaTotaleParziale()
    {
        // Metodo già implementato: evita di duplicare il calcolo del parziale.
        return PrezzoUnitario * QuantitaScelta;
    }

    public void CambiaQuantitaScelta(int nuovaQuantita)
{
    if (nuovaQuantita <= 0)
    {
        throw new ArgumentException("La quantità scelta deve essere maggiore di zero.");
    }
    QuantitaScelta = nuovaQuantita;
}
}

public class Acquisto
{
    public Utente Utente { get; private set; }
    public string NomeUtente
    {
        get { return Utente.Nome; }
    }

    public List<ElementoAcquistato> ProdottiAcquistati { get; private set; }
    public decimal TotaleOrdine { get; private set; }
    public DateTime DataAcquisto { get; private set; }

    public Acquisto(Utente utente, List<ElementoAcquistato> prodottiAcquistati)
    {
        Utente = utente;
        ProdottiAcquistati = prodottiAcquistati;
        DataAcquisto = DateTime.Now;
        TotaleOrdine = CalcolaTotaleOrdine();
    }

    private decimal CalcolaTotaleOrdine()
    {
        // Metodo già implementato: somma tutti i parziali dei prodotti acquistati.
        return ProdottiAcquistati.Sum(prodotto => prodotto.TotaleParziale);
    }
}

public class ElementoAcquistato
{
    public string CodiceProdotto { get; private set; }
    public string NomeProdotto { get; private set; }
    public int QuantitaAcquistata { get; private set; }
    public decimal PrezzoUnitario { get; private set; }
    public decimal TotaleParziale { get; private set; }

    public ElementoAcquistato(string codiceProdotto, string nomeProdotto, int quantitaAcquistata, decimal prezzoUnitario)
    {
        CodiceProdotto = codiceProdotto;
        NomeProdotto = nomeProdotto;
        QuantitaAcquistata = quantitaAcquistata;
        PrezzoUnitario = prezzoUnitario;
        TotaleParziale = prezzoUnitario * quantitaAcquistata;
    }
}

public class CatalogoProdotti : IGestioneCatalogo
{
    private readonly List<Prodotto> prodotti;

    public CatalogoProdotti()
    {
        prodotti = new List<Prodotto>();
    }

    public void AggiungiProdotto(Prodotto prodotto)
    {
        // Metodo già implementato: evita codici duplicati nel catalogo.
        bool codiceGiaPresente = prodotti.Any(p => p.CodiceProdotto == prodotto.CodiceProdotto);

        if (codiceGiaPresente)
        {
            throw new InvalidOperationException("Esiste già un prodotto con lo stesso codice.");
        }

        prodotti.Add(prodotto);
    }

    public bool EliminaProdotto(string codiceProdotto)
{

    Prodotto? prodotto = CercaProdottoPerCodice(codiceProdotto);
    
    if (prodotto != null)
    {
        prodotti.Remove(prodotto);
        return true; // Prodotto trovato ed eliminato
    }
    
    return false; // Prodotto non trovato
}

    public Prodotto? CercaProdottoPerCodice(string codiceProdotto)
    {
        // Metodo già implementato: ricerca case-insensitive per rendere più comodo l'input da console.
        return prodotti.FirstOrDefault(prodotto =>
            prodotto.CodiceProdotto.Equals(codiceProdotto, StringComparison.OrdinalIgnoreCase));
    }

    public List<Prodotto> OttieniTuttiIProdotti()
    {
        // Metodo già implementato: restituisce una copia per proteggere la lista interna.
        return new List<Prodotto>(prodotti);
    }

    public bool ModificaPrezzoProdotto(string codiceProdotto, decimal nuovoPrezzo)
{
    Prodotto? prodotto = CercaProdottoPerCodice(codiceProdotto);
    
    if (prodotto == null)
    {
        return false; // Codice non esistente
    }

    // Il metodo CambiaPrezzo della classe Prodotto valida già che il prezzo sia > 0
    prodotto.CambiaPrezzo(nuovoPrezzo);
    return true;
}

   public bool ModificaQuantitaProdotto(string codiceProdotto, int variazioneQuantita)
{
    Prodotto? prodotto = CercaProdottoPerCodice(codiceProdotto);
    
    if (prodotto == null)
    {
        return false; // Prodotto non trovato
    }

    // Il metodo CambiaQuantita della classe Prodotto controlla già internamente 
    // che lo stock finale non vada sotto zero (lanciando InvalidOperationException)
    prodotto.CambiaQuantita(variazioneQuantita);
    return true;
}
}
public class CarrelloUtente : IGestioneCarrello
{
    private readonly List<ElementoCarrello> elementiCarrello;

    public CarrelloUtente()
    {
        elementiCarrello = new List<ElementoCarrello>();
    }

    public bool AggiungiAlCarrello(Prodotto prodotto, int quantita)
{
    // Rifiuta quantità minori o uguali a zero o maggiori della disponibilità iniziale
    if (quantita <= 0 || quantita > prodotto.QuantitaDisponibile)
    {
        return false;
    }

    // Cerchiamo se il prodotto è già presente nel carrello (confronto case-insensitive)
    ElementoCarrello? esistente = elementiCarrello.FirstOrDefault(e => 
        e.ProdottoSelezionato.CodiceProdotto.Equals(prodotto.CodiceProdotto, StringComparison.OrdinalIgnoreCase));

    if (esistente != null)
    {
        // Controlliamo che la quantità totale non superi il magazzino
        if (esistente.QuantitaScelta + quantita > prodotto.QuantitaDisponibile)
        {
            return false;
        }
        esistente.CambiaQuantitaScelta(esistente.QuantitaScelta + quantita);
    }
    else
    {
        // Se non è presente, creiamo una nuova riga nel carrello
        elementiCarrello.Add(new ElementoCarrello(prodotto, quantita));
    }

    return true;
}

    public bool ModificaQuantitaNelCarrello(string codiceProdotto, int nuovaQuantita)
{
    if (nuovaQuantita <= 0)
    {
        return false;
    }

    // Cerchiamo l'elemento nel carrello
    ElementoCarrello? elemento = elementiCarrello.FirstOrDefault(e => 
        e.ProdottoSelezionato.CodiceProdotto.Equals(codiceProdotto, StringComparison.OrdinalIgnoreCase));

    if (elemento == null || nuovaQuantita > elemento.ProdottoSelezionato.QuantitaDisponibile)
    {
        return false;
    }

    elemento.CambiaQuantitaScelta(nuovaQuantita);
    return true;
}

   public bool RimuoviDalCarrello(string codiceProdotto)
{
    ElementoCarrello? elemento = elementiCarrello.FirstOrDefault(e => 
        e.ProdottoSelezionato.CodiceProdotto.Equals(codiceProdotto, StringComparison.OrdinalIgnoreCase));

    if (elemento != null)
    {
        elementiCarrello.Remove(elemento);
        return true;
    }

    return false;
}
    public void SvuotaCarrello()
    {
        // Metodo già implementato: cancella tutti gli elementi del carrello.
        elementiCarrello.Clear();
    }

    public decimal CalcolaTotale()
    {
        // Metodo già implementato: ricalcola sempre il totale dai parziali correnti.
        return elementiCarrello.Sum(elemento => elemento.CalcolaTotaleParziale());
    }

    public List<ElementoCarrello> OttieniElementi()
    {
        // Metodo già implementato: restituisce una copia per evitare modifiche esterne dirette.
        return new List<ElementoCarrello>(elementiCarrello);
    }
}

public class StoricoAcquisti : IGestioneAcquisti
{
    private readonly List<Acquisto> acquisti;

    public StoricoAcquisti()
    {
        acquisti = new List<Acquisto>();
    }

    public void RegistraAcquisto(Acquisto acquisto)
    {
        // Metodo già implementato: conserva l'acquisto in memoria durante l'esecuzione.
        acquisti.Add(acquisto);
    }

    public List<Acquisto> OttieniTuttiGliAcquisti()
    {
        // Metodo già implementato: restituisce una copia dello storico.
        return new List<Acquisto>(acquisti);
    }

    public List<Acquisto> OttieniAcquistiPerUtente(string nomeUtente)
{
    return acquisti
        .Where(acquisto => acquisto.NomeUtente.Equals(nomeUtente, StringComparison.OrdinalIgnoreCase))
        .ToList();
}
}

public class ServizioNegozio
{
    private readonly CatalogoProdotti catalogoProdotti;
    private readonly CarrelloUtente carrelloUtente;
    private readonly StoricoAcquisti storicoAcquisti;

    public ServizioNegozio(CatalogoProdotti catalogoProdotti, CarrelloUtente carrelloUtente, StoricoAcquisti storicoAcquisti)
    {
        this.catalogoProdotti = catalogoProdotti;
        this.carrelloUtente = carrelloUtente;
        this.storicoAcquisti = storicoAcquisti;
    }

    public bool AggiungiProdottoAlCarrello(string codiceProdotto, int quantita)
    {
        // TODO: cercare il prodotto nel catalogo e delegare a carrelloUtente.AggiungiAlCarrello.
        // Restituire false se il prodotto non esiste o se la quantità non è valida.
        throw new NotImplementedException("Completare il metodo AggiungiProdottoAlCarrello.");
    }

    public Acquisto ConfermaAcquisto(Utente utente)
    {
        // TODO: completare la conferma dell'acquisto.
        // Regole richieste dalla traccia:
        // - impedire l'acquisto se il carrello è vuoto;
        // - ricontrollare che ogni quantità sia valida e disponibile in magazzino;
        // - creare gli ElementoAcquistato partendo dagli elementi del carrello;
        // - diminuire la quantità disponibile dei prodotti acquistati;
        // - registrare l'acquisto nello storico;
        // - svuotare il carrello dopo un acquisto completato;
        // - creare e restituire un Acquisto associato all'Utente ricevuto.
        throw new NotImplementedException("Completare il metodo ConfermaAcquisto.");
    }

    public List<ReportProdotto> CreaReportProdotti()
    {
        // Metodo già implementato: prepara il report richiesto per l'amministratore.
        return catalogoProdotti.OttieniTuttiIProdotti()
            .Select(prodotto => new ReportProdotto(
                prodotto.CodiceProdotto,
                prodotto.Nome,
                prodotto.QuantitaIniziale,
                prodotto.CalcolaQuantitaVenduta(),
                prodotto.QuantitaDisponibile))
            .ToList();
    }

    public void StampaAcquisto(Acquisto acquisto)
    {
        // Metodo già implementato: mostra i dettagli di un acquisto completato.
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Utente: " + acquisto.NomeUtente);
        Console.WriteLine("Data: " + acquisto.DataAcquisto.ToString("dd/MM/yyyy HH:mm"));
        Console.WriteLine("Prodotti acquistati:");

        foreach (ElementoAcquistato elemento in acquisto.ProdottiAcquistati)
        {
            Console.WriteLine(
                "- " + elemento.CodiceProdotto + " - " +
                elemento.NomeProdotto + " - " +
                "Quantità: " + elemento.QuantitaAcquistata + " - " +
                "Prezzo unitario: " + elemento.PrezzoUnitario.ToString("0.00") + " euro - " +
                "Parziale: " + elemento.TotaleParziale.ToString("0.00") + " euro");
        }

        Console.WriteLine("Totale ordine: " + acquisto.TotaleOrdine.ToString("0.00") + " euro");
    }

    public void StampaReportProdotti()
    {
        // Metodo già implementato: mostra il report quantità richiesto all'amministratore.
        List<ReportProdotto> report = CreaReportProdotti();

        Console.WriteLine();
        Console.WriteLine("=== REPORT PRODOTTI ===");

        if (report.Count == 0)
        {
            Console.WriteLine("Nessun prodotto presente nel catalogo.");
            return;
        }

        foreach (ReportProdotto riga in report)
        {
            Console.WriteLine(
                riga.CodiceProdotto + " - " +
                riga.NomeProdotto + " - " +
                "Iniziale: " + riga.QuantitaIniziale + " - " +
                "Venduta: " + riga.QuantitaVenduta + " - " +
                "Disponibile: " + riga.QuantitaDisponibile);
        }
    }
}

public class ReportProdotto
{
    public string CodiceProdotto { get; private set; }
    public string NomeProdotto { get; private set; }
    public int QuantitaIniziale { get; private set; }
    public int QuantitaVenduta { get; private set; }
    public int QuantitaDisponibile { get; private set; }

    public ReportProdotto(string codiceProdotto, string nomeProdotto, int quantitaIniziale, int quantitaVenduta, int quantitaDisponibile)
    {
        CodiceProdotto = codiceProdotto;
        NomeProdotto = nomeProdotto;
        QuantitaIniziale = quantitaIniziale;
        QuantitaVenduta = quantitaVenduta;
        QuantitaDisponibile = quantitaDisponibile;
    }
}