using System;
using System.IO;

class MainClass {
  struct Podaci_o_filmovima_rezisera
  {
    public string reziser;
    public string[] zanrovi;
    public int[] br_filmovi;
  }

  static void Ucitavanje_podataka (ref string[,] matrica)
  {
    if (File.Exists("ulazni_podaci.csv"))
    {
      StreamReader podaci = new StreamReader("ulazni_podaci.csv");
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
      podaci.Close();
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

  //Metoda Leksikografski sortira niz stringova
  public static void LeksikografskiPoredak(ref string[] a)
  {
    bool promena = true;
    while(promena)
    {
      promena = false;
      for(int i=0;i < a.Length-1; i++)
      {
        if(a[i].CompareTo(a[i+1])==1)
        {
          a[i] = Zamena(ref  a[i+1], a[i]);
          promena = true;
        }
      }
    }
  }
  public static string Zamena(ref string a, string b)
  {
    string j = a;
    a = b;
    return j; 
  }

  static void Postoji_zanr_od_tog_rezisera(Podaci_o_filmovima_rezisera reziser_zanrovi ,string[] niz_ulazni_zanrovi) //proveriti zagrade
  {
    for(int i=0;i<niz_ulazni_zanrovi.Length;i++)
    {
      for (int j=0; j<reziser_zanrovi.zanrovi.Length;j++)
      {
        if(niz_ulazni_zanrovi[i]==reziser_zanrovi.zanrovi[j])
        {
          reziser_zanrovi.br_filmovi[j]++;
        }
      }
    }
  }
  
  //Metoda koja proverava da li reziser vec postoji 
  static int Vec_postoji_reziser(Podaci_o_filmovima_rezisera[] reziser_zanrovi, string reziser,int brojac_struktura)
  {
    for(int i=0;i<brojac_struktura;i++)
    {
      if(reziser==reziser_zanrovi[i].reziser)return i;
    }
    return -1;
  }

  static bool Poredjenje_sa_konzolom(string zanr_niz,string[] niz_ulazni_zanrovi)
  {
      for (int j=0; j<niz_ulazni_zanrovi.Length;j++)
      {
        if(zanr_niz==niz_ulazni_zanrovi[j])return true;
      }
      return false;
  }
  
  //Glavna metoda obrade: izdvajaju se reziseri, njihovi zanrovi i broj zanrova
  static Podaci_o_filmovima_rezisera[] Izdvajanje_zanrova_filmova_sa_reziserima (string[,] podaci_matrica, string[] zanr_niz)
  {
    Podaci_o_filmovima_rezisera[] reziser_zanrovi = new Podaci_o_filmovima_rezisera[1000];
    int brojac_struktura = 0; //brojac razlicitih rezisera(brojac-1 dalje u programu)
    string[] niz_ulazni_zanrovi;
    
    for (int i=0; i<podaci_matrica.GetLength(0); i++)
    {
      niz_ulazni_zanrovi = podaci_matrica[i,2].Split("|");
      int indeks=Vec_postoji_reziser(reziser_zanrovi,podaci_matrica[i,4],brojac_struktura);
      if(indeks!=-1)
      {
        for(int k=0; k<zanr_niz.Length; k++)
        {
          if(Poredjenje_sa_konzolom(zanr_niz[k],niz_ulazni_zanrovi))
          {
             Postoji_zanr_od_tog_rezisera(reziser_zanrovi[indeks],niz_ulazni_zanrovi); 
          }
        }
      }
      else
      {
        int brojac=0;
        reziser_zanrovi[brojac_struktura].br_filmovi= new int [zanr_niz.Length];
        reziser_zanrovi[brojac_struktura].zanrovi= new string [zanr_niz.Length];
        for(int k=0; k<zanr_niz.Length; k++)
        {
          if(Poredjenje_sa_konzolom(zanr_niz[k],niz_ulazni_zanrovi))
          {
            reziser_zanrovi[brojac_struktura].reziser = podaci_matrica[i,4];
            reziser_zanrovi[brojac_struktura].zanrovi[brojac] = zanr_niz[k];
            reziser_zanrovi[brojac_struktura].br_filmovi[brojac] = 1;
            brojac++;
          }
        }
        Array.Resize(ref reziser_zanrovi[brojac_struktura].br_filmovi,brojac);
        Array.Resize(ref reziser_zanrovi[brojac_struktura].zanrovi,brojac);
        brojac_struktura++;
      }
    }
    Array.Resize(ref reziser_zanrovi,brojac_struktura);
    return reziser_zanrovi;
  }

  static void Ispis_niza_struktura(Podaci_o_filmovima_rezisera[] niz)
  {
    StreamWriter ispis_provera = new StreamWriter("ispis_provera.txt");
    for (int i=0; i<niz.Length; i++)
    {
      ispis_provera.Write(niz[i].reziser+" ");
      Console.Write(niz[i].reziser+" ");
      for (int j=0; j<niz[i].zanrovi.Length; j++)
      {
        ispis_provera.Write(niz[i].zanrovi[j]+" | "+niz[i].br_filmovi[j]+" ");
        Console.Write(niz[i].zanrovi[j]+" | "+niz[i].br_filmovi[j]+" ");
      }
      ispis_provera.WriteLine();
      Console.WriteLine();
    }
    ispis_provera.Close();
  }

  static void Ispis_matrice_provera (string[,] matrica)
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
    string[] zanr_niz = Unos_zanrova();
    //Ispis_matrice_provera(podaci_matrica);
    Podaci_o_filmovima_rezisera[] niz_provera = Izdvajanje_zanrova_filmova_sa_reziserima(podaci_matrica,zanr_niz);
    Ispis_niza_struktura(niz_provera);
  }
}