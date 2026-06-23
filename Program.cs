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
        ApplicazioneNegozio applicazione = new ApplicazioneNegozio();

        // Sblocchiamo l'avvio del negozio reale interattivo
        applicazione.Avvia();

        // Se vuoi che i test vengano comunque eseguiti alla chiusura del negozio, lasciarlo attivo:
        // TestNegozioOnline.EseguiTuttiITest();
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

    private void CaricaDatiIniziali()
    {
        // Metodo già implementato: fornisce prodotti di partenza per testare subito il sistema.
        catalogoProdotti.AggiungiProdotto(new Prodotto("P001", "Tastiera meccanica", 79.90m, 10));
        catalogoProdotti.AggiungiProdotto(new Prodotto("P002", "Mouse wireless", 24.50m, 25));
        catalogoProdotti.AggiungiProdotto(new Prodotto("P003", "Monitor 24 pollici", 149.99m, 7));
        catalogoProdotti.AggiungiProdotto(new Prodotto("P004", "Cavo USB-C", 9.99m, 40));
    }
    public void Avvia()
    {
        Console.Clear();
        Console.WriteLine("==================================================================");
        Console.WriteLine("             SISTEMA GESTIONALE - NEXUS RETAILING                 ");
        Console.WriteLine("                  Sincronizzazione Magazzino v2.4                 ");
        Console.WriteLine("==================================================================");
        Console.WriteLine("\n[SISTEMA] Inizializzazione dei moduli completata con successo.");
        Console.WriteLine("[INFO] Benvenuto nel portale ufficiale del negozio online.");

        bool continua = true;
        while (continua)
        {
            string ruolo = ScegliRuolo();
            switch (ruolo)
            {
                case "utente":
                    GestisciMenuUtente();
                    break;
                case "amministratore":
                    GestisciMenuAmministratore();
                    break;
                case "esci":
                    continua = false;
                    Console.Clear();
                    Console.WriteLine("==================================================================");
                    Console.WriteLine("         GRAZIE PER AVER UTILIZZATO I NOSTRI SERVIZI GESTIONALI   ");
                    Console.WriteLine("                      Sessione chiusa correttamente.              ");
                    Console.WriteLine("==================================================================");
                    break;
            }
        }
    }
    private string ScegliRuolo()
    {
        while (true)
        {
            // Pulisce la console per tenere la schermata ordinata ad ogni ritorno al menu principale
            Console.Clear();

            Console.WriteLine("=======================================");
            Console.WriteLine("            MENU PRINCIPALE            ");
            Console.WriteLine("=======================================");
            Console.WriteLine("Scegli come accedere al sistema:");
            Console.WriteLine("1. Accedi come Cliente (Utente)");
            Console.WriteLine("2. Accedi come Admin (Amministratore)");
            Console.WriteLine("0. Chiudi l'applicazione (Esci)");
            Console.Write("Seleziona un'opzione: ");

            string? input = Console.ReadLine()?.Trim();

            if (input == "1") return "utente";
            if (input == "2") return "amministratore";
            if (input == "0") return "esci";

            Console.WriteLine("\nOpzione non valida. Premi un tasto per riprovare...");
            Console.ReadKey(); // Aspetta che l'utente prema un tasto prima di rifare il Clear e il ciclo
        }
    }

    private void GestisciMenuUtente()
    {
        Console.Clear();
        Console.WriteLine("==================================================================");
        Console.WriteLine("                  AUTENTICAZIONE UTENTE / CLIENTE                 ");
        Console.WriteLine("==================================================================");
        Console.Write("\nSi prega di inserire il proprio Nome Utente per accedere: ");
        string? nome = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("\n[X] ERRORE: Input non valido. Il Nome Utente non può essere vuoto.");
            Console.WriteLine("Rinvio in corso al menu di identificazione primario...");
            Console.WriteLine("\nPremere un tasto per continuare...");
            Console.ReadKey();
            return;
        }
        Utente sessioneUtente = new Utente(nome);

        bool inMenu = true;
        while (inMenu)
        {
            Console.Clear();
            Console.WriteLine("==================================================================");
            Console.WriteLine($"  PORTALE CLIENTI  |  Sessione Attiva: {sessioneUtente.Nome.ToUpper()}");
            Console.WriteLine("==================================================================");
            Console.WriteLine("  [1] Esplora il Catalogo Prodotti Disponibili");
            Console.WriteLine("  [2] Aggiungi un Prodotto al Carrello");
            Console.WriteLine("  [3] Ispeziona il Carrello Corrente");
            Console.WriteLine("  [4] Modifica Quantità di un Elemento nel Carrello");
            Console.WriteLine("  [5] Rimuovi un Prodotto dal Carrello");
            Console.WriteLine("  [6] Svuota Integralmente il Carrello");
            Console.WriteLine("  [7] Procedi alla Cassa e Conferma l'Acquisto");
            Console.WriteLine("  [8] Consulta il Proprio Storico Ordini");
            Console.WriteLine("  [0] Termina Sessione Cliente (Torna al Menu Principale)");
            Console.WriteLine("------------------------------------------------------------------");
            Console.Write("Selezionare l'operazione desiderata: ");

            string? scelta = Console.ReadLine();
            Console.WriteLine();

            switch (scelta)
            {
                case "1":
                    MostraCatalogo();
                    break;

                case "2":
                    MostraCatalogo();
                    Console.WriteLine("------------------------------------------------------------------");
                    Console.Write("[RICHIESTA] Inserire il codice identificativo del prodotto: ");
                    string? codAdd = Console.ReadLine()?.Trim();
                    int qtaAdd = LeggiInteroPositivo("[RICHIESTA] Specificare la quantità da riservare: ");

                    if (servizioNegozio.AggiungiProdottoAlCarrello(codAdd ?? "", qtaAdd))
                        Console.WriteLine("\n[√] SUCCESSO: L'articolo è stato registrato nel carrello.");
                    else
                        Console.WriteLine("\n[X] ERRORE: Impossibile elaborare la richiesta. Verificare il codice o lo stock disponibile.");
                    break;

                case "3":
                    MostraCarrello();
                    break;

                case "4":
                    MostraCarrello();
                    Console.WriteLine("------------------------------------------------------------------");
                    Console.Write("[RICHIESTA] Inserire il codice del prodotto da modificare: ");
                    string? codMod = Console.ReadLine()?.Trim();
                    int qtaMod = LeggiInteroPositivo("[RICHIESTA] Specificare il nuovo quantitativo totale: ");

                    if (carrelloUtente.ModificaQuantitaNelCarrello(codMod ?? "", qtaMod))
                        Console.WriteLine("\n[√] SUCCESSO: Il quantitativo in carrello è stato aggiornato correttamente.");
                    else
                        Console.WriteLine("\n[X] ERRORE: Aggiornamento respinto. La quantità richiesta eccede lo stock di magazzino o il codice è errato.");
                    break;

                case "5":
                    MostraCarrello();
                    Console.WriteLine("------------------------------------------------------------------");
                    Console.Write("[RICHIESTA] Inserire il codice del prodotto da rimuovere: ");
                    string? codRem = Console.ReadLine()?.Trim();

                    if (carrelloUtente.RimuoviDalCarrello(codRem ?? ""))
                        Console.WriteLine("\n[√] SUCCESSO: L'articolo selezionato è stato rimosso dal carrello.");
                    else
                        Console.WriteLine("\n[X] ERRORE: Riferimento non trovato all'interno del carrello.");
                    break;

                case "6":
                    Console.Write("[ATTENZIONE] Confermare lo svuotamento totale del carrello? (s/n): ");
                    string? confermaSvuota = Console.ReadLine()?.Trim().ToLower();
                    if (confermaSvuota == "s" || confermaSvuota == "si")
                    {
                        carrelloUtente.SvuotaCarrello();
                        Console.WriteLine("\n[√] SUCCESSO: Il carrello è stato completamente azzerato.");
                    }
                    else
                    {
                        Console.WriteLine("\n[i] INFO: Operazione annullata. Nessuna modifica applicata.");
                    }
                    break;

                case "7":
                    try
                    {
                        Console.WriteLine("[PROCESSO] Elaborazione della transazione finanziaria in corso...");
                        Acquisto confermato = servizioNegozio.ConfermaAcquisto(sessioneUtente);
                        Console.WriteLine("\n[√] TRANSAZIONE COMPLETATA: L'ordine è stato preso in carico dal magazzino!");
                        servizioNegozio.StampaAcquisto(confermato);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n[X] ERRORE DI TRANSAZIONE: Impossibile finalizzare l'ordine. Dettaglio: {ex.Message}");
                    }
                    break;

                case "8":
                    MostraStoricoUtente();
                    break;

                case "0":
                    inMenu = false;
                    break;

                default:
                    Console.WriteLine("[X] NOTA: Selezione non valida. Si prega di utilizzare i codici numerici previsti.");
                    break;
            }

            if (inMenu)
            {
                Console.WriteLine("\nPremere un tasto per tornare al pannello principale cliente...");
                Console.ReadKey();
            }
        }
    }
    private void GestisciMenuAmministratore()
    {
        bool inMenu = true;
        while (inMenu)
        {
            Console.Clear();
            Console.WriteLine("==================================================================");
            Console.WriteLine("           PANNELLO DI CONTROLLO AMMINISTRATIVO & LOGISTICA       ");
            Console.WriteLine("==================================================================");
            Console.WriteLine("  [1] Ispeziona Inventario Completo dei Beni");
            Console.WriteLine("  [2] Censimento ed Immissione Nuovo Prodotto a Catalogo");
            Console.WriteLine("  [3] Depennamento / Eliminazione Definitiva di un Prodotto");
            Console.WriteLine("  [4] Rimodulazione Listino Prezzi (Modifica Prezzo)");
            Console.WriteLine("  [5] Rettifica Scorte di Magazzino (Variazione Stock)");
            Console.WriteLine("  [6] Audit Globale Storico Transazioni del Negozio");
            Console.WriteLine("  [7] Genera Report Vendite e Analisi Giacenze");
            Console.WriteLine("  [0] Termina Sessione Amministratore (Torna al Menu Principale)");
            Console.WriteLine("------------------------------------------------------------------");
            Console.Write("Selezionare l'azione logistica da intraprendere: ");

            string? scelta = Console.ReadLine();
            Console.WriteLine();

            switch (scelta)
            {
                case "1":
                    MostraCatalogo();
                    break;

                case "2":
                    Console.Write("[REQUISITO] Impostare l'ID Codice Univoco del nuovo prodotto: ");
                    string? nuovoCod = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(nuovoCod))
                    {
                        Console.WriteLine("\n[X] ANOMALIA: Il codice identificativo non può essere nullo o composto da soli spazi.");
                        break;
                    }

                    Console.Write("[REQUISITO] Specificare la Designazione Commerciale (Nome): ");
                    string? nuovoNome = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(nuovoNome))
                    {
                        Console.WriteLine("\n[X] ANOMALIA: Il nome commerciale del bene è obbligatorio.");
                        break;
                    }

                    decimal nuovoPrezzo = LeggiPrezzoPositivo("[REQUISITO] Determinare la tariffa di vendita unitaria: ");
                    int nuovaQta = LeggiInteroPositivo("[REQUISITO] Caricare lo stock iniziale di magazzino: ");

                    try
                    {
                        catalogoProdotti.AggiungiProdotto(new Prodotto(nuovoCod, nuovoNome, nuovoPrezzo, nuovaQta));
                        Console.WriteLine("\n[√] INVENTARIO AGGIORNATO: Il nuovo articolo è ora disponibile per i clienti.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n[X] OPERAZIONE FALLITA: {ex.Message}");
                    }
                    break;

                case "3":
                    MostraCatalogo();
                    Console.WriteLine("------------------------------------------------------------------");
                    Console.Write("[REQUISITO] Specificare il codice del prodotto da rimuovere dall'inventario: ");
                    string? codDel = Console.ReadLine()?.Trim();

                    if (catalogoProdotti.EliminaProdotto(codDel ?? ""))
                        Console.WriteLine("\n[√] INVENTARIO AGGIORNATO: L'articolo è stato rimosso in modo permanente.");
                    else
                        Console.WriteLine("\n[X] ANOMALIA: Nessuna corrispondenza trovata per il codice inserito.");
                    break;

                case "4":
                    MostraCatalogo();
                    Console.WriteLine("------------------------------------------------------------------");
                    Console.Write("[RICHIESTA] Inserire il codice del prodotto da riprezzare: ");
                    string? codPrc = Console.ReadLine()?.Trim();
                    decimal prc = LeggiPrezzoPositivo("[RICHIESTA] Digitare il nuovo valore di listino (Euro): ");

                    if (catalogoProdotti.ModificaPrezzoProdotto(codPrc ?? "", prc))
                        Console.WriteLine("\n[√] LISTINO AGGIORNATO: Il nuovo prezzo è in vigore sul portale clienti.");
                    else
                        Console.WriteLine("\n[X] ANOMALIA: Impossibile aggiornare. Verificare l'esistenza del codice prodotto.");
                    break;

                case "5":
                    MostraCatalogo();
                    Console.WriteLine("------------------------------------------------------------------");
                    Console.Write("[RICHIESTA] Inserire il codice identificativo della merce: ");
                    string? codStk = Console.ReadLine()?.Trim();
                    Console.Write("[RICHIESTA] Digitare '+' per incrementare lo stock o '-' per decrementarlo: ");
                    string? segno = Console.ReadLine()?.Trim();
                    int variazione = LeggiInteroPositivo("[RICHIESTA] Indicare l'entità quantitativa della variazione: ");

                    if (segno == "-") variazione = -variazione;
                    else if (segno != "+")
                    {
                        Console.WriteLine("\n[X] OPERAZIONE ANNULLATA: Indicatore di direzione scorte ('+' o '-') non riconosciuto.");
                        break;
                    }

                    try
                    {
                        if (catalogoProdotti.ModificaQuantitaProdotto(codStk ?? "", variazione))
                            Console.WriteLine("\n[√] SCORTE AGGIORNATE: La giacenza fisica è stata modificata correttamente.");
                        else
                            Console.WriteLine("\n[X] ANOMALIA: Codice articolo inesistente nel database logistico.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n[X] ERRORE LOGISTICO: Impossibile completare la variazione. Dettaglio: {ex.Message}");
                    }
                    break;

                case "6":
                    List<Acquisto> tuttiAcquisti = storicoAcquisti.OttieniTuttiGliAcquisti();
                    Console.WriteLine("==================================================================");
                    Console.WriteLine("                  REGISTRO REVISIONE AUDIT TRANSATTIVO             ");
                    Console.WriteLine("==================================================================");
                    if (tuttiAcquisti.Count == 0) Console.WriteLine("[i] Registro vuoto. Nessuna transazione registrata sul server.");
                    foreach (var acq in tuttiAcquisti)
                    {
                        servizioNegozio.StampaAcquisto(acq);
                    }
                    break;

                case "7":
                    servizioNegozio.StampaReportProdotti();
                    break;

                case "0":
                    inMenu = false;
                    break;

                default:
                    Console.WriteLine("[X] NOTA: Selezione non ammessa nel pannello di controllo.");
                    break;
            }

            if (inMenu)
            {
                Console.WriteLine("\nPremere un tasto per tornare al pannello amministrativo...");
                Console.ReadKey();
            }
        }
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
        // Cerca il prodotto nel catalogo
        Prodotto? prodotto = catalogoProdotti.CercaProdottoPerCodice(codiceProdotto);

        // Se il prodotto non esiste, restituisce false
        if (prodotto == null)
        {
            return false;
        }

        // Delega la logica di controllo e inserimento alla classe carrelloUtente
        return carrelloUtente.AggiungiAlCarrello(prodotto, quantita);
    }

    public Acquisto ConfermaAcquisto(Utente utente)
    {
        // 1. Impedire l'acquisto se il carrello è vuoto
        List<ElementoCarrello> elementi = carrelloUtente.OttieniElementi();
        if (elementi.Count == 0)
        {
            throw new InvalidOperationException("Il carrello è vuoto. Impossibile procedere con l'acquisto.");
        }

        // 2. Ricontrollare preventivamente che ogni quantità sia valida e disponibile in magazzino
        foreach (ElementoCarrello elemento in elementi)
        {
            Prodotto? prodCatalogo = catalogoProdotti.CercaProdottoPerCodice(elemento.ProdottoSelezionato.CodiceProdotto);
            if (prodCatalogo == null || elemento.QuantitaScelta > prodCatalogo.QuantitaDisponibile)
            {
                throw new InvalidOperationException($"Il prodotto '{elemento.ProdottoSelezionato.Nome}' non è più disponibile nella quantità richiesta.");
            }
        }

        // 3. Creare gli ElementoAcquistato partendo dagli elementi del carrello e diminuire lo stock
        List<ElementoAcquistato> prodottiAcquistati = new List<ElementoAcquistato>();
        foreach (ElementoCarrello elemento in elementi)
        {
            Prodotto prodCatalogo = catalogoProdotti.CercaProdottoPerCodice(elemento.ProdottoSelezionato.CodiceProdotto)!;

            // Diminuire la quantità disponibile inserendo una variazione negativa
            prodCatalogo.CambiaQuantita(-elemento.QuantitaScelta);

            // Creazione dell'elemento storico dell'ordine
            prodottiAcquistati.Add(new ElementoAcquistato(
                prodCatalogo.CodiceProdotto,
                prodCatalogo.Nome,
                elemento.QuantitaScelta,
                elemento.PrezzoUnitario
            ));
        }

        // 4. Creare e registrare l'acquisto nello storico associato all'Utente ricevuto
        Acquisto nuovoAcquisto = new Acquisto(utente, prodottiAcquistati);
        storicoAcquisti.RegistraAcquisto(nuovoAcquisto);

        // 5. Svuotare il carrello dopo un acquisto completato
        carrelloUtente.SvuotaCarrello();

        return nuovoAcquisto;
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