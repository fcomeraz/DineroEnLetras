using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberToLettersApp
{
    public class NumberToLetterConverter
    {
        struct Grupo
        {
            public String grupo;
            public List<int> exclude;
            public string plural;

            public Grupo( string grupo, List<int> exclude, string plural)
            {
                this.grupo = grupo;
                this.exclude = exclude;
                this.plural = plural;
            }
            
        }
         
        Dictionary<int, string> NumerosEspeciales = new Dictionary<int, string>() { { 0, "Cero" }, { 1, "Un" }, { 2, "Dos" }, { 3, "Tres" }, { 4, "Cuatro" }, { 5, "Cinco" }, { 6, "Seis" }, { 7, "Siete" }, { 8, "Ocho" }, { 9, "Nueve" }, { 10, "Diez" }, { 11, "Once" }, { 12, "Doce" }, { 13, "Trece" }, { 14, "Catorce" }, { 15, "Quince" }, { 20, "Veinte" }, { 100, "Cien" } };
        Dictionary<int, string> Decenas = new Dictionary<int, string>() { { 1, "Diez" }, { 2, "Veinti" }, { 3, "Treinta" }, { 4, "Cuarenta" }, { 5, "Cincuenta" }, { 6, "Sesenta" }, { 7, "Setenta" }, { 8, "Ochenta" }, { 9, "Noventa" } };
        Dictionary<int, string> Centenas = new Dictionary<int, string>() { { 1, "Ciento" }, { 2, "Docientos" }, { 3, "Trecientos" }, { 4, "Cuatrocientos" }, { 5, "Quinientos" }, { 6, "Seicientos" }, { 7, "Setecientos" }, { 8, "Ochocientos" }, { 9, "Novecientos" } };

        Dictionary<int, Grupo> NumerosGrupos = new Dictionary<int, Grupo>()
        {
            { 2, new Grupo("Mil", new List<int>() { 1 }, null)},
            { 3, new Grupo("Millon", null, "Millones")},
            { 4, new Grupo("Billon", null, "Billones")}
        };

        private string ConvertHelper(int number)
        {
            string ret = "";

            if(number == 0)
            {
                return "";
            }

            if (NumerosEspeciales.ContainsKey(number))
            {
                return NumerosEspeciales[number];
            }else
            {
                int u = number % 10;
                int d = (number / 10) % 10;
                int c = (number / 100) % 10;

                if (Centenas.ContainsKey(c))
                {
                    ret += Centenas[c];
                }

                if (number%100 > 0)
                {
                    if (NumerosEspeciales.ContainsKey(number % 100))
                    {
                        ret += (ret.Count() > 0 ? " " : "") + NumerosEspeciales[number % 100];
                    }else
                    {
                        if (Decenas.ContainsKey(d))
                        {
                            ret += (ret.Count() > 0 ? " " : "") + Decenas[d];
                        }

                        if(u > 0)
                        {
                            if (NumerosEspeciales.ContainsKey(u))
                            {
                                if (d == 2) ret += NumerosEspeciales[u].ToLower();
                                else ret += " y " + NumerosEspeciales[u];
                            }
                        }
                    }
                }

                return ret;
            }

        }

        public String Convert(int number)
        {
            string ret = "";

            if (NumerosEspeciales.ContainsKey(number))
            {
                return NumerosEspeciales[number];
            }else
            {
                int grupo = 1;
                int subNumber = number % 1000;
                
                while(number.ToString().Count() > (grupo - 1) * 3)
                {
                    if (subNumber > 0)
                    {
                        if (NumerosGrupos.ContainsKey(grupo))
                        {
                            Grupo numberGroup = NumerosGrupos[grupo];
                            if(numberGroup.exclude != null)
                            {
                                if (numberGroup.exclude.Contains(subNumber))
                                {
                                    ret = ((subNumber > 1 && numberGroup.plural != null) ? numberGroup.plural : numberGroup.grupo) + " " + ret;
                                }
                                else
                                {
                                    ret = ConvertHelper(subNumber) + " " +((subNumber > 1 && numberGroup.plural != null) ? numberGroup.plural : numberGroup.grupo) + " " + ret;
                                }
                            }
                            else
                            {
                                ret = ConvertHelper(subNumber) + " " + ((subNumber > 1 && numberGroup.plural != null) ? numberGroup.plural : numberGroup.grupo) + " " + ret;
                            }
                        }
                        else
                        {
                            ret = ConvertHelper(subNumber) + (ret.Count() > 0 ? " " + ret : "");
                        }
                    }
                    grupo++;
                    subNumber = (number % ((int)(Math.Pow(1000, grupo))))/((int)(Math.Pow(1000, grupo -1)));
                }
            }

            return ret;
        }

        public string convertToMoney(double number)
        {
            int intPart = (int)number;
            int floatPart = (int)((number - intPart) * 100);
            return Convert(intPart) + " " + (intPart != 1 ? "Lempira" : "Lempiras") + " con " + floatPart + "/100 Centavos.";
        }
    }
}
