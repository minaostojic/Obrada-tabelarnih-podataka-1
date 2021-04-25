using System;
using System.IO;

class MainClass {
  struct Podaci_o_filmovima_rezisera
  {
    public string reziser;
    public string[] zanrovi;
    public int[] br_filmova;
  }

  static void Ucitavanje_podataka (ref string[,] matrica)
  {
    if (File.Exists("ulazni_podaci.csv"))
    {
      StreamReader podaci = new StreamReader("ulazni_podaci.csv");
      //string[,] matrica = new string[1000,6];
      int brojac=0;
      string s = podaci.ReadLine();
      while (!podaci.EndOfStream)      
      {
        s = podaci.ReadLine();
        string[] elementi = s.Split(";"); //provera
        for (int i=0; i<6; i++)
        {
          matrica[brojac,i] = elementi[i];

        }
        brojac++;
      }
    }
    else Console.Error.WriteLine("Greska! Ne postoji datoteka ulazni_podaci");
  }

  //Metoda za ucitavanje niza zanrova
  static string[] Unos_zanrova()
  {
    Console.WriteLine("Unesite zanrove po izboru (odvojene zapetama)");
    string[] zanr = Console.ReadLine().Split(",");
    string[] zanr_niz = new string[zanr.Length];
    for (int i=0; i<zanr_niz.Length; i++)
      zanr_niz[i]=zanr[i];
    return zanr_niz;
  }

   //Metoda vraca indeks vec unetog rezisera, koji se trazi
  static int Indeks_trazenog_rezisera (string reziser,Podaci_o_filmovima_rezisera[] strukture)
  {
    for (int i=0; i<strukture.Length; i++)
      if (reziser == strukture[i].reziser) return i;
    return -1;
  }

  //Metoda za proveru da li zanr matrice postoji u zeljenom nizu
  static bool Postoji_zanr_u_nizu (string[] zanr_niz, string zanr)
  {
    for(int i=0;i<zanr_niz.Length;i++)
      if(zanr==zanr_niz[i])return true;
    return false;
  }

  static bool Postoji_zanr_od_tog_rezisera(string zanr,string[,] podaci_matrica)
  {
    for(int i=0;i<podaci_matrica.Length;i++)
    {
      if(podaci_matrica[i,2]==zanr)return true;
    }
    return false;
  }

  static Podaci_o_filmovima_rezisera[] Vec_postoji_reziser(Podaci_o_filmovima_rezisera[] reziser_zanrovil, string reziseri,int brojac_struktura)
  {
    for(int i=0;i<brojac_struktura;i++)
    {
      if(reziseri==reziser_zanrovi[i].reziser)
    }
  }

  //Glavna metoda obrade: izdvajaju se reziseri, njihovi zanrovi i broj zanrova
  static Podaci_o_filmovima_rezisera[] Izdvajanje_zanrova_filmova_sa_reziserima (string[,] podaci_matrica, int[] zanr_niz)
  {
    Podaci_o_filmovima_rezisera[] reziser_zanrovi = new Podaci_o_filmovima_rezisera[1000];
    int brojac_struktura = 0; //brojac razlicitih rezisera(brojac-1 dalje u programu)
    int indeks_rezisera = 0;
    string[] niz;
    
    for (int i=0; i<podaci_matrica.GetLength(0); i++)
    {
      niz = podaci_matrica[i,2].Split("|");
      indeks_rezisera = Indeks_trazenog_rezisera(podaci_matrica[i,4],reziser_zanrovi);
      if (indeks_rezisera != -1)
      {
        for(int j=0;j<zanr_niz.Length;j++)
        {

        }
      }
      else
      {
        reziser_filmovi[brojac_struktura].reziser = podaci_matrica[i,4];
        brojaci_filmova[brojac_struktura] = 1;
        Array.Resize(ref reziser_filmovi[brojac_struktura].filmovi, 1);
        reziser_filmovi[brojac_struktura].filmovi[0] = film;
        //brojaci_filmova[brojac_struktura]=0; unet je prvi film u novu strukturu;
        Array.Resize(ref reziser_filmovi[brojac_struktura].zarade, 1);
        reziser_filmovi[brojac_struktura].zarade[0] = zarada;
        reziser_filmovi[brojac_struktura].ukupna_zarada = 0;
        brojac_struktura++;
      }
    }
    Array.Resize(ref reziser_filmovi,brojac_struktura);
    return reziser_filmovi;
  }

  static void Ispis_niza_struktura(Podaci_o_filmovima_rezisera[] niz)
  {
    StreamWriter ispis_provera = new StreamWriter("ispis_provera.txt");
    for (int i=0; i<niz.Length; i++)
    {
      ispis_provera.Write(niz[i].reziser);
      for (int j=0; j<niz[i].zanrovi.Length; j++)
        ispis_provera.Write(niz[i].zanrovi[j]+" | "+niz[i].br_filmova[j]);
      ispis_provera.WriteLine();
    }
  }

  static void Ispis_matrice_provera (int[,] matrica)
  {
    for (int i=0; i<matrica.GetLength(0); i++)
    {
      for (int j=0; j<matrica.GetLength(1); j++)
      {
        Console.Write(matrica[i,j]+" ");
      }
      Console.WriteLine();
    }
  }
  public static void Main (string[] args) {
    string[,] podaci_matrica = new string[1000,6];
    Ucitavanje_podataka(ref podaci_matrica);
    int[,] zanr_niz = Unos_zanrova();
    Ispis_matrice_provera(period_matrica);
    Podaci_o_filmovima_rezisera[] niz_provera = Izdvajanje_zanrova_filmova_sa_reziserima(podaci_matrica,zanr_niz);
    Ispis_niza_struktura(niz_provera);
  }
}