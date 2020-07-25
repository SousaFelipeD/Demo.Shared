using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Shared.ValueObjects
{
    public class Telefone : ValueObject
    {
        public Telefone(string ddi, string ddd, string numero, string ramal)
        {
            Ddi = ddi;
            Ddd = ddd;
            Numero = numero;
            Ramal = ramal;

            AddNotifications(new Contract()
                    .IsNotNullOrEmpty(numero,nameof(Numero),"O telefone não possui um número")
                    .IsLowerOrEqualsThan(numero.Length, 3, nameof(Numero), "O número do telefone é pequeno.")
                    );
        }

        public string Ddi { get; private set; }
        public string Ddd { get; private set; }
        public string Numero { get; private set; }
        public string Ramal { get; private set; }

        public string ToFormat()
        {
            return Numero;
        }

        public override string ToString()
        {
            return Ddi + Ddd + Numero;
        }
    }
}
